namespace Domain
{
    public interface ILoanMonthlyAmortizationFactory
    {
        LoanMonthlyAmortization Create(float amount, float termYears, float monthlyPayment);
    }
}