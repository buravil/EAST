using System;
using System.Collections.Generic;
using System.Text;

namespace East_CSharp
{
    class ChileanParameters
    {
        private double[,] chilean = new double[31 , 23];
        public int Feve { get; set; }

        public ChileanParameters()
        {
        }
        public double[,] Chilean { get => chilean; set => chilean = value; }
    }
}
