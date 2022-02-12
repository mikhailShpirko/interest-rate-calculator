using Microsoft.Extensions.Options;
using ServiceClients.Configuration.InterestRateCalculatorService;

namespace WebApi.Configuration.InterestRateCalculatorService
{
    public class InterestRateCalculatorServiceConfiguration 
        : ServiceConfiguration, IInterestRateCalculatorServiceConfiguration
    {
        public InterestRateCalculatorServiceConfiguration(IOptions<InterestRateCalculatorServiceOptions> options)
            : base(options)
        {
        }
    }
}
