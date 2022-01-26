namespace Domain
{
    public class MonthlyAmortization
    {
        public readonly int MonthNumber;
        public readonly float Payment;
        public readonly float Balance;

        public MonthlyAmortization(int monthNumber,
            float payment,
            float balance)
        {
            MonthNumber = monthNumber;
            Payment = payment;
            Balance = balance;
        }
    }
}
