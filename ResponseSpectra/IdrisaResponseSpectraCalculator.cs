using System;
using System.Collections.Generic;
using System.Text;

namespace East_CSharp
{
    class IdrisaResponseSpectraCalculator : IResponseSpectraCalculator
    {
        private EquationParameters parameters;
        private const int R_LIMIT = 300;
        private NormalRandom normRand = new NormalRandom();

        private double[,] a = new double[32, 7];
        private double[,] b = new double[32, 5];
        private double[] g = new double[32];
        private double[] y = new double[32];
        private double[] f = new double[32];
        private double[] k = new double[32];
        private double F = 0;

        public IdrisaResponseSpectraCalculator(EquationParameters parameters)
        {
            this.parameters = parameters;
            for (int i = 0; i < 31; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    a[i, j] = parameters.IdrissaParameters.Idrissa[i, j];
                    a[i, j + 3] = parameters.IdrissaParameters.Idrissa[i, j+8];
                }

                for (int j = 1; j <= 2; j++)
                {
                    b[i, j] = parameters.IdrissaParameters.Idrissa[i, j + 3];
                    b[i, j+2] = parameters.IdrissaParameters.Idrissa[i, j + 11];
                }

                g[i] = parameters.IdrissaParameters.Idrissa[i, 6];
                y[i] = parameters.IdrissaParameters.Idrissa[i, 7];
                f[i] = parameters.IdrissaParameters.Idrissa[i, 8];
                k[i] = 1;
            }
            for (int i = 4; i <= 19; i++)
            {
                k[i] = 1.02;
            }

            if (parameters.TypeOfMovemant == 0)
            {
                F = 0;
            } else if (parameters.TypeOfMovemant == 2)
            {
                F = 0;
            } else if (parameters.TypeOfMovemant == 4)
            {
                F = 1;
            }
        }

        public ResponseSpectra CalculateBetta(double M, double R, double H)
        {
            ResponseSpectra responseSpectra = new ResponseSpectra(parameters.SaAndRsParameters.PeriodCountInSaMatrix, "I14");
            if (R < R_LIMIT)
            {
                int fm = M < 6.75 ? 0 : 1;

                double MM;

                if (M < 5)
                {
                    MM = 5;
                } else if (M >= 7.5)
                {
                    MM = 7.5;
                } else
                {
                    MM = M;
                }

                for (int i = 0; i < 31; i++)
                {
                    double TT;
                    if (i <= 2)
                    {
                        TT = 0.05;
                    } else if (i >= 20)
                    {
                        TT = 3;
                    } else
                    {
                        TT = Math.Pow(10, -3 + (i / 10.0));
                    }
                    double sigma = 1.18 + 0.035 * Math.Log(TT) - 0.06 * MM;
                    double deltaA = normRand.NextDouble() * sigma;
                    
                    double s1 = a[i, 1 + fm * 3] * k[i];
                    double s2 = a[i, 2 + fm * 3] * M;
                    double s3 = a[i, 3 + fm * 3] * (8.5 - M) * (8.5 - M);
                    double s4 = (b[i, 1 + fm * 2] + b[i, 2 + fm * 2] * M) * Math.Log(R + 10);
                    double s5 = g[i] * Math.Log(parameters.Vs);
                    double s6 = y[i] * R;
                    double s7 = f[i] * F;
                    double pow = s1 + s2 + s3 - s4 + s5 + s6 + s7 + deltaA;
                    responseSpectra.CurrentBettaResponseSpectra[1, i] = Math.Pow(Math.E, pow);

                }
                responseSpectra.Pga = responseSpectra.CurrentBettaResponseSpectra[1, 0];
                responseSpectra.IsCalculated = true;
                return responseSpectra;
            }

            return responseSpectra;
        }
    }
}
