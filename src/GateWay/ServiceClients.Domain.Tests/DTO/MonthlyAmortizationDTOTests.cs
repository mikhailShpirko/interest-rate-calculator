using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceClients.Domain.DTO;

namespace ServiceClients.Tests.DTO
{
    [TestClass]
    public class MonthlyAmortizationDTOTests
    {
        [TestMethod]
        public void Ctor_TestValues_ProperMappingToProperties()
        {
            var loanDto = new MonthlyAmortizationDTO(1, 2, 3);
            Assert.AreEqual(1, loanDto.MonthNumber, "MonthNumber is invalid");
            Assert.AreEqual(2, loanDto.Payment, "Payment is invalid");
            Assert.AreEqual(3, loanDto.Balance, "Balance is invalid");
        }
    }
}
