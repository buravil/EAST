using System;
using System.Collections.Generic;
using System.Text;

namespace East_CSharp
{
    class EquationParameters
    {
        private AptikaevParameters aptikaevParameters;
        private SaAndRsParameters saAndRsParameters;
        

        internal AptikaevParameters AptikaevParameters { get => aptikaevParameters; set => aptikaevParameters = value; }
        internal SaAndRsParameters SaAndRsParameters { get => saAndRsParameters; set => saAndRsParameters = value; }
    }
}
