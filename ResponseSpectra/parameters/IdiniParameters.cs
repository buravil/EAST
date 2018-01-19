using System;
using System.Collections.Generic;
using System.Text;

namespace East_CSharp
{
    class IdiniParameters
    {
        private double[,] chilean = new double[31 , 23];

        private double[] periods;
        private double[] sII;
        private double[] sIII;
        private double[] sIV;
        private double[] sV;
        private double[] sVI;
        private double[] c1;
        private double[] c2;
        private double[] c3;
        private double[] c4;
        private double[] c5;
        private double[] c6;
        private double[] c7;
        private double[] c8;
        private double[] c9;
        private double[] dc1;
        private double[] dc2;
        private double[] dc3;
        private double[] qe;
        private double[] qt;
        private double[] qr;

        public double Vs { get; set; }
        public double Vref { get; set; }
        public double T30 { get; set; }

        public IdiniParameters(int size)
        {
            this.periods = new double[size];
            this.sII = new double[size];
            this.sIII = new double[size];
            this.sIV = new double[size];
            this.sV = new double[size];
            this.sVI = new double[size];
            this.c1 = new double[size];
            this.c2 = new double[size];
            this.c3 = new double[size];
            this.c4 = new double[size];
            this.c5 = new double[size];
            this.c6 = new double[size];
            this.c7 = new double[size];
            this.c8 = new double[size];
            this.c9 = new double[size];
            this.dc1 = new double[size]; 
            this.dc2 = new double[size];
            this.dc3 = new double[size];
            this.qe = new double[size];
            this.qt = new double[size];
            this.qr = new double[size];
        }

        public double[] Periods { get => periods; set => periods = value; }
        public double[] SII { get => sII; set => sII = value; }
        public double[] SIII { get => sIII; set => sIII = value; }
        public double[] SIV { get => sIV; set => sIV = value; }
        public double[] SV { get => sV; set => sV = value; }
        public double[] SVI { get => sVI; set => sVI = value; }
        public double[] C1 { get => c1; set => c1 = value; }
        public double[] C2 { get => c2; set => c2 = value; }
        public double[] C3 { get => c3; set => c3 = value; }
        public double[] C4 { get => c4; set => c4 = value; }
        public double[] C5 { get => c5; set => c5 = value; }
        public double[] C6 { get => c6; set => c6 = value; }
        public double[] C7 { get => c7; set => c7 = value; }
        public double[] C8 { get => c8; set => c8 = value; }
        public double[] C9 { get => c9; set => c9 = value; }
        public double[] Dc1 { get => dc1; set => dc1 = value; }
        public double[] Dc2 { get => dc2; set => dc2 = value; }
        public double[] Dc3 { get => dc3; set => dc3 = value; }
        public double[] Qe { get => qe; set => qe = value; }
        public double[] Qt { get => qt; set => qt = value; }
        public double[] Qr { get => qr; set => qr = value; }
        public double[,] Chilean { get => chilean; set => chilean = value; }
    }
}
