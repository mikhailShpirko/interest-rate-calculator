using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests
{
    [TestClass]
    public class LoanMonthlyAmortizationFactoryTests
    {
        [TestMethod]
        public void Create_AnyArguments_CreatesInstanceOfLoanMonthlyAmortization()
        {
            var monthlyAMortization = new LoanMonthlyAmortizationFactory()
                .Create(1f, 2f, 3f);

            Assert.IsNotNull(monthlyAMortization);
        }
    }
}
