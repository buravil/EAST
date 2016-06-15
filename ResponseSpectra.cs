using System;
using System.IO;
using System.Windows.Forms;

namespace East_CSharp
{
    class ResponseSpectra
    {
        //Массив с коэффициентами типа грунта и кинематики
        double[] C2array = new double[] { -0.1, 0, 0.1 };
        double[] C1array = new double[] { -0.05, -0.025, 0, 0.025, 0.05 };
        double[] C3array = new double[] { -0.15, 0, 0.45 };
        double[] C4array = new double[] { -0.25, -0.12, 0, 0.12, 0.25 };
        double[] C5array = new double[] { -0.1, -0.05, 0, 0.05, 0.1 };
        double[] C6array = new double[] { 0.800, 0.717, 0.633, 0.550, 0.467 };
        double[] C7array = new double[] { -0.15, 0, 0.15 };
        double[] C8array = new double[] { -0.05, 0, 0.2 };

        NormalRandom normRand = new NormalRandom();

        //Коэффициенты
        double C1, C2, C3, C4, C5, C6, C7, C8;

        //Массив со значениями: S-ширина спектра, d-длительность, t-период, b-бетта
        // double[] SDTB;

        //шаг
        double lg_D;

       //количество итераций
        int Iter;

        //Период повторяемости
        double T;

        //Количество периодов в единичном спектре реакций
        int NN;

        //Количество периодов в матрице SA
        int Njsa;

        //Количество значений PGA в матрице SA
        int Nisa;

        //Матрица
        double[,] SA;
        double[,] SAkum;
        double[,] SAitog;

        //расчитанная вероятностная функция
        double[,] YY3;

        //сохранение 
        private String NameDIR;
private DirectoryInfo Dir;



        //Конструктор
        public ResponseSpectra(int typeOfGrunt, double Tmax )
        {
            //Задаем коэффициенты
            C2 = C2array[typeOfGrunt];
           
            C3 = C3array[typeOfGrunt];

            C7 = C7array[typeOfGrunt];
            C8 = C8array[typeOfGrunt];
            lg_D = 0.1;

            NN = Convert.ToInt32(Math.Round(2 * Math.Pow(lg_D, -1)));

            //Iter = 100;
            //T = 5000 * Iter;
            T = Tmax;

            Njsa = 33;
            Nisa = 62;
            SA = new double[Nisa, Njsa];
            SAitog = new double[Nisa, Njsa];
            SAkum = new double[Nisa, Njsa];
            YY3 = new double[2, 61];
            double test = Math.Pow(10, (-1.5 + (1.0 / 10)));


            NameDIR = Application.StartupPath;
           // Dir = Directory.CreateDirectory(NameDIR + "\\Out");

            for (int i = 0; i < Njsa - 2; i++)
            {
                SA[0, i + 2] = Math.Pow(10, (-1.5 + (Convert.ToDouble(i) / 10.0)));
            }

            for (int i = 0; i < Nisa - 1; i++)
            {
                SA[i + 1, 0] = Math.Pow(10, -3 + (Convert.ToDouble(i) / 10.0));
            }
        }

        public void Calculat(int kinematika, double M, double R)
        {
            C1 = C1array[kinematika];
            C4 = C4array[kinematika];
            C5 = C5array[kinematika];
            C6 = C6array[kinematika];

            SAcalculation(M, R);


        }

        //Расчет PGA
        private double PGAcalculation(double M, double R)
        {
            //Расчет PGA в зависимости от условия
            if (R < Math.Pow(10, 0.33 * M - 1.51))
            {
                return C6 * (0.33 * M - 0.61 - Math.Log10(Math.Pow(10, 0.33 * M - 1.51)));
            }
            else if (R >= Math.Pow(10, 0.33 * M - 1.51) && R <= Math.Pow(10, 0.33 * M - 0.61 + C7 * 0.8))
            {
                return (C6 * (0.33 * M - 0.61 - Math.Log10(R))) + 2.23;
            }
            else if (R > Math.Pow(10, 0.33 * M - 0.61 + C7 * 0.8))
            {
                return 0.634 * M - 1.92 * Math.Log10(R) + 1.076 + C7;
            }
            else
                return 0;

            //сделать проверку на отрицательную величину
        }

