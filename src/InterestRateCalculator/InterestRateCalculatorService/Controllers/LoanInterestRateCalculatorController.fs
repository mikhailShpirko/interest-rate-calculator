namespace InterestRateCalculatorService.Controllers

open Domain
open System
open InterestRateCalculatorService.Models
open Microsoft.AspNetCore.Mvc

[<ApiController>]
[<Route("[controller]")>]
type LoanInterestRateCalculatorController () =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get(amout : float, termYears : float, montlyPayment : float) =
        try
            let loan = new Loan(amout, termYears, montlyPayment)
            Response.Success<float>(loan.InterestRate)
        with  
            | :? ValidationException as ex -> Response.Error<float>(ex.Message)
            | ex -> Response.Error<float>("Error during processing" )