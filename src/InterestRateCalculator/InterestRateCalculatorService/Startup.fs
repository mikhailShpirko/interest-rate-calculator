namespace InterestRateCalculatorService

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy;
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Swashbuckle.AspNetCore.Swagger
open Microsoft.OpenApi.Models

type Startup(configuration: IConfiguration) =
    member _.Configuration = configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member _.ConfigureServices(services: IServiceCollection) =
        let info = OpenApiInfo()
        info.Title <- "Loan Interest Rate Calculator V1"
        info.Version <- "v1"
        services.AddSwaggerGen(fun config -> config.SwaggerDoc("v1", info)) |> ignore
        // Add framework services.
        services.AddControllers() |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member _.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore
        app.UseHttpsRedirection()
           .UseSwagger()
           .UseSwaggerUI(fun config -> config.SwaggerEndpoint("/swagger/v1/swagger.json", "Loan Interest Rate Calculator V1"))
           .UseRouting()
           .UseAuthorization()
           .UseEndpoints(fun endpoints ->
                endpoints.MapControllers() |> ignore
            ) |> ignore
