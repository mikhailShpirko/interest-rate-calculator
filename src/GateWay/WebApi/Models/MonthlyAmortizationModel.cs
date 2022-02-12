namespace WebApi.Models
{
    public class MonthlyAmortizationModel
    {
        public int MonthNumber { get; set; }
        public float Payment { get; set; }
        public float Balance { get; set; }
    }
}
