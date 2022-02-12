using ServiceClients.Configuration.InterestRateCalculatorService;
using System;

namespace WebApi.Configuration.InterestRateCalculatorService
{
    public class InterestRateCalculatorServiceConfigurationFromEnvironment
        : IInterestRateCalculatorServiceConfiguration
    {
        public string HostAddress => 
            Environment.GetEnvironmentVariable("INTEREST_RATE_CALCULATOR_SERVICE_ADDRESS");
    }
}
