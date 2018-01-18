using System;
using System.Collections.Generic;
using System.Text;

namespace East_CSharp
{
    class SaAndRsParameters
    {
        //Количество периодов в единичном спектре реакций
        private int periondCountInOneRS;
        private double stepLgD;
        //Количество периодов в матрице SA
        private int periodCountInSaMatrix = 33;
        //Количество значений PGA в матрице SA
        private int pgaCountInSaMatrix = 62;

        public SaAndRsParameters(int periodCountInSaMatrix, int pgaCountInSaMatrix)
        {
            this.StepLgD = 0.1;
            this.PeriondCountInOneRS = Convert.ToInt32(Math.Round(2 * Math.Pow(StepLgD, -1)));
            this.PeriodCountInSaMatrix = periodCountInSaMatrix;
            this.PgaCountInSaMatrix = pgaCountInSaMatrix;
        }

        public int PeriondCountInOneRS { get => periondCountInOneRS; set => periondCountInOneRS = value; }
        public double StepLgD { get => stepLgD; set => stepLgD = value; }
        public int PeriodCountInSaMatrix { get => periodCountInSaMatrix; set => periodCountInSaMatrix = value; }
        public int PgaCountInSaMatrix { get => pgaCountInSaMatrix; set => pgaCountInSaMatrix = value; }
    }
}
