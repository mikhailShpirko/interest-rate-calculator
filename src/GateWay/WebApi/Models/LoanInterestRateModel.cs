using System.Collections.Generic;

namespace WebApi.Models
{
    public class LoanInterestRateModel
    {
        public float TotalPayment { get; set; }
        public float TotalInterestPaid { get; set; }
        public float InterestRate { get; set; }
        public IEnumerable<MonthlyAmortizationModel> MonthlyAmortization { get; set; }
    }
}