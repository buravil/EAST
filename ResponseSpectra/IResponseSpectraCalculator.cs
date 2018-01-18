using System;
using System.Collections.Generic;
using System.Text;

namespace East_CSharp
{
    interface IResponseSpectraCalculator
    {
        ResponseSpectra CalculateBetta(double M, double R);
    }
}