        //Расчет значений: s-ширина спектра, d-длительность, t-период, b-бетта и запись значений в массив sdtb[]
        private double[] SDTBcalculation(double M, double R)
        {
            double h;
            double[] sdtb = new double[4];
            sdtb[0] = 0.6 + C8 + C1;
            sdtb[1] = Math.Pow(10, 0.15 * M + 0.5 * Math.Log10(R) + C3 + C4 - 1.3);
            h = (R < Math.Pow(10, 0.33 * M - 1.51)) ? Math.Pow(10, 0.33 * M - 1.51) : R;
            sdtb[2] = Math.Pow(10, Math.Round((0.15 * M + 0.25 * Math.Log10(h) - 1.9 + C5) * Math.Pow(lg_D, -1), 0) * lg_D);
            sdtb[3] = 0.72 - 0.28 * sdtb[0] + 0.07 * Math.Log10(sdtb[1]);

            return sdtb;
        }

        //Расчет кривой бэтта с определенной магнитудой и расстоянием
        public double[,] BettaCalculation(double M, double R, double deltaB, double deltaT)
        {
            //случайные величины
            double tg_alfa;
            double Lgf1, Lgf2;
            double i0;
            double B2;

            //Массив вспомогательных значений
            double[] lg_B = new double[NN + 1];

            //Массив выходных значений
            double[,] AA = AA = new double[NN + 1, 2];

            //S-ширина спектра
            double S = SDTBcalculation(M, R)[0];

            //D - длительность
            double D = SDTBcalculation(M, R)[1];

            //T-период
            double T = SDTBcalculation(M, R)[2] + deltaT;
            T = (T <= 0) ? 0.01 : T;

            //Максимальное значение бетты
            double Bmax = Math.Pow(10, SDTBcalculation(M, R)[3] + deltaB);

            double LogBmax = Math.Log10(Bmax);

            tg_alfa = Math.Log10(0.5 * Bmax) / (S * 0.5);
            Lgf1 = LogBmax * Math.Pow(tg_alfa, -1);
            Lgf2 = -(0.8 * LogBmax) * Math.Pow(tg_alfa, -1);

            i0 = Math.Round((Lgf1 + 1) * Math.Pow(lg_D, -1));
            B2 = LogBmax - 2 * tg_alfa * Lgf2 - 0.8 * LogBmax;

            //Формирование спектра реакций в зависимости от условия
            for (int i = 0; i <= NN; i++)
            {
                double ii = -1 + lg_D * i;

                if (ii < Lgf2)
                {
                    lg_B[i] = 2 * tg_alfa * (ii) + B2;
                }
                else if (ii >= Lgf2 && ii < 0)
                {
                    lg_B[i] = tg_alfa * (ii) + LogBmax;
                }
                else if (ii >= 0 && i < i0)
                {
                    lg_B[i] = -tg_alfa * (ii) + LogBmax;
                }
                else if (i >= i0)
                {
                    lg_B[i] = 0;
                }

                AA[NN - i, 1] = Math.Pow(10, lg_B[i]);
                AA[NN - i, 0] = T * Math.Pow(10, 1 - lg_D * i);
            }

            return AA;
        }

        public void SAcalculation(double M, double R)
        {
            double deltaA, deltaB, deltaT;
            double PGA;
            double[,] Bitog = new double[NN + 1, 2];
            int isa, jsa, ipga;

            double logInIsa, doubleIsa, doubleIpga;

            if (R < 1.0807 * Math.Pow(Math.E, 0.976 * M))
            {
                deltaA = normRand.NextDouble() * 0.18;
                deltaB = normRand.NextDouble() * 0.07;
                deltaT = normRand.NextDouble() * 0.2;

                PGA = PGAcalculation(M, R) + deltaA;

                if (PGA > 0.01)
                {

                    Bitog = BettaCalculation(M, R, deltaB, deltaT);
                    for (int i = 0; i <= NN; i++)
                    {
                        //находим номер столбца
                        jsa = Convert.ToInt32(Math.Round((1.5 + Math.Log10(Bitog[i, 0])) * 10 + 2));

                        logInIsa = Math.Log10(0.001 * Math.Pow(10, PGA) * Bitog[i, 1]);

                        doubleIsa = (3 + Math.Round(logInIsa * Math.Pow(lg_D, -1)) * lg_D) * 10 + 1;
                        //находим номер строки
                        isa = Convert.ToInt32(doubleIsa);

                        if (isa > 0 && isa < 40 && jsa > 1 && jsa < 30)
                        {
                            SA[isa, jsa] = SA[isa, jsa] + 1;
                        }
                    }

                    //заполняем значения для второго столбца - PGA
                    doubleIpga = ((3 + Math.Round(Math.Log10(0.001 * Math.Pow(10, PGA)) * Math.Pow(lg_D, -1)) * lg_D) * 10) + 1;
                    ipga = Convert.ToInt32(doubleIpga);

                    if (ipga > 1 && ipga < 30)
                    {
                        SA[ipga, 1] = SA[ipga, 1] + 1;
                    }
                }
            }
        }

