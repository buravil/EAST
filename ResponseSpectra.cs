using System;
using System.IO;
using System.Windows.Forms;

namespace East_CSharp
{
    class ResponseSpectra
    {
        //Массив с коэффициентами типа грунта и кинематики
        private double[] C2array = new double[] { -0.1, 0, 0.1 };
        private double[] C1array = new double[] { -0.05, -0.025, 0, 0.025, 0.05 };
        private double[] C3array = new double[] { -0.15, 0, 0.45 };
        private double[] C4array = new double[] { -0.25, -0.12, 0, 0.12, 0.25 };
        private double[] C5array = new double[] { -0.1, -0.05, 0, 0.05, 0.1 };
        private double[] C6array = new double[] { 0.800, 0.717, 0.633, 0.550, 0.467 };
        private double[] C7array = new double[] { -0.15, 0, 0.15 };
        private double[] C8array = new double[] { -0.05, 0, 0.2 };

        private NormalRandom normRand = new NormalRandom();

        //Коэффициенты
        private double C1, C2, C3, C4, C5, C6, C7, C8;

        //Массив со значениями: S-ширина спектра, d-длительность, t-период, b-бетта
        // double[] SDTB;

        private double duration;

        //шаг
        private double stepLgD;

        //Период повторяемости
        private double returnPeriodT;

        //Количество периодов в единичном спектре реакций
        private int periondCountInOneRS;

        //Количество периодов в матрице SA
        private int periodCountInSaMatrix = 33;

        //Количество значений PGA в матрице SA
        private int pgaCountInSaMatrix = 62;

        //текущий расчитанный спектр реакций
        private double[,] currentBettaResponseSpectra;

        //значение PGA
        private double pga;

        //Матрица
        private double[,] matrixSa = new double[62, 33];
        private double[,] matrixSaKumulative = new double[62, 33];
        private double[,] matrixSaItog = new double[62, 33];

        //расчитанная вероятностная функция
        private double[,] probabilityFunction;

        //сохранение 
        private String directoryName;
        private DirectoryInfo directoryInfo;

        //переменная-флаг, истина - если расчитался текущий спектр реакций
        private bool isCalculated;

        //Конструктор
        public ResponseSpectra(double Tmax)
        {
            this.stepLgD = 0.1;
            this.periondCountInOneRS = Convert.ToInt32(Math.Round(2 * Math.Pow(stepLgD, -1)));
            this.returnPeriodT = Tmax;
            this.currentBettaResponseSpectra = new double[periondCountInOneRS + 1, 2];
            this.probabilityFunction = new double[2, 61];
            this.directoryName = Application.StartupPath;

            for (int i = 0; i < periodCountInSaMatrix - 2; i++)
            {
                matrixSa[0, i + 2] = Math.Pow(10, (-1.5 + (Convert.ToDouble(i) / 10.0)));
            }

            for (int i = 0; i < pgaCountInSaMatrix - 1; i++)
            {
                matrixSa[i + 1, 0] = Math.Pow(10, -3 + (Convert.ToDouble(i) / 10.0));
            }
        }

        public bool IsCalculated { get => isCalculated; set => isCalculated = value; }
        public double Pga { get => pga; set => pga = value; }
        public double[,] CurrentBettaResponseSpectra { get => currentBettaResponseSpectra; set => currentBettaResponseSpectra = value; }
        public double Duration { get => duration; set => duration = value; }

        public void Calculate(int kinematika, int typeOfGrunt, double M, double R)
        {
            //Задаем коэффициенты
            this.C2 = C2array[typeOfGrunt];
            this.C3 = C3array[typeOfGrunt];
            this.C7 = C7array[typeOfGrunt];
            this.C8 = C8array[typeOfGrunt];

            this.C1 = C1array[kinematika];
            this.C4 = C4array[kinematika];
            this.C5 = C5array[kinematika];
            this.C6 = C6array[kinematika];

            SAcalculation(M, R);
        }

