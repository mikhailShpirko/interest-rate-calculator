using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceClients.Domain;
using ServiceClients.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Mapping;
using WebApi.Models;

namespace WebApi.Tests.Controllers
{
    [TestClass]
    public class LoanInterestRateControllerTests
    {
        private readonly Mock<IAmortizationCalculatorServiceClient> _amortizationCalculatorMock;
        private readonly Mock<IInterestRateCalculatorServiceClient> _interestRateCalculatorMock;
        private readonly IMapper _mapper;

        public LoanInterestRateControllerTests()
        {
            _amortizationCalculatorMock = new Mock<IAmortizationCalculatorServiceClient>();
            _interestRateCalculatorMock = new Mock<IInterestRateCalculatorServiceClient>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new LoanInterestRateProfile());
                mc.AddProfile(new LoanProfile());
                mc.AddProfile(new MonthlyAmortizationProfile());
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [TestMethod]
        public async Task CalculateAsync_MockServicesReturnSuccess_LoanInterestRateModelAnd200StatusCode()
        {
            _amortizationCalculatorMock
                .Setup(x => x.CalculateMonthlyAmortizationAsync(It.IsAny<LoanDTO>()))
                .Returns(Task.FromResult(
                    Result.Ok(Enumerable.Empty<MonthlyAmortizationDTO>())));

            _interestRateCalculatorMock
                .Setup(x => x.CalculateLoanInterestRateAsync(It.IsAny<LoanDTO>()))
                .Returns(Task.FromResult(
                    Result.Ok(new LoanInterestRateDTO(1,2,3))));

            var controller = new LoanInterestRateController(
                _amortizationCalculatorMock.Object,
                _interestRateCalculatorMock.Object,
                _mapper);

            var result = await controller.CalculateAsync(new LoanModel());

            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.IsInstanceOfType(objectResult.Value, typeof(LoanInterestRateModel));
        }

        [TestMethod]
        public async Task CalculateAsync_MockAmortizationServicesReturnSuccess_BadRequestModelAnd400StatusCode()
        {
            _amortizationCalculatorMock
                .Setup(x => x.CalculateMonthlyAmortizationAsync(It.IsAny<LoanDTO>()))
                .Returns(Task.FromResult(
                    Result.Ok(Enumerable.Empty<MonthlyAmortizationDTO>())));

            _interestRateCalculatorMock
                .Setup(x => x.CalculateLoanInterestRateAsync(It.IsAny<LoanDTO>()))
                .Returns(Task.FromResult(
                    Result.Fail<LoanInterestRateDTO>("Error")));

            var controller = new LoanInterestRateController(
                _amortizationCalculatorMock.Object,
                _interestRateCalculatorMock.Object,
                _mapper);

            var result = await controller.CalculateAsync(new LoanModel());

            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(400, objectResult.StatusCode);
            Assert.IsInstanceOfType(objectResult.Value, typeof(BadRequestModel));
        }

        [TestMethod]
        public async Task CalculateAsync_MockInterestRateServicesReturnSuccess_BadRequestModelAnd400StatusCode()
        {
            _amortizationCalculatorMock
                .Setup(x => x.CalculateMonthlyAmortizationAsync(It.IsAny<LoanDTO>()))
                .Returns(Task.FromResult(
                    Result.Fail<IEnumerable<MonthlyAmortizationDTO>>("Error")));

            _interestRateCalculatorMock
                .Setup(x => x.CalculateLoanInterestRateAsync(It.IsAny<LoanDTO>()))
                .Returns(Task.FromResult(
                    Result.Ok(new LoanInterestRateDTO(1, 2, 3))));

            var controller = new LoanInterestRateController(
                _amortizationCalculatorMock.Object,
                _interestRateCalculatorMock.Object,
                _mapper);

            var result = await controller.CalculateAsync(new LoanModel());

            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(400, objectResult.StatusCode);
            Assert.IsInstanceOfType(objectResult.Value, typeof(BadRequestModel));
        }

        [TestMethod]
        public async Task CalculateAsync_MockAmortizationServiceThrowsError_InternalServerErrorModelAnd500ErrorCode()
        {
            _amortizationCalculatorMock
                .Setup(x => x.CalculateMonthlyAmortizationAsync(It.IsAny<LoanDTO>()))
                .Throws(new Exception());

            _interestRateCalculatorMock
                .Setup(x => x.CalculateLoanInterestRateAsync(It.IsAny<LoanDTO>()))
                .Returns(Task.FromResult(
                    Result.Ok(new LoanInterestRateDTO(1, 2, 3))));

            var controller = new LoanInterestRateController(
                _amortizationCalculatorMock.Object,
                _interestRateCalculatorMock.Object,
                _mapper);

            var result = await controller.CalculateAsync(new LoanModel());

            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.IsInstanceOfType(objectResult.Value, typeof(InternalServerErrorModel));
        }

        [TestMethod]
        public async Task CalculateAsync_MockInterestRateServiceThrowsError_InternalServerErrorModelAnd500ErrorCode()
        {
            _amortizationCalculatorMock
                .Setup(x => x.CalculateMonthlyAmortizationAsync(It.IsAny<LoanDTO>()))
                .Returns(Task.FromResult(
                    Result.Ok(Enumerable.Empty<MonthlyAmortizationDTO>())));
                

            _interestRateCalculatorMock
                .Setup(x => x.CalculateLoanInterestRateAsync(It.IsAny<LoanDTO>()))
                .Throws(new Exception());

            var controller = new LoanInterestRateController(
                _amortizationCalculatorMock.Object,
                _interestRateCalculatorMock.Object,
                _mapper);

            var result = await controller.CalculateAsync(new LoanModel());

            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.IsInstanceOfType(objectResult.Value, typeof(InternalServerErrorModel));
        }
    }
}
