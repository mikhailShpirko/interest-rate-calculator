namespace WebApi.Models
{
    public class LoanModel
    {
        public float Amount { get; set; }
        public float TermYears { get; set; }
        public float MonthlyPayment { get; set; }

        public override string ToString()
        {
            return $"{nameof(Amount)}: {Amount}, {nameof(TermYears)}: {TermYears}, {nameof(MonthlyPayment)}: {MonthlyPayment}";
        }
    }
}