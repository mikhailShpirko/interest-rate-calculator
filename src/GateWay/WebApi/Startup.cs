using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ServiceClients.Configuration.AmortizationCalculatorService;
using ServiceClients.Configuration.InterestRateCalculatorService;
using WebApi.Configuration.AmortizationCalculatorService;
using WebApi.Configuration.InterestRateCalculatorService;
using ServiceClients.AmortizationCalculator;
using ServiceClients.InterestRateCalculator;
using ServiceClients.Domain;
using Consul;
using System;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi GateWay", Version = "v1" });
            });

            services.Configure<InterestRateCalculatorServiceOptions>(
                    Configuration.GetSection(nameof(InterestRateCalculatorServiceOptions)));

            services.Configure<AmortizationCalculatorServiceOptions>(
                    Configuration.GetSection(nameof(AmortizationCalculatorServiceOptions)));

            if (Environment.IsDevelopment())
            {
                services.AddScoped<IInterestRateCalculatorServiceConfiguration,
                    InterestRateCalculatorServiceConfiguration>();

                services.AddScoped<IAmortizationCalculatorServiceConfiguration,
                    AmortizationCalculatorServiceConfiguration>();
            }
            else
            {
                services.AddScoped<IInterestRateCalculatorServiceConfiguration, 
                    InterestRateCalculatorServiceConfigurationFromEnvironment>();

                services.AddScoped<IAmortizationCalculatorServiceConfiguration,
                    AmortizationCalculatorServiceConfigurationFromEnvironment>();
            }

            services.AddScoped<IAmortizationCalculatorServiceClient,
                    AmortizationCalculatorServiceClient>();

            services.AddScoped<IInterestRateCalculatorServiceClient,
                    InterestRateCalculatorServiceClient>();

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();        
            }

            if (!env.IsDevelopment())
            {
                //consul registration
                var consulAddress =
                    System.Environment.GetEnvironmentVariable("CONSUL_ADDRESS");

                var consulClient = new ConsulClient(x =>
                x.Address = new Uri(consulAddress));//Consul address requesting registration

                var serviceName = "GateWay";
                // Register service with consul
                var registration = new AgentServiceRegistration()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = serviceName,
                    Address = "localhost",
                    Port = 8989,
                    Tags = new[] { $"urlprefix-/{serviceName}" }//Add a tag tag in the format of urlprefix-/servicename so that Fabio can recognize it
                };

                consulClient.Agent.ServiceRegister(registration).Wait();//Register when the service starts, the internal implementation is actually to register using the Consul API (initiated by HttpClient)
                lifetime.ApplicationStopping.Register(() =>
                {
                    consulClient.Agent.ServiceDeregister(registration.ID).Wait();//Unregister when the service stops
                });
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi GateWay v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
