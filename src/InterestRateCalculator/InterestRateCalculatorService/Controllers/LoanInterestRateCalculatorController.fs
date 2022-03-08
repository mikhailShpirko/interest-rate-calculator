namespace InterestRateCalculatorService.Controllers

open Domain
open InterestRateCalculatorService.Models
open Microsoft.AspNetCore.Mvc
open Serilog

[<ApiController>]
[<Route("[controller]")>]
type LoanInterestRateCalculatorController (loanfactory : ILoanFactory, logger : ILogger) =
    inherit ControllerBase()
    let _loanfactory = loanfactory
    let _logger = logger

    [<HttpGet>]
    member _.Get(amout : float, termYears : float, montlyPayment : float) =
        let requestInfoLog = $"{nameof(amout)}: {amout}, {nameof(termYears)}: {termYears}, {nameof(montlyPayment)}: {montlyPayment}"
        try
            _logger.Information("Calculating interest rate for loan {loan}", requestInfoLog)
            let loan = _loanfactory.Create(amout, termYears, montlyPayment)
            Response.Success<Loan>(loan)
        with  
            | :? ValidationException as ex -> Response.Error<Loan>(ex.Message)
            | ex -> 
                _logger.Error(ex, "Error interest rate for loan {loan}", requestInfoLog)
                Response.Error<Loan>("Error during processing")