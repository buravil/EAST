using System;
using System.Collections.Generic;
using System.Text;

namespace East_CSharp
{
    class ChileanParameters
    {
        private double[,] chilean = new double[31 , 23];

        public double Vs { get; set; }
        public double Vref { get; set; }
        public double T30 { get; set; }
        public int Feve { get; set; }

        public ChileanParameters()
        {
        }
        public double[,] Chilean { get => chilean; set => chilean = value; }
    }
}
