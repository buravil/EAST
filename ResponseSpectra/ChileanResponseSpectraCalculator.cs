using System;
using System.Collections.Generic;
using System.Text;

namespace East_CSharp
{
    class ChileanResponseSpectraCalculator : IResponseSpectraCalculator
    {
        public EquationParameters EqParam { get; set; }

        private NormalRandom normRand = new NormalRandom();
        private int jsa = 30;
        private int isa = 60;
        private int i1 = 6;
        private int i2 = 9;
        private int i3 = 3;
        private double[,] STx;
        private double[,] C;
        private double[,] DC;
        private double[,] q;

        public ChileanResponseSpectraCalculator(EquationParameters parameters)
        {
            EqParam = parameters;
            STx = new double[jsa + 1, i1 + 1];
            C = new double[jsa + 1, i2 + 1];
            DC = new double[jsa + 1, i3 + 1];
            q = new double[jsa + 1, i3 + 1];

            for (int i = 0; i <= jsa; i++)
            {
                for (int j = 2; j <= i1; j++)
                {
                    STx[i, j] = EqParam.ChileanParameters.Chilean[i, j];
                }
            }

            for (int i = 0; i <= jsa; i++)
            {
                for (int j = 1; j <= i2; j++)
                {
                    C[i, j] = EqParam.ChileanParameters.Chilean[i, j + 6];
                }
            }

            for (int i = 0; i <= jsa; i++)
            {
                for (int j = 1; j <= i3; j++)
                {
                    DC[i, j] = EqParam.ChileanParameters.Chilean[i, j + 15];
                }
            }

            for (int i = 0; i <= jsa; i++)
            {
                for (int j = 1; j <= i3; j++)
                {
                    q[i, j] = EqParam.ChileanParameters.Chilean[i, j + 18];
                }
            }

        }

        public ResponseSpectra CalculateBetta(double M, double R, double H)
        {
            double lg_D = EqParam.SaAndRsParameters.StepLgD;
            int h0 = 50;
            int Mr = 5;
            int Feve = EqParam.ChileanParameters.Feve;
            double Vs = EqParam.ChileanParameters.Vs;
            double Vref = EqParam.ChileanParameters.Vref;
            double T30 = EqParam.ChileanParameters.T30;

            ResponseSpectra responseSpectra = new ResponseSpectra(EqParam.SaAndRsParameters.PeriodCountInSaMatrix, "Chilean2017");

            int ST = 2;

            if (T30 <= 0.2)
            {
                ST = 2;
            }
            else if (T30 > 0.2 && T30 <= 0.4)
            {
                ST = 3;
            }
            else if (T30 > 0.4 && T30 <= 0.8)
            {
                ST = 4;
            }
            else if (T30 > 0.8)
            {
                ST = 6;
            }

            for (int i = 0; i < EqParam.SaAndRsParameters.PeriodCountInSaMatrix - 2; i++)
            {
                responseSpectra.CurrentBettaResponseSpectra[0, i + 2] = Math.Pow(10, (-1.5 + (Convert.ToDouble(i) / 10.0)));
            }
            responseSpectra.CurrentBettaResponseSpectra[0, 1] = 0.01;

            

            double Rlim = Feve == 0 ? 100 * M + 500 : 66.429 * M + 197.86;

            double DfM = 0;
            double g = 0;
            double R0 = 0;
            double FF = 0;
            double FD = 0;
            double FS = 0;

            for (int i = 0; i <= jsa; i++)
            {
                double deltaSA = normRand.NextDouble() * q[i, 2];
                //double deltaSA1 = 0.313;
               // double deltaSA2 = normRand.NextDouble() * q[i, 3];
                //double deltaSA2 = 0.363;
                //double deltaSA = Math.Sqrt(deltaSA1 * deltaSA1 + deltaSA2 * deltaSA2);

                DfM = Feve == 0 ? C[i, 9] * (M * M) : DC[i, 1] + DC[i, 2] * M;
                g = C[i, 3] + C[i, 4] * (M - Mr) + DC[i, 3] * Feve;
                R0 = (1 - Feve) * C[i, 6] * Math.Pow(10, C[i, 7] * (M - Mr));
                FF = C[i, 1] + C[i, 2] * M + C[i, 8] * (H - h0) * Feve + DfM;
                FD = g * Math.Log10(R + R0) + C[i, 5] * R;

                FS = STx[i, ST] * Math.Log10(Vs / Vref);
                responseSpectra.CurrentBettaResponseSpectra[1, i] = Math.Pow(10, FF + FD + FS + deltaSA);
            }
            responseSpectra.IsCalculated = true;

            return responseSpectra;
        }
    }
}
