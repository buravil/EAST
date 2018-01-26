using System;
using System.Collections.Generic;
using System.Text;

namespace East_CSharp
{
    class GraizerKalkanResponseSpectraCalculator : IResponseSpectraCalculator
    {
        private EquationParameters parameters;
        private const int R_LIMIT = 300;
        private const int V_S = 350;
        private NormalRandom normRand = new NormalRandom();

        public GraizerKalkanResponseSpectraCalculator(EquationParameters parameters)
        {
            this.parameters = parameters;
        }

        public ResponseSpectra CalculateBetta(double M, double R, double H)
        {
            ResponseSpectra responseSpectra = new ResponseSpectra(parameters.SaAndRsParameters.PeriodCountInSaMatrix, "Graizer");
            double F = 1;
            switch (parameters.GraizerKalkanParameters.TipPodvizhki)
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
            double D1 = parameters.GraizerKalkanParameters.Z < 1 ? 0.65 : 0.35;

            if (R < R_LIMIT)
            {
                double deltaA = normRand.NextDouble() * 0.552;
                double mu = -0.0012 * R + -0.4087 * M + 0.0006 * V_S + 3.63;
            }


                throw new NotImplementedException();
        }
    }
}
