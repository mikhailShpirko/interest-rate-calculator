namespace InterestRateCalculatorService.Tests.Controllers

open InterestRateCalculatorService.Controllers
open Microsoft.VisualStudio.TestTools.UnitTesting
open Domain
open System
open Serilog

[<TestClass>]
type LoanInterestRateCalculatorControllerTests () =
    let _loanFactoryMock = {
        new ILoanFactory with
            member this.Create(amount, termYear, monthlyPayment) = new Loan(amount, termYear, monthlyPayment) }    
    let _loggerMock = 
        LoggerConfiguration()
            .Enrich
            .FromLogContext()
            .Enrich
            .WithMachineName()
            .WriteTo
            .Console()
            .CreateLogger()

    let _controller = new LoanInterestRateCalculatorController(_loanFactoryMock, _loggerMock)

    [<TestMethod>]
    member this.Get_ValidArguments_SuccessResponse () =       
        let response = _controller.Get(1.0, 2.0, 3.0)
        Assert.IsTrue(response.IsSuccess)
        Assert.IsNotNull(response.Value)

    [<TestMethod>]
    member this.Get_InvalidArguments_ErrorResponse () =
        let response = _controller.Get(0.0, 0.0, 0.0)
        Assert.IsFalse(response.IsSuccess)
        Assert.IsNull(response.Value)

    [<TestMethod>]
    member this.Get_MoqThatThrowsException_ErrorResponseWithCustomMessage () =
        let loanFactoryExceptionMock = {
            new ILoanFactory with
                member this.Create(amount, termYear, monthlyPayment) = raise (Exception "Testing exception handling") }
        let controller = new LoanInterestRateCalculatorController(loanFactoryExceptionMock, _loggerMock)
        let response = controller.Get(1.0, 2.0, 3.0)
        Assert.IsFalse(response.IsSuccess)
        Assert.IsNull(response.Value)
        Assert.AreEqual("Error during processing", response.ErrorMessage)
     