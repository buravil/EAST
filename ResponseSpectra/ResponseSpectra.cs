using System;
using System.Collections.Generic;
using System.Text;

namespace East_CSharp
{
    class ResponseSpectra
    {
        private double[,] currentBettaResponseSpectra;
        private bool isCalculated;
        private double pga;
        private double duration;


        public ResponseSpectra(SaAndRsParameters saAndRsParameters)
        {
            this.isCalculated = false;
            this.CurrentBettaResponseSpectra = new double[saAndRsParameters.PeriondCountInOneRS + 1, 2];
        }

        public double[,] CurrentBettaResponseSpectra { get => currentBettaResponseSpectra; set => currentBettaResponseSpectra = value; }
        public bool IsCalculated { get => isCalculated; set => isCalculated = value; }
        public double Pga { get => pga; set => pga = value; }
        public double Duration { get => duration; set => duration = value; }

    }
}
