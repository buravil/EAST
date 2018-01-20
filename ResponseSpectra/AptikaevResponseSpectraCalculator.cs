using System;
using System.Collections.Generic;
using System.Text;

namespace East_CSharp
{
    class AptikaevResponseSpectraCalculator : IResponseSpectraCalculator
    {
        private NormalRandom normRand = new NormalRandom();
        public EquationParameters EqParam { get; set; }

        public AptikaevResponseSpectraCalculator(EquationParameters equationParameters)
        {
            EqParam = equationParameters;
        }

        public ResponseSpectra CalculateBetta(double M, double R, double H)
        {
            double deltaA, deltaB, deltaT, deltaD;

            double pga = 0;
            //          int isa, jsa, ipga;
            //       bool IsCalculated = false;
            //     double logInIsa, doubleIsa, doubleIpga;

            ResponseSpectra responseSpectra = new ResponseSpectra(EqParam.SaAndRsParameters.PeriondCountInOneRS, "SIS17"); ;

            if (R < 1.0807 * Math.Pow(Math.E, 0.976 * M))
            {

                deltaA = normRand.NextDouble() * 0.18;
                deltaB = normRand.NextDouble() * 0.07;
                deltaT = normRand.NextDouble() * 0.2;
                deltaD = normRand.NextDouble() * 0.3;

                pga = PGAcalculation(M, R) + deltaA;
                if (pga > 0.01)
                {
                    responseSpectra = BettaCalculation(responseSpectra, M, R, deltaB, deltaT, deltaD);
                    responseSpectra.Pga = pga;
                    responseSpectra.IsCalculated = true;
                }
            }
            return responseSpectra;
        }

        //Расчет PGA
        private double PGAcalculation(double M, double R)
        {
            //Расчет PGA в зависимости от условия
            if (R < Math.Pow(10, 0.33 * M - 1.51))
            {
                return EqParam.AptikaevParameters.C6 * (0.33 * M - 0.61 - Math.Log10(Math.Pow(10, 0.33 * M - 1.51)));
            }
            else if (R >= Math.Pow(10, 0.33 * M - 1.51) && R <= Math.Pow(10, 0.33 * M - 0.61 + EqParam.AptikaevParameters.C7 * 0.8))
            {
                return (EqParam.AptikaevParameters.C6 * (0.33 * M - 0.61 - Math.Log10(R))) + 2.23;
            }
            else if (R > Math.Pow(10, 0.33 * M - 0.61 + EqParam.AptikaevParameters.C7 * 0.8))
            {
                return 0.634 * M - 1.92 * Math.Log10(R) + 1.076 + EqParam.AptikaevParameters.C7;
            }
            else
                return 0;

            //сделать проверку на отрицательную величину
        }

        //Расчет кривой бэтта с определенной магнитудой и расстоянием
        private ResponseSpectra BettaCalculation(ResponseSpectra responseSpectra, double M, double R, double deltaB, double deltaT, double deltaD)
        {
            
            //случайные величины
            double tg_alfa;
            double Lgf1, Lgf2;
            double i0;
            double B2;

            //Массив вспомогательных значений
            double[] lg_B = new double[EqParam.SaAndRsParameters.PeriondCountInOneRS + 1];

            //Массив выходных значений
            double[,] AA = AA = new double[EqParam.SaAndRsParameters.PeriondCountInOneRS + 1, 2];

            //S-ширина спектра
            double S = SDTBcalculation(M, R)[0];

            //Расчитываем длительность
            double duration = Math.Pow(10, (SDTBcalculation(M, R)[1] + deltaD));
            responseSpectra.Duration = duration;
            //T-период
            double T = SDTBcalculation(M, R)[2] + deltaT;
            T = (T <= 0) ? 0.01 : T;

            //Максимальное значение бетты
            double Bmax = Math.Pow(10, SDTBcalculation(M, R)[3] + deltaB);

            double LogBmax = Math.Log10(Bmax);

            tg_alfa = Math.Log10(0.5 * Bmax) / (S * 0.5);
            Lgf1 = LogBmax * Math.Pow(tg_alfa, -1);
            Lgf2 = -(0.8 * LogBmax) * Math.Pow(tg_alfa, -1);

            i0 = Math.Round((Lgf1 + 1) * Math.Pow(EqParam.SaAndRsParameters.StepLgD, -1));
            B2 = LogBmax - 2 * tg_alfa * Lgf2 - 0.8 * LogBmax;

            //Формирование спектра реакций в зависимости от условия
            for (int i = 0; i <= EqParam.SaAndRsParameters.PeriondCountInOneRS; i++)
            {
                double ii = -1 + EqParam.SaAndRsParameters.StepLgD * i;

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

                AA[EqParam.SaAndRsParameters.PeriondCountInOneRS - i, 1] = Math.Pow(10, lg_B[i]);
                AA[EqParam.SaAndRsParameters.PeriondCountInOneRS - i, 0] = T * Math.Pow(10, 1 - EqParam.SaAndRsParameters.StepLgD * i);
            }
            responseSpectra.CurrentBettaResponseSpectra = AA;
            return responseSpectra;
        }

        //Расчет значений: s-ширина спектра, d-длительность, t-период, b-бетта и запись значений в массив sdtb[]
        private double[] SDTBcalculation(double M, double R)
        {
            double h;
            double[] sdtb = new double[4];
            sdtb[0] = 0.6 + EqParam.AptikaevParameters.C8 + EqParam.AptikaevParameters.C1;
            //sdtb[1] = Math.Pow(10, 0.15 * M + 0.5 * Math.Log10(R) + C3 + C4 - 1.3);
            sdtb[1] = 0.15 * M + 0.5 * Math.Log10(R) + EqParam.AptikaevParameters.C3 + EqParam.AptikaevParameters.C4 - 1.3;
            h = (R < Math.Pow(10, 0.33 * M - 1.51)) ? Math.Pow(10, 0.33 * M - 1.51) : R;
            sdtb[2] = Math.Pow(10, Math.Round((0.15 * M + 0.25 * Math.Log10(h) - 1.9
                + EqParam.AptikaevParameters.C5) * Math.Pow(EqParam.SaAndRsParameters.StepLgD, -1), 0) * EqParam.SaAndRsParameters.StepLgD);
            sdtb[3] = 0.72 - 0.28 * sdtb[0] + 0.07 * sdtb[1];

            return sdtb;
        }
    }
}
