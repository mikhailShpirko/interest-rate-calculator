namespace InterestRateCalculatorService.Controllers

open Domain
open InterestRateCalculatorService.Models
open Microsoft.AspNetCore.Mvc

[<ApiController>]
[<Route("[controller]")>]
type LoanInterestRateCalculatorController (loanfactory : ILoanFactory) =
    inherit ControllerBase()
    let _loanfactory = loanfactory

    [<HttpGet>]
    member _.Get(amout : float, termYears : float, montlyPayment : float) =
        try
            let loan = _loanfactory.Create(amout, termYears, montlyPayment)
            Response.Success<Loan>(loan)
        with  
            | :? ValidationException as ex -> Response.Error<Loan>(ex.Message)
            | ex -> Response.Error<Loan>("Error during processing")