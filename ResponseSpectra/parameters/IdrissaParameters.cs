using System;
using System.Collections.Generic;
using System.Text;

namespace East_CSharp
{

    class IdrissaParameters
    {

        private double[,] idrissa = new double[32, 14];

        public IdrissaParameters()
        {
        }

        public double[,] Idrissa { get => idrissa; set => idrissa = value; }
    }
}
