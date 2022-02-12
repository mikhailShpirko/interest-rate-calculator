namespace ServiceClients.Domain.DTO
{
    public class LoanInterestRateDTO
    {
        public LoanInterestRateDTO(float totalPayment,
            float totalInterestPaid,
            float interestRate)
        {
            TotalPayment = totalPayment;
            TotalInterestPaid = totalInterestPaid;
            InterestRate = interestRate;
        }

        public float TotalPayment { get; private set; }

        public float TotalInterestPaid { get; private set; }

        public float InterestRate { get; private set; }
    }
}
