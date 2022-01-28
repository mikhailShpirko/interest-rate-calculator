using Grpc.Core;
using System.Threading.Tasks;
using System;
using Domain;
using System.Linq;

namespace AmortizationCalculatorService.Endpoints
{
    public class AmortizationCalculatorEndpoint : AmortizationCalculator.AmortizationCalculatorBase
    {
        private readonly ILoanMonthlyAmortizationFactory _loanMonthlyAmortizationFactory;

        public AmortizationCalculatorEndpoint(ILoanMonthlyAmortizationFactory loanMonthlyAmortizationFactory)
        {
            _loanMonthlyAmortizationFactory = loanMonthlyAmortizationFactory;
        }

        public override async Task<MonthlyAmortizationResponse> CalculateMonthlyAmortization(Loan loan, 
            ServerCallContext context)
        {
            try
            {
                var loanMonthlyAmortization = _loanMonthlyAmortizationFactory.Create(loan.Amount,
                    loan.TermYears,
                    loan.MonthlyPayment);

                var response = new MonthlyAmortizationResponse
                {
                    IsSuccess = true
                };

                response.Data
                    .AddRange(loanMonthlyAmortization
                        .Select(m => new MonthlyAmortization
                        {
                            MonthNumber = m.MonthNumber,
                            Payment = m.Payment,
                            Balance = m.Balance
                        }));

                return response;
            }
            catch (Exception ex)
            {
                return new MonthlyAmortizationResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
