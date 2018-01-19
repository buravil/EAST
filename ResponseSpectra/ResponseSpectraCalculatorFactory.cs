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
                case 1:
                    return new IdiniResponseSpectraCalculator(parameters);
                default:
                    return new AptikaevResponseSpectraCalculator(parameters);
            }
            
        }
    }
}
