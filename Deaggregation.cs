using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace East_CSharp
{
    class Deaggregation
    {
        
        double[] periods = new double[] { 0.1, 0.2, 0.3, 0.4, 0.5, 0.7, 1, 2, 3, 5 };

        StreamReader testStream, BALLDEAGREG = null;        
        double lg_D;
        string aaa = "";
        double[,] ABCD;
        double[,] MR;

        long NumberOfPoint;

        private String NameDIR;
        private DirectoryInfo Dir;

        // double[] x;

        //Конструктор
        public Deaggregation(string path, long NumberOfPoint_)
        {
            //количество точек
            NumberOfPoint = NumberOfPoint_;
            //логарифмический шаг
            lg_D = 0.1;
            //Инициализация массива с деагрегацией
            MR = new double[1215 * NumberOfPoint, 90];
            ABCD = new double[NumberOfPoint, 90];

            NameDIR = Application.StartupPath;

            BALLDEAGREG = new StreamReader(path);
            aaa = BALLDEAGREG.ReadLine();
             while (BALLDEAGREG.EndOfStream != true)
            {
                aaa = BALLDEAGREG.ReadLine();
                String[] nums = aaa.Split('\t');

                for (int i=0;i< NumberOfPoint; i++)
                {
                    for (int j = 0; j < 90; j++)
                    {
                        ABCD[i,j] = Convert.ToDouble(nums[j]);
                    }
                }
            }

             for (int i=0; i< 1215 * NumberOfPoint; i++)
            {
                double x = (i - (Math.Round(i / 1215.0 + 0.5, MidpointRounding.AwayFromZero) - 1) * 1215) / 15.0 + 0.5;

                MR[i, 1] = (Math.Round(x, MidpointRounding.AwayFromZero)-1) * 5;

                MR[i, 0] = (i-(Math.Round(i/15.0+0.5, MidpointRounding.AwayFromZero) -1)*15) * 0.5 + 2;
            }

            int df = 0;
        }

        //тест функции
        public void calcTest()
        {
            double PGAA = 2.771;
            double[,] bitog = new double[21, 2];

            testStream = new StreamReader("D:\\!Работа2016\\EAST\\bin\\x86\\Debug\\testBitog.txt");
            int i = 0;
            while (testStream.EndOfStream != true)
            {
                aaa = testStream.ReadLine();
                String[] nums = aaa.Split('\t');

                bitog[i, 0] = Convert.ToDouble(nums[0]);
                bitog[i, 1] = Convert.ToDouble(nums[1]);
                i++;
            }

            SA_deag(8, 15, bitog, PGAA, 0);

            int tadfasdf = 0;
        }
        


        //расчет значения для любого периода
        public double SA_T(double T, double[,] Bitog, double PGA)
        {
            double YY3 = 0;
            double[] X = new double[Convert.ToInt16(Math.Round(2 * Math.Pow(lg_D, -1)))+1];
            double[] Y = new double[Convert.ToInt16(Math.Round(2 * Math.Pow(lg_D, -1)))+1];

            for (int i = 0; i <= Convert.ToInt16(Math.Round(2 * Math.Pow(lg_D, -1))); i++)
            {
                X[i] = Math.Log10(Bitog[i, 0]);
                Y[i] = Math.Log10(Math.Pow(10, -3) * Math.Pow(10, PGA) * Bitog[i, 1]);
            }

            for (int i = 1; i <= Convert.ToInt16(Math.Round(2 * Math.Pow(lg_D, -1))); i++)
            {
               if((Bitog[i-1,0]<=T)&& (Bitog[i, 0] >= T))
                {
                    YY3 = Math.Pow(10, (((Y[i] - Y[i - 1]) / (X[i] - X[i - 1])) * Math.Log10(T) + Y[i - 1] - ((Y[i] - Y[i - 1]) / (X[i] - X[i - 1])) * X[i - 1]));
                }
            }
            return YY3;
        }

        public void SA_deag(double Mlh, double R, double[,] Bitog, double PGA, int tochka)
        {
            double[,] Saa = new double[10, 2];

            for (int i = 0; i <=9; i++)
            {
                Saa[i, 0] = periods[i];
                Saa[i, 1] = SA_T(Saa[i, 0], Bitog, PGA);
            }
            
            int Round = Convert.ToInt16(Mlh * 2 - 4 + Math.Round(R*0.2, MidpointRounding.AwayFromZero) * 15 + 1215 * tochka);
            
            for (int i = 14; i <= 23; i++)
            {
                if (ABCD[0, i] <= Saa[i - 14, 1])
                {
                    MR[Round, i]++;
                }
            }
            for (int i = 25; i <= 34; i++)
            {
                if (ABCD[0, i] <= Saa[i - 25, 1])
                {
                    MR[Round, i]++;
                }
            }

            for (int i = 36; i <= 45; i++)
            {
                if (ABCD[0, i] <= Saa[i - 36, 1])
                {
                    MR[Round, i]++;
                }
            }

            for (int i = 47; i <= 56; i++)
            {
                if (ABCD[0, i] <= Saa[i - 47, 1])
                {
                    MR[Round, i]++;
                }
            }

            for (int i = 58; i <= 67; i++)
            {
                if (ABCD[0, i] <= Saa[i - 58, 1])
                {
                    MR[Round, i]++;
                }
            }

            for (int i = 69; i <= 78; i++)
            {
                if (ABCD[0, i] <= Saa[i - 69, 1])
                {
                    MR[Round, i]++;
                }
            }

            for (int i = 80; i <= 89; i++)
            {
                if (ABCD[0, i] <= Saa[i - 80, 1])
                {
                    MR[Round, i]++;
                }
            }
        }


        public void SaveGeagreg()
        {
            StreamWriter SAwriter = new StreamWriter(NameDIR + "\\" + "Geadgreg_SA_.txt");

            for (int i = 0; i< 1215 * NumberOfPoint; i++)
            {
                for (int j = 0; j < 90; j++)
                {
                    SAwriter.Write("{0}\t", MR[i, j]);
                }
                SAwriter.Write("\n");
            }
            SAwriter.Close();
        }
    }
}
