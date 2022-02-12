namespace ServiceClients.Domain.DTO
{
    public class LoanDTO
    {
        public LoanDTO(float amount,
            float monthlyPayment,
            float termYears)
        {
            Amount = amount;
            MonthlyPayment = monthlyPayment;
            TermYears = termYears;
        }
        public float Amount { get; }
        public float MonthlyPayment { get; }
        public float TermYears { get; }
    }
}