namespace ServiceClients.Domain.DTO
{
    public class MonthlyAmortizationDTO
    {
        public MonthlyAmortizationDTO(int monthNumber,
            float payment,
            float balance)
        {
            MonthNumber = monthNumber;
            Payment = payment;
            Balance = balance;
        }

        public int MonthNumber { get; private set; }
        public float Payment { get; private set; }
        public float Balance { get; private set; }
    }
}
