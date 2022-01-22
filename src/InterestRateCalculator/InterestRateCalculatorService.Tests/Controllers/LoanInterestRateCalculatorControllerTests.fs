namespace InterestRateCalculatorService.Tests.Controllers

open InterestRateCalculatorService.Controllers
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type LoanInterestRateCalculatorControllerTests () =
    let _controller = new LoanInterestRateCalculatorController()

    [<TestMethod>]
    member this.Get_ValidArguments_SuccessResponse () =
        let response = _controller.Get(1.0, 2.0, 3.0)
        Assert.IsTrue(response.IsSuccess)

    [<TestMethod>]
    member this.Get_InvalidArguments_ErrorResponse () =
        let response = _controller.Get(0.0, 0.0, 0.0)
        Assert.IsFalse(response.IsSuccess)
