using Grpc.Core;
using System.Threading.Tasks;
using static AmortizationCalculatorService.AmortizationCalculator;

namespace AmortizationCalculatorService.Services
{
    public class AmortizationCalculator : AmortizationCalculatorBase
    {
        public override Task<Amortization> Calculate(Loan request, ServerCallContext context)
        {
            return base.Calculate(request, context);
        }
    }
}
