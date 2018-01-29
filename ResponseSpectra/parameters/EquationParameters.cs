using System;
using System.Collections.Generic;
using System.Text;

namespace East_CSharp
{
    class EquationParameters
    {
        public AptikaevParameters AptikaevParameters { get; set; }
        public SaAndRsParameters SaAndRsParameters { get; set; }
        public ChileanParameters ChileanParameters { get; set; }

        public double Vs { get; set; }
        public double Vref { get; set; }
        public double T30 { get; set; }
        public int TypeOfMovemant { get; set; }
        public double Z { get; set; }
    }
}
