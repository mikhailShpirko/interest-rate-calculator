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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();        
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
