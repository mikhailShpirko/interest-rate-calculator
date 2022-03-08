using Grpc.Core;
using System.Threading.Tasks;
using System;
using Domain;
using System.Linq;
using Serilog;

namespace AmortizationCalculatorService.Endpoints
{
    public class AmortizationCalculatorEndpoint : AmortizationCalculator.AmortizationCalculatorBase
    {
        private readonly ILoanMonthlyAmortizationFactory _loanMonthlyAmortizationFactory;
        private readonly ILogger _logger;

        public AmortizationCalculatorEndpoint(ILoanMonthlyAmortizationFactory loanMonthlyAmortizationFactory,
            ILogger logger)
        {
            _loanMonthlyAmortizationFactory = loanMonthlyAmortizationFactory;
            _logger = logger;
        }

        public override async Task<MonthlyAmortizationResponse> CalculateMonthlyAmortization(Loan loan, 
            ServerCallContext context)
        {
            try
            {
                _logger.Information("Calculating amortization for loan {Loan}", loan);

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
                _logger.Error(ex, "Error calculating amortization for loan {loan}", loan);
                return new MonthlyAmortizationResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
