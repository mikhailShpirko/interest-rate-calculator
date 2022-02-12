using Newtonsoft.Json;

namespace ServiceClients.InterestRateCalculator
{
    public class LoanInterestRate
    {
        [JsonProperty]
        public float Amount { get; private set; }

        [JsonProperty]
        public float TermYears { get; private set; }

        [JsonProperty]
        public float MontlyPayment { get; private set; }

        [JsonProperty]
        public float NumberOfPaymentPeriods { get; private set; }

        [JsonProperty]
        public float TotalPayment { get; private set; }

        [JsonProperty]
        public float TotalInterestPaid { get; private set; }

        [JsonProperty]
        public float InterestRate { get; private set; }
    }
}