        public void SAcalculation(double M, double R)
        {
            double deltaA, deltaB, deltaT, deltaD;

            Array.Clear(CurrentBettaResponseSpectra, 0, CurrentBettaResponseSpectra.Length);
            Pga = 0;
            int isa, jsa, ipga;
            IsCalculated = false;
            double logInIsa, doubleIsa, doubleIpga;

            if (R < 1.0807 * Math.Pow(Math.E, 0.976 * M))
            {

                deltaA = normRand.NextDouble() * 0.18;
                deltaB = normRand.NextDouble() * 0.07;
                deltaT = normRand.NextDouble() * 0.2;
                deltaD = normRand.NextDouble() * 0.3;

                Pga = PGAcalculation(M, R) + deltaA;

                if (Pga > 0.01)
                {
                    IsCalculated = true;
                    CurrentBettaResponseSpectra = BettaCalculation(M, R, deltaB, deltaT, deltaD);
                    for (int i = 0; i <= periondCountInOneRS; i++)
                    {
                        //находим номер столбца
                        jsa = Convert.ToInt32(Math.Round((1.5 + Math.Log10(CurrentBettaResponseSpectra[i, 0])) * 10 + 2));

                        logInIsa = Math.Log10((Math.Pow(10, Pga) / 981) * CurrentBettaResponseSpectra[i, 1]);

                        doubleIsa = (3 + Math.Round(logInIsa * Math.Pow(stepLgD, -1)) * stepLgD) * 10 + 1;
                        //находим номер строки
                        isa = Convert.ToInt32(doubleIsa);

                        if (isa > 0 && isa < 40 && jsa > 1 && jsa < 30)
                        {
                            matrixSa[isa, jsa] = matrixSa[isa, jsa] + 1;
                        }
                    }

                    //заполняем значения для второго столбца - PGA
                    doubleIpga = ((3 + Math.Round(Math.Log10((Math.Pow(10, Pga)) / 981) * Math.Pow(stepLgD, -1)) * stepLgD) * 10) + 1;
                    ipga = Convert.ToInt32(doubleIpga);

                    if (ipga > 1 && ipga < 30)
                    {
                        matrixSa[ipga, 1] = matrixSa[ipga, 1] + 1;
                    }
                }
            }
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
            //sdtb[1] = Math.Pow(10, 0.15 * M + 0.5 * Math.Log10(R) + C3 + C4 - 1.3);
            sdtb[1] = 0.15 * M + 0.5 * Math.Log10(R) + C3 + C4 - 1.3;
            h = (R < Math.Pow(10, 0.33 * M - 1.51)) ? Math.Pow(10, 0.33 * M - 1.51) : R;
            sdtb[2] = Math.Pow(10, Math.Round((0.15 * M + 0.25 * Math.Log10(h) - 1.9 + C5) * Math.Pow(stepLgD, -1), 0) * stepLgD);
            sdtb[3] = 0.72 - 0.28 * sdtb[0] + 0.07 * sdtb[1];

            return sdtb;
        }

        //Расчет кривой бэтта с определенной магнитудой и расстоянием
        private double[,] BettaCalculation(double M, double R, double deltaB, double deltaT, double deltaD)
        {
            //случайные величины
            double tg_alfa;
            double Lgf1, Lgf2;
            double i0;
            double B2;

            //Массив вспомогательных значений
            double[] lg_B = new double[periondCountInOneRS + 1];

            //Массив выходных значений
            double[,] AA = AA = new double[periondCountInOneRS + 1, 2];

            //S-ширина спектра
            double S = SDTBcalculation(M, R)[0];

            //Расчитываем длительность
            Duration = Math.Pow(10, (SDTBcalculation(M, R)[1] + deltaD));

            //T-период
            double T = SDTBcalculation(M, R)[2] + deltaT;
            T = (T <= 0) ? 0.01 : T;

            //Максимальное значение бетты
            double Bmax = Math.Pow(10, SDTBcalculation(M, R)[3] + deltaB);

            double LogBmax = Math.Log10(Bmax);

            tg_alfa = Math.Log10(0.5 * Bmax) / (S * 0.5);
            Lgf1 = LogBmax * Math.Pow(tg_alfa, -1);
            Lgf2 = -(0.8 * LogBmax) * Math.Pow(tg_alfa, -1);

            i0 = Math.Round((Lgf1 + 1) * Math.Pow(stepLgD, -1));
            B2 = LogBmax - 2 * tg_alfa * Lgf2 - 0.8 * LogBmax;

            //Формирование спектра реакций в зависимости от условия
            for (int i = 0; i <= periondCountInOneRS; i++)
            {
                double ii = -1 + stepLgD * i;

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

                AA[periondCountInOneRS - i, 1] = Math.Pow(10, lg_B[i]);
                AA[periondCountInOneRS - i, 0] = T * Math.Pow(10, 1 - stepLgD * i);
            }

            return AA;
        }



