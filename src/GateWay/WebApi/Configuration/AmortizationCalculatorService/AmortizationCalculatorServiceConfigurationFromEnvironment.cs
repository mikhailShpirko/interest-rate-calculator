using ServiceClients.Configuration.AmortizationCalculatorService;
using System;

namespace WebApi.Configuration.AmortizationCalculatorService
{
    public class AmortizationCalculatorServiceConfigurationFromEnvironment
        : IAmortizationCalculatorServiceConfiguration
    {
        public string HostAddress => 
            Environment.GetEnvironmentVariable("AMORTIZATION_CALCULATOR_SERVICE_ADDRESS");
    }
}