        //Расчет куммулятивной таблицы
        public void SAItogCalculation()
        {
            Array.Copy(SA, SAkum, SA.Length);

            for (int j = 1; j <= 30; j++)
            {
                for (int i = 1; i <= 50; i++)
                {
                    SAkum[51 - i, j] = SA[51 - i, j] + SAkum[52 - i, j];
                }
            }

            Array.Copy(SAkum, SAitog, SAkum.Length);

            for (int j = 1; j <= 30; j++)
            {
                for (int i = 1; i <= 50; i++)
                {
                    double pow = (-50 * (SAkum[i, j] / T));
                    SAitog[i, j] = Math.Round((1 - Math.Pow(Math.E, pow)) * 1000) * 0.1;
                }
            }

        }

        //функция пересчета в вероятность
        public void ProbabilityCalculation(double P)
        {
            double X1, X2, Y1, Y2;

            Array.Clear(YY3, 0, YY3.Length);

            for (int i = 1; i <= 32; i++)
            {
                X1 = 0;
                X2 = 0;
                Y1 = 0;
                Y2 = 0;
                for (int j = 2; j <= 60; j++)
                {
                    if (((SAitog[j - 1, i] <= P) && (SAitog[j, i] >= P)) || ((SAitog[j - 1, i] >= P) && (SAitog[j, i] <= P)))
                    {
                        X1 = (SAitog[j - 1, i] > 0) ? Math.Log10(SAitog[j - 1, i]) : 0;
                        X2 = (SAitog[j, i] > 0) ? Math.Log10(SAitog[j, i]) : 0;
                        Y1 = SAitog[j - 1, 0];
                        Y2 = SAitog[j, 0];

                        YY3[1, i] = (X1 != X2) ? ((Y2 - Y1) / (X2 - X1)) * Math.Log10(P) + Y1 - ((Y2 - Y1) / (X2 - X1)) * X1 : ((Y2 + Y1)/2);
                    }
                }
            }

            for (int i = 0; i <= 30; i++)
            {
                YY3[0, i + 2] = Math.Pow(10, (-1.5 + (i / 10.0)));
            }
            YY3[0, 1] = 0.01;
        }
        //Функция, возвращающая значение с вероятностной кривой для любого значения периода
        public double PGASACalculation(double t, double P)
        {
            double X1, X2, Y1, Y2;
            double outPGA = 0;
            ProbabilityCalculation(P);

            for (int i = 1; i <= 32; i++)
            {
                if (((YY3[0, i - 1] <= t) && (YY3[0, i] >= t)) || ((YY3[0, i - 1] >= t) && (YY3[0, i] <= t)))
                {
                    X1 = (YY3[0, i - 1] > 0) ? Math.Log10(YY3[0, i - 1]) : 0;
                    X2 = (YY3[0, i] > 0) ? Math.Log10(YY3[0, i]) : 0;
                    Y1 = YY3[1, i - 1];
                    Y2 = YY3[1, i];

                    outPGA = (X1 != X2) ? ((Y2 - Y1) / (X2 - X1)) * Math.Log10(t) + Y1 - ((Y2 - Y1) / (X2 - X1)) * X1 : ((Y2 + Y1) / 2);
                }
            }

            return outPGA;
        }

        //Сохранение результатов в текстовый файл
        public void SAsave(string str)
        {


         //   StreamWriter SAwriter = new StreamWriter(Dir.FullName + "\\" + "SA" + str + ".txt");

            StreamWriter SAwriterProbabil = new StreamWriter(Dir.FullName + "\\" + "RS_" + str + ".txt");

           // for (int i = 0; i < Nisa; i++)
           // {
           //     for (int j = 0; j < Njsa; j++)
          //      {
         //           SAwriter.Write("{0}\t", SAitog[i, j]);
         //       }
         //       SAwriter.Write("\n");
         //   }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 33; j++)
                {
                    SAwriterProbabil.Write("{0}\t", YY3[i, j]);
                }
                SAwriterProbabil.Write("\n");
            }


        //    SAwriter.Close();
            SAwriterProbabil.Close();


        }

    }
}
