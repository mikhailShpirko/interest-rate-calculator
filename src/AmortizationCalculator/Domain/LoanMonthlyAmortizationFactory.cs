namespace Domain
{
    public class LoanMonthlyAmortizationFactory : ILoanMonthlyAmortizationFactory
    {
        public LoanMonthlyAmortization Create(float amount,
            float termYears,
            float monthlyPayment)
        {
            return new LoanMonthlyAmortization(amount, termYears, monthlyPayment);
        }
    }
}
