using System;
using System.IO;
using System.Windows.Forms;

namespace East_CSharp
{
    class MatrixSa
    {
        private NormalRandom normRand = new NormalRandom();
        //Период повторяемости
        private double returnPeriodT;


        //Количество периодов в матрице SA
        private int periodCountInSaMatrix = 33;

        //Количество значений PGA в матрице SA
        private int pgaCountInSaMatrix = 62;
        
        //Матрица
        private double[,] matrixSa = new double[62, 33];
        private double[,] matrixSaKumulative = new double[62, 33];
        private double[,] matrixSaItog = new double[62, 33];

        //сохранение 
        private String directoryName;
        private DirectoryInfo directoryInfo;

        //Конструктор
        public MatrixSa(double Tmax)
        {
            this.returnPeriodT = Tmax;
            this.directoryName = Application.StartupPath;

            for (int i = 0; i < periodCountInSaMatrix - 2; i++)
            {
                matrixSa[0, i + 2] = Math.Pow(10, (-1.5 + (Convert.ToDouble(i) / 10.0)));
            }

            for (int i = 0; i < pgaCountInSaMatrix - 1; i++)
            {
                matrixSa[i + 1, 0] = Math.Pow(10, -3 + (Convert.ToDouble(i) / 10.0));
            }

            matrixSa[0, 1] = 0.01;
        }

        public void Calculate(ResponseSpectra responseSpectra)
        {
            if ("SIS17".Equals(responseSpectra.Type))
            {
                AptikaevCalcualte(responseSpectra);
                return;
            } else if ("GK2008".Equals(responseSpectra.Type))
            {
                GraizerkalkanCalculate(responseSpectra);
            }
            else
            {
                OtherCalculation(responseSpectra);
                return;
            }
        }

        private void OtherCalculation(ResponseSpectra responseSpectra)
        {
            double stepLgD = 0.1;
            for (int i = 0; i < 30; i++)
            {
                //double logInIsa = Math.Log10((Math.Pow(10, responseSpectra.Pga) / 981) * responseSpectra.CurrentBettaResponseSpectra[i, 1]);

                double doubleIsa = (3 + Math.Round(Math.Log10(responseSpectra.CurrentBettaResponseSpectra[1, i]) * Math.Pow(stepLgD, -1)) * stepLgD) * 10 + 1;
                int isa = Convert.ToInt32(doubleIsa);
                if (isa > 0 && isa < 40 && i > 0 && i < 30)
                {
                    matrixSa[isa, i] = matrixSa[isa, i] + 1;
                }
            }

        }

        private void GraizerkalkanCalculate(ResponseSpectra responseSpectra)
        {
            double doubleIsa;
            double stepLgD = 0.1;
            for (int i = 0; i < 30; i++)
            {
                doubleIsa = (3 + Math.Round(Math.Log10(responseSpectra.CurrentBettaResponseSpectra[1, i]) * Math.Pow(stepLgD, -1)) * stepLgD) * 10 + 1;
                int isa = Convert.ToInt32(doubleIsa);
                if (isa > 0 && isa < 40)
                {
                    matrixSa[isa, i + 2] = matrixSa[isa, i + 2] + 1;
                }
            }
            doubleIsa = (3 + Math.Round(Math.Log10(responseSpectra.Pga) * Math.Pow(stepLgD, -1)) * stepLgD) * 10 + 1;
            int ipga = Convert.ToInt32(doubleIsa);

            if (ipga > 0 && ipga < 40)
            {
                matrixSa[ipga, 1] = matrixSa[ipga, 1] + 1;
            }
        }

        private void AptikaevCalcualte(ResponseSpectra responseSpectra)
        {

            int isa, jsa, ipga;
            double logInIsa, doubleIsa, doubleIpga;
            int periondCountInOneRS = 20;
            double stepLgD = 0.1;
            for (int i = 0; i <= periondCountInOneRS; i++)
            {
                //находим номер столбца
                jsa = Convert.ToInt32(Math.Round((1.5 + Math.Log10(responseSpectra.CurrentBettaResponseSpectra[i, 0])) * 10 + 2));

                logInIsa = Math.Log10((Math.Pow(10, responseSpectra.Pga) / 981) * responseSpectra.CurrentBettaResponseSpectra[i, 1]);

                doubleIsa = (3 + Math.Round(logInIsa * Math.Pow(stepLgD, -1)) * stepLgD) * 10 + 1;
                //находим номер строки
                isa = Convert.ToInt32(doubleIsa);

                if (isa > 0 && isa < 40 && jsa > 1 && jsa < 30)
                {
                    matrixSa[isa, jsa] = matrixSa[isa, jsa] + 1;
                }
            }

            //заполняем значения для второго столбца - PGA
            doubleIpga = ((3 + Math.Round(Math.Log10((Math.Pow(10, responseSpectra.Pga)) / 981) * Math.Pow(stepLgD, -1)) * stepLgD) * 10) + 1;
            ipga = Convert.ToInt32(doubleIpga);

            if (ipga > 1 && ipga < 30)
            {
                matrixSa[ipga, 1] = matrixSa[ipga, 1] + 1;
            }
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
                    matrixSaItog[i, j] = Math.Round((1 - Math.Pow(Math.E, pow)) * 988) * 0.1;
                }
            }

        }



        //Функция, возвращающая значение с вероятностной кривой для любого значения периода
        public double PGASACalculation(double t, double P)
        {
            double X1, X2, Y1, Y2;
            double outPGA = 0;
            //расчитанная вероятностная функция
            double[,] probabilityFunction = ProbabilityCalculation(P);

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

        //функция пересчета в вероятность
        private double[,] ProbabilityCalculation(double P)
        {
            double X1, X2, Y1, Y2;
            double[,] probabilityFunction = new double[2, 61];
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
            return probabilityFunction;
        }


        public void WriteSAInConsole()
        {
            for (int i = 0; i < 62; i++)
            {
                for (int j = 0; j < 33; j++)
                {
                    Console.Write("{0,3:f3}\t", matrixSa[i, j]);
                }
                Console.WriteLine();

            }
        }
        //Сохранение результатов в текстовый файл для тестирования
       /* public void SAsave(string str)
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
        }*/

    }
}
