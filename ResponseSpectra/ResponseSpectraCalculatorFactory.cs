namespace East_CSharp
{
    class ResponseSpectraCalculatorFactory
    {
        private EquationParameters parameters;

        internal EquationParameters Parameters { get => parameters; set => parameters = value; }

        public IResponseSpectraCalculator getResponseSpectraCalculator(int type)
        {
            switch (type)
            {
                default:
                    return new AptikaevResponseSpectraCalculator(Parameters.AptikaevParameters, parameters.SaAndRsParameters);
            }
            
        }
    }
}
