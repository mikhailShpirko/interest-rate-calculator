using Microsoft.Extensions.Options;
using ServiceClients.Configuration;

namespace WebApi.Configuration
{
    public abstract class ServiceConfiguration
        : IServiceConfiguration
    {
        private readonly string _hostAddress;
        public string HostAddress => _hostAddress;

        protected ServiceConfiguration(IOptions<ServiceOptions> serviceOptions)
        {
            _hostAddress = serviceOptions.Value.HostAddress;
        }
    }
}
