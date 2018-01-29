using System;
using System.Collections.Generic;
using System.Text;

namespace East_CSharp
{
    class GraizerKalkanResponseSpectraCalculator : IResponseSpectraCalculator
    {
        private EquationParameters parameters;
        private const int R_LIMIT = 300;
        private const int V_S30 = 350;
        private NormalRandom normRand = new NormalRandom();

        public GraizerKalkanResponseSpectraCalculator(EquationParameters parameters)
        {
            this.parameters = parameters;
        }

        public ResponseSpectra CalculateBetta(double M, double R, double H)
        {
            ResponseSpectra responseSpectra = new ResponseSpectra(parameters.SaAndRsParameters.PeriodCountInSaMatrix, "GK2008");
            double F = 1;
            switch (parameters.TypeOfMovemant)
            {
                case 0:
                    F = 1.0;
                    break;
                case 2:
                    F = 1.0;
                    break;
                case 4:
                    F = 1.28;
                    break;
                default:
                    break;
            }
            double D1 = parameters.Z < 1 ? 0.65 : 0.35;

            if (R < R_LIMIT)
            {
                double deltaA = normRand.NextDouble() * 0.552;
                double mu = -0.0012 * R + -0.4087 * M + 0.0006 * V_S30 + 3.63;
                double I = (0.017 * M + 1.27) * Math.Pow(Math.E, 0.0001 * R);
                double S = 0.001 * R - (0.077 * M + 0.3251);
                double Tsp = 0.0022 * R + 0.63 * M - 0.0005 * V_S30 + -2.1;
                double D0 = -0.125 * Math.Cos(1.19 * M - 6.15) + 0.525;
                double A = 0.14 * Math.Atan(M - 6.25) + 0.37;
                double firstLog = Math.Log(Math.Pow(1 - (R / (2.237 * M - 7.542)), 2) + 4 * (D0 * D0) * (R / (2.237 * M - 7.542)));
                double secondLog = Math.Log((1 - Math.Sqrt(0.01 * R)) * (1 - Math.Sqrt(0.01 * R)) + 4 * D1 * D1 * Math.Sqrt(0.01 * R));
                double pow = Math.Log(A) - 0.5 * firstLog - 0.5 *secondLog - 0.24 * Math.Log(V_S30/484.5) + deltaA;                
                double PGA = Math.Pow(Math.E, pow);
                responseSpectra.Pga = PGA;

                for (int i = 0; i <= 30; i++)
                {
                    double first = I * Math.Pow(Math.E, -0.5 * Math.Pow((Math.Log(Math.Pow(10, -1.5 + (i / 10.0))) + mu) / S, 2.0));
                    double second = Math.Pow(1.0 - Math.Pow(Math.Pow(10.0, -1.5 + (i / 10.0)) / Tsp, 1.5), 2);
                    double third = 4 * Math.Pow(0.75,2) * Math.Pow(Math.Pow(10, (-1.5 + (i / 10.0)) )/ Tsp, 1.5);
                    double pow2 = Math.Pow(second + third, -0.5);
                    responseSpectra.CurrentBettaResponseSpectra[1, i] = PGA * (first + pow2);
                }
                responseSpectra.IsCalculated = true;
                return responseSpectra;
            }

            return responseSpectra;
        }
    }
}
