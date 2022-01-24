namespace InterestRateCalculatorService.Tests.Models

open InterestRateCalculatorService.Models
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type ResponseTests () =

    [<TestMethod>]
    member this.Success_IntValue_ProperlyMappedToMemebers () =
        let expectedResponseValue = 1
        let successResponse = Response.Success<int>(expectedResponseValue)
        Assert.IsTrue(successResponse.IsSuccess)
        Assert.AreEqual(expectedResponseValue, successResponse.Value)
        Assert.IsNull(successResponse.ErrorMessage)

    [<TestMethod>]
    member this.Error_ErrorMessage_ProperlyMappedToMemebers () =
        let expectedResponseErrorMessage = "Some error message"
        let errorResponse = Response.Error<int>(expectedResponseErrorMessage)
        Assert.IsFalse(errorResponse.IsSuccess)
        Assert.AreEqual(expectedResponseErrorMessage, errorResponse.ErrorMessage)
        Assert.AreEqual(Unchecked.defaultof<int>, errorResponse.Value)
