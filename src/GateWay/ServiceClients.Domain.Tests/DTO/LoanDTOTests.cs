using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceClients.Domain.DTO;

namespace ServiceClients.Tests.DTO
{
    [TestClass]
    public class LoanDTOTests
    {
        [TestMethod]
        public void Ctor_TestValues_ProperMappingToProperties()
        {
            var loanDto = new LoanDTO(1, 2, 3);
            Assert.AreEqual(1, loanDto.Amount, "Amount is invalid");
            Assert.AreEqual(2, loanDto.MonthlyPayment, "MonthlyPayment is invalid");
            Assert.AreEqual(3, loanDto.TermYears, "TermYears is invalid");
        }
    }
}
