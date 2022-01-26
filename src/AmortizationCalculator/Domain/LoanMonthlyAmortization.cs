using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain
{
    public class LoanMonthlyAmortization : IEnumerable<MonthlyAmortization>
    {
        private readonly ReadOnlyCollection<MonthlyAmortization> _amortization;

        public LoanMonthlyAmortization(float amount,
            float termYears,
            float monthlyPayment)
        {
            var amortization = new List<MonthlyAmortization>();

            //should generate record for each month + initial record for 0 month
            for (int i = 0; i <= Math.Ceiling(12 * termYears); i++)
            {
                var totalPaymentForPeriod = monthlyPayment * i;
                amortization.Add(new MonthlyAmortization(i,
                        totalPaymentForPeriod,
                        amount - totalPaymentForPeriod));
            }

            _amortization = amortization.AsReadOnly();
        }

        public IEnumerator<MonthlyAmortization> GetEnumerator()
        {
            return _amortization.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
