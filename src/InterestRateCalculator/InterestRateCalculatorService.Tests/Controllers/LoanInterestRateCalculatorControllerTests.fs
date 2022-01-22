namespace InterestRateCalculatorService.Tests.Controllers

open InterestRateCalculatorService.Controllers
open Microsoft.VisualStudio.TestTools.UnitTesting
open Domain
open System

[<TestClass>]
type LoanInterestRateCalculatorControllerTests () =
    let _loanFactoryMock = {
        new ILoanFactory with
            member this.Create(amount, termYear, monthlyPayment) = new Loan(amount, termYear, monthlyPayment) }
    
    let _controller = new LoanInterestRateCalculatorController(_loanFactoryMock)

    [<TestMethod>]
    member this.Get_ValidArguments_SuccessResponse () =       
        let response = _controller.Get(1.0, 2.0, 3.0)
        Assert.IsTrue(response.IsSuccess)

    [<TestMethod>]
    member this.Get_InvalidArguments_ErrorResponse () =
        let response = _controller.Get(0.0, 0.0, 0.0)
        Assert.IsFalse(response.IsSuccess)

    [<TestMethod>]
    member this.Get_MoqThatThrowsException_ErrorResponseWithCustomMessage () =
        let loanFactoryExceptionMock = {
            new ILoanFactory with
                member this.Create(amount, termYear, monthlyPayment) = raise (Exception "Testing exception handling") }
        let controller = new LoanInterestRateCalculatorController(loanFactoryExceptionMock)
        let response = controller.Get(1.0, 2.0, 3.0)
        Assert.IsFalse(response.IsSuccess)
        Assert.AreEqual("Error during processing", response.ErrorMessage)
     