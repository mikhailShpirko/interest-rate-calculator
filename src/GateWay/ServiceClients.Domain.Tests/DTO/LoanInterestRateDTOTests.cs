using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceClients.Domain.DTO;

namespace ServiceClients.Tests.DTO
{
    [TestClass]
    public class LoanInterestRateDTOTests
    {
        [TestMethod]
        public void Ctor_TestValues_ProperMappingToProperties()
        {
            var loanDto = new LoanInterestRateDTO(1, 2, 3);
            Assert.AreEqual(1, loanDto.TotalPayment, "TotalPayment is invalid");
            Assert.AreEqual(2, loanDto.TotalInterestPaid, "TotalInterestPaid is invalid");
            Assert.AreEqual(3, loanDto.InterestRate, "InterestRate is invalid");
        }
    }
}
