namespace East_CSharp
{
    class ResponseSpectraCalculatorFactory
    {
        private EquationParameters parameters;

        internal EquationParameters Parameters { get => parameters; set => parameters = value; }

        public IResponseSpectraCalculator getResponseSpectraCalculator(string type)
        {
            switch (type)
            {
                case "Chilean2017":
                    return new ChileanResponseSpectraCalculator(parameters);
                case "Graizer":
                    return new GraizerKalkanResponseSpectraCalculator(parameters);
                case "SIS17":
                    return new AptikaevResponseSpectraCalculator(parameters);
                default:
                    return new AptikaevResponseSpectraCalculator(parameters);
            }
            
        }
    }
}
