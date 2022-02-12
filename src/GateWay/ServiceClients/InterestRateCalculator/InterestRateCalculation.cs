using Newtonsoft.Json;

namespace ServiceClients.InterestRateCalculator
{
    public class InterestRateCalculation
    {
        [JsonProperty]
        public bool IsSuccess { get; private set; }

        [JsonProperty]
        public LoanInterestRate Value { get; private set; }

        [JsonProperty]
        public string ErrorMessage { get; private set; }
    }
}
