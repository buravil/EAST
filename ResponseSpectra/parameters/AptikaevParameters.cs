
namespace East_CSharp
{
    class AptikaevParameters
    {
        //Массив с коэффициентами типа грунта и кинематики
        private double[] C2array = new double[] { -0.1, 0, 0.1 };
        private double[] C1array = new double[] { -0.05, -0.025, 0, 0.025, 0.05 };
        private double[] C3array = new double[] { -0.15, 0, 0.45 };
        private double[] C4array = new double[] { -0.25, -0.12, 0, 0.12, 0.25 };
        private double[] C5array = new double[] { -0.1, -0.05, 0, 0.05, 0.1 };
        private double[] C6array = new double[] { 0.800, 0.717, 0.633, 0.550, 0.467 };
        private double[] C7array = new double[] { -0.15, 0, 0.15 };
        private double[] C8array = new double[] { -0.05, 0, 0.2 };

        //Коэффициенты
        private double c1, c2, c3, c4, c5, c6, c7, c8;

        private int kinematika;
        private int typeOfGrunt;

        public AptikaevParameters(int kinematika, int typeOfGrunt)
        {
            this.Kinematika = kinematika;
            this.TypeOfGrunt = typeOfGrunt;
            this.C2 = C2array[typeOfGrunt];
            this.C3 = C3array[typeOfGrunt];
            this.C7 = C7array[typeOfGrunt];
            this.C8 = C8array[typeOfGrunt];

            this.C1 = C1array[kinematika];
            this.C4 = C4array[kinematika];
            this.C5 = C5array[kinematika];
            this.C6 = C6array[kinematika];
        }

        public double C1 { get => c1; set => c1 = value; }
        public double C2 { get => c2; set => c2 = value; }
        public double C3 { get => c3; set => c3 = value; }
        public double C4 { get => c4; set => c4 = value; }
        public double C5 { get => c5; set => c5 = value; }
        public double C6 { get => c6; set => c6 = value; }
        public double C7 { get => c7; set => c7 = value; }
        public double C8 { get => c8; set => c8 = value; }
        public int Kinematika { get => kinematika; set => kinematika = value; }
        public int TypeOfGrunt { get => typeOfGrunt; set => typeOfGrunt = value; }
    }
}
