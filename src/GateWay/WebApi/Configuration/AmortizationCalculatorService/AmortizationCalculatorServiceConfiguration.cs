using Microsoft.Extensions.Options;
using ServiceClients.Configuration.AmortizationCalculatorService;

namespace WebApi.Configuration.AmortizationCalculatorService
{
    public class AmortizationCalculatorServiceConfiguration
        : ServiceConfiguration, IAmortizationCalculatorServiceConfiguration
    {
        public AmortizationCalculatorServiceConfiguration(IOptions<AmortizationCalculatorServiceOptions> options)
            : base(options)
        {
        }
    }
}
