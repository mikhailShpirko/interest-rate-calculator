namespace InterestRateCalculatorService

open System
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Serilog
open Serilog.Sinks.Elasticsearch

module Program =
    let exitCode = 0

    let configureLogger (configuration : LoggerConfiguration) = 
        let elasticSearchLogsEndpointAddress = Environment.GetEnvironmentVariable("ELASTIC_SEARCH_LOGS_ENDPOINT_ADDRESS") 
        configuration
            .Enrich
            .FromLogContext()
            .Enrich
            .WithMachineName()
            .WriteTo
            .Console() |> ignore
    
        if not (String.IsNullOrWhiteSpace(elasticSearchLogsEndpointAddress)) then                  
            let elasticSearchSinkOptions = new ElasticsearchSinkOptions(new Uri(elasticSearchLogsEndpointAddress))
            elasticSearchSinkOptions.IndexFormat <- $"InterestRateCalculator-Logs-{DateTime.UtcNow:``yyyy-MM``}"
            elasticSearchSinkOptions.AutoRegisterTemplate <- true
            elasticSearchSinkOptions.NumberOfShards <- 2
            elasticSearchSinkOptions.NumberOfReplicas <- 1
    
            configuration
                .WriteTo
                .Elasticsearch(elasticSearchSinkOptions) |> ignore

    let CreateHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .UseSerilog(fun context configuration ->
                configureLogger(configuration) 
            )
            .ConfigureWebHostDefaults(fun webBuilder ->
                webBuilder.UseStartup<Startup>() |> ignore
            )

    [<EntryPoint>]
    let main args =
        CreateHostBuilder(args).Build().Run()

        exitCode
