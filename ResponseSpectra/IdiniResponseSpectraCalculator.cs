using System;
using System.Collections.Generic;
using System.Text;

namespace East_CSharp
{
    class IdiniResponseSpectraCalculator : IResponseSpectraCalculator
    {
        public EquationParameters EqParam { get; set; }

        private int jsa = 30;
        private int isa = 60;
        private int i1 = 6;
        private int i2 = 9;
        private int i3 = 3;
        private double[,] STx;
        private double[,] C;
        private double[,] DC;
        private double[,] q;

        public IdiniResponseSpectraCalculator(EquationParameters parameters)
        {
            EqParam = parameters;
            STx = new double[jsa+1, i1+1];
            C = new double[jsa+1, i2+1];
            DC = new double[jsa+1, i3+1];
            q = new double[jsa+1, i3+1];

            for (int i = 0; i <= jsa; i++)
            {
                for (int j = 2; j <= i1; j++)
                {
                    STx[i, j] = EqParam.IdinParameters.Chilean[i, j];
                }
            }

            for (int i = 0; i <= jsa; i++)
            {
                for (int j = 1; j <= i2; j++)
                {
                    C[i, j] = EqParam.IdinParameters.Chilean[i, j + 6];
                }
            }

            for (int i = 0; i <= jsa; i++)
            {
                for (int j = 1; j <= i3; j++)
                {
                    DC[i, j] = EqParam.IdinParameters.Chilean[i, j + 15];
                }
            }

            for (int i = 0; i <= jsa; i++)
            {
                for (int j = 1; j <= i3; j++)
                {
                    q[i, j] = EqParam.IdinParameters.Chilean[i, j + 18];
                }
            }

        }

        public ResponseSpectra CalculateBetta(double M, double R)
        {

            double lg_D = EqParam.SaAndRsParameters.StepLgD;
            int h0 = 50;
            int Mr = 5;
            int Feve = 0;


            int ST;
            double T30 = EqParam.IdinParameters.T30;
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

            double Rlim = Feve == 0 ? 100 * M + 500 : 66.429 * M + 197.86;

            for (int i = 0; i < jsa; i++)
            {

            }

            throw new NotImplementedException();
        }
    }
}
