using AmortizationCalculatorService.Endpoints;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace AmortizationCalculatorService.Tests.Services
{
    [TestClass]
    public class AmortizationCalculatorEndpointTests
    {
        [TestMethod]
        public void CalculateMonthlyAmortization_ValidCalculationFactory_SuccessResponse()
        {
            var loanMonthlyAmortizationFactory = new Mock<ILoanMonthlyAmortizationFactory>();
            loanMonthlyAmortizationFactory
                .Setup(f =>
                    f.Create(It.IsAny<float>(), It.IsAny<float>(), It.IsAny<float>()))
                .Returns(new LoanMonthlyAmortization(1f, 2f, 3f));

            var serviceEndpoint = new AmortizationCalculatorEndpoint(loanMonthlyAmortizationFactory.Object);
            var response = serviceEndpoint.CalculateMonthlyAmortization(
                new Loan
                {
                    Amount = 1f,
                    TermYears = 2f,
                    MonthlyPayment = 3f
                }, 
                null)
                .GetAwaiter()
                .GetResult();

            Assert.IsTrue(response.IsSuccess, "Must be successful response");
            Assert.IsTrue(string.IsNullOrWhiteSpace(response.ErrorMessage), "Response ErrorMessage must be empty");
            Assert.IsNotNull(response.Data, "Response Data must not be null");
            Assert.IsTrue(response.Data.Count > 0, "Response Data must contain at least one item");
        }


        [TestMethod]
        public void CalculateMonthlyAmortization_CalculationFactoryThatThrowsException_ErrorResponse()
        {
            var loanMonthlyAmortizationFactory = new Mock<ILoanMonthlyAmortizationFactory>();
            loanMonthlyAmortizationFactory
                .Setup(f =>
                    f.Create(It.IsAny<float>(), It.IsAny<float>(), It.IsAny<float>()))
                .Throws(new Exception("Test error"));

            var serviceEndpoint = new AmortizationCalculatorEndpoint(loanMonthlyAmortizationFactory.Object);
            var response = serviceEndpoint.CalculateMonthlyAmortization(
                new Loan
                {
                    Amount = 1f,
                    TermYears = 2f,
                    MonthlyPayment = 3f
                },
                null)
                .GetAwaiter()
                .GetResult();

            Assert.IsFalse(response.IsSuccess, "Must be error response");
            Assert.IsFalse(string.IsNullOrWhiteSpace(response.ErrorMessage), "Response ErrorMessage must not be empty");
            Assert.IsTrue(response.Data.Count == 0, "Response Data must not contain any items");
        }
    }
}