        //Расчет куммулятивной таблицы
        public void SAItogCalculation()
        {
            Array.Copy(matrixSa, matrixSaKumulative, matrixSa.Length);

            for (int j = 1; j <= 30; j++)
            {
                for (int i = 1; i <= 50; i++)
                {
                    matrixSaKumulative[51 - i, j] = matrixSa[51 - i, j] + matrixSaKumulative[52 - i, j];
                }
            }

            Array.Copy(matrixSaKumulative, matrixSaItog, matrixSaKumulative.Length);

            for (int j = 1; j <= 30; j++)
            {
                for (int i = 1; i <= 50; i++)
                {
                    double pow = (-50 * (matrixSaKumulative[i, j] / returnPeriodT));
                    matrixSaItog[i, j] = Math.Round((1 - Math.Pow(Math.E, pow)) * 1000) * 0.1;
                }
            }

        }

        //функция пересчета в вероятность
        private void ProbabilityCalculation(double P)
        {
            double X1, X2, Y1, Y2;

            Array.Clear(probabilityFunction, 0, probabilityFunction.Length);

            for (int i = 1; i <= 32; i++)
            {
                X1 = 0;
                X2 = 0;
                Y1 = 0;
                Y2 = 0;
                for (int j = 2; j <= 60; j++)
                {
                    if (((matrixSaItog[j - 1, i] <= P) && (matrixSaItog[j, i] >= P)) || ((matrixSaItog[j - 1, i] >= P) && (matrixSaItog[j, i] <= P)))
                    {
                        X1 = (matrixSaItog[j - 1, i] > 0) ? Math.Log10(matrixSaItog[j - 1, i]) : 0;
                        X2 = (matrixSaItog[j, i] > 0) ? Math.Log10(matrixSaItog[j, i]) : 0;
                        Y1 = matrixSaItog[j - 1, 0];
                        Y2 = matrixSaItog[j, 0];

                        probabilityFunction[1, i] = (X1 != X2) ? ((Y2 - Y1) / (X2 - X1)) * Math.Log10(P) + Y1 - ((Y2 - Y1) / (X2 - X1)) * X1 : ((Y2 + Y1) / 2);
                    }
                }
            }

            for (int i = 0; i <= 30; i++)
            {
                probabilityFunction[0, i + 2] = Math.Pow(10, (-1.5 + (i / 10.0)));
            }
            probabilityFunction[0, 1] = 0.01;
        }

        //Функция, возвращающая значение с вероятностной кривой для любого значения периода
        public double PGASACalculation(double t, double P)
        {
            double X1, X2, Y1, Y2;
            double outPGA = 0;
            ProbabilityCalculation(P);

            for (int i = 1; i <= 32; i++)
            {
                if (((probabilityFunction[0, i - 1] <= t) && (probabilityFunction[0, i] >= t)) || ((probabilityFunction[0, i - 1] >= t) && (probabilityFunction[0, i] <= t)))
                {
                    X1 = (probabilityFunction[0, i - 1] > 0) ? Math.Log10(probabilityFunction[0, i - 1]) : 0;
                    X2 = (probabilityFunction[0, i] > 0) ? Math.Log10(probabilityFunction[0, i]) : 0;
                    Y1 = probabilityFunction[1, i - 1];
                    Y2 = probabilityFunction[1, i];

                    outPGA = (X1 != X2) ? ((Y2 - Y1) / (X2 - X1)) * Math.Log10(t) + Y1 - ((Y2 - Y1) / (X2 - X1)) * X1 : ((Y2 + Y1) / 2);
                }
            }

            return outPGA;
        }


        //Сохранение результатов в текстовый файл
        public void SAsave(string str)
        {
            StreamWriter SAwriterProbabil = new StreamWriter(directoryInfo.FullName + "\\" + "RS_" + str + ".txt");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 33; j++)
                {
                    SAwriterProbabil.Write("{0}\t", probabilityFunction[i, j]);
                }
                SAwriterProbabil.Write("\n");
            }
            SAwriterProbabil.Close();
        }

    }
}
