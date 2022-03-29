using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;

namespace AmortizationCalculatorService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, configuration) =>
                {
                    var elasticSearchLogsEndpointAddress = Environment
                        .GetEnvironmentVariable("ELASTIC_SEARCH_LOGS_ENDPOINT_ADDRESS");

                    configuration
                        .Enrich
                        .FromLogContext()
                        .Enrich
                        .WithMachineName()
                        .WriteTo
                        .Console();

                    if (!string.IsNullOrWhiteSpace(elasticSearchLogsEndpointAddress))
                    {
                        var elasticSearchSinkOptions =
                            new ElasticsearchSinkOptions(new Uri(elasticSearchLogsEndpointAddress))
                            {
                                IndexFormat = $"AmortizationCalculator-Logs-{DateTime.UtcNow:yyyy-MM}",
                                AutoRegisterTemplate = true,
                                NumberOfShards = 2,
                                NumberOfReplicas = 1
                            };

                        configuration
                            .WriteTo
                            .Elasticsearch(elasticSearchSinkOptions);
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
