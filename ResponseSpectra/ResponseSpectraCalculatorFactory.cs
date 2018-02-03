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
                case "GK2008":
                    return new GraizerKalkanResponseSpectraCalculator(parameters);
                case "SIS17":
                    return new AptikaevResponseSpectraCalculator(parameters);
                case "I14":
                    return new IdrisaResponseSpectraCalculator(parameters);
                default:
                    throw new System.Exception("Заданный тип затухания - " + type + " не найден");
            }
            
        }
    }
}
