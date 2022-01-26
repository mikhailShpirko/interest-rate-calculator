using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests
{
    [TestClass]
    public class MonthlyAmortizationTests
    {
        private const int _monthNumber = 1;
        private const float _payment = 2f;
        private const float _balance = 3f;
        private readonly MonthlyAmortization _amortization = new MonthlyAmortization(_monthNumber,
            _payment,
            _balance);

        [TestMethod]
        public void Ctor_MonthNumber_PropertyMappedProperly()
        {
            Assert.AreEqual(_monthNumber, _amortization.MonthNumber);
        }

        [TestMethod]
        public void Ctor_Payment_PropertyMappedProperly()
        {
            Assert.AreEqual(_payment, _amortization.Payment);
        }

        [TestMethod]
        public void Ctor_Balance_PropertyMappedProperly()
        {
            Assert.AreEqual(_balance, _amortization.Balance);
        }
    }
}
