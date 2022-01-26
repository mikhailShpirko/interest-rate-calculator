using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Domain.Tests
{
    [TestClass]
    public class LoanMonthlyAmortizationTests
    {
        private const float _amount = 2000f;
        private const float _termYears = 3f;
        private const float _monthlyPayment = 600f;
        private readonly LoanMonthlyAmortization _monthlyAmortization = new LoanMonthlyAmortization(_amount,
            _termYears,
            _monthlyPayment);

        [TestMethod]
        public void Ctor_Amortization_ProperNumberOfRecordsGenerated()
        {            
            Assert.AreEqual(_termYears * 12 + 1, _monthlyAmortization.Count());
        }

        [TestMethod]
        public void Ctor_Amortization_FirstRecordHas0MonthNumberAndPaymentAndBalanceEqualsToAmount()
        {
            var initialAmortization = _monthlyAmortization.First();
            Assert.AreEqual(0, initialAmortization.MonthNumber, "Invalid month number");
            Assert.AreEqual(0, initialAmortization.Payment, "Invalid payment");
            Assert.AreEqual(_amount, initialAmortization.Balance, "Invalid balance");
        }

        [TestMethod]
        public void Ctor_Amortization_LastRecordHasProperMonthNumberAndTotalPayment()
        {
            var totalNumberOfMonths = Math.Ceiling(_termYears * 12);
            var lastAmortization = _monthlyAmortization.Last();
            Assert.AreEqual(totalNumberOfMonths, lastAmortization.MonthNumber, "Invalid month number");
            Assert.AreEqual(totalNumberOfMonths * _monthlyPayment, lastAmortization.Payment, "Invalid payment");
            Assert.IsTrue(lastAmortization.Balance <= 0, "Invalid balance");
        }

        [TestMethod]
        public void Ctor_Amortization_MonthNumberAndPaymentIncrementsAndBalanceDecrementsThroughCollection()
        {
            var recordsWhereMonthNumberNotIncrements = Enumerable.Range(1, _monthlyAmortization.Count() - 1)
                  .Where(i => _monthlyAmortization.ElementAt(i).MonthNumber <= _monthlyAmortization.ElementAt(i - 1).MonthNumber)
                  .ToList();

            var recordsWherePaymentNotIncrements = Enumerable.Range(1, _monthlyAmortization.Count() - 1)
                  .Where(i => _monthlyAmortization.ElementAt(i).Payment <= _monthlyAmortization.ElementAt(i - 1).Payment)
                  .ToList();

            var recordsWhereBalanceNotDecrements = Enumerable.Range(1, _monthlyAmortization.Count() - 1)
                  .Where(i => _monthlyAmortization.ElementAt(i).Balance >= _monthlyAmortization.ElementAt(i - 1).Balance)
                  .ToList();

            Assert.IsFalse(recordsWhereMonthNumberNotIncrements.Any(), "Month numbers do not increment");
            Assert.IsFalse(recordsWherePaymentNotIncrements.Any(), "Payment do not all increment");
            Assert.IsFalse(recordsWhereBalanceNotDecrements.Any(), "Payment do not decrement");
        }
    }
}
