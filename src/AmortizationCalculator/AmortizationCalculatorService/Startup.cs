using AmortizationCalculatorService.Endpoints;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace AmortizationCalculatorService
{
    public class Startup
    {      
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services
                .AddGrpcHealthChecks()
                .AddCheck("HealthCheck", () => HealthCheckResult.Healthy());
            services.AddScoped<ILoanMonthlyAmortizationFactory, LoanMonthlyAmortizationFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<AmortizationCalculatorEndpoint>();
                endpoints.MapGrpcHealthChecksService();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });         
            });
        }
    }
}
