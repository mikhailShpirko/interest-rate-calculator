namespace Domain.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Domain

[<TestClass>]
type LoanTests () =
    let _amount = 20000.0
    let _termYears = 3.0
    let _monthlyPayment = 600.0
    let _loan = new Loan(_amount, _termYears, _monthlyPayment)

    [<TestMethod>]
    member this.Ctor_Amount_ProperlyMappedToMemeber () =
        let loanAmount = _loan.Amount
        Assert.AreEqual(_amount, loanAmount);
    
    [<TestMethod>]
    member this.Ctor_TermYears_ProperlyMappedToMemeber () =
        let loanTermYears = _loan.TermYears
        Assert.AreEqual(_termYears, loanTermYears);
      
    [<TestMethod>]
    member this.Ctor_MonthlyPayment_ProperlyMappedToMemeber () =
        let loanMontlyPayment = _loan.MontlyPayment
        Assert.AreEqual(_monthlyPayment, loanMontlyPayment);

    [<TestMethod>]
    member this.Ctor_NumberOfPaymentPeriods_ProperlyMappedToMemeber () =
        let loanNumberOfPaymentPeriods = _loan.NumberOfPaymentPeriods
        Assert.AreEqual(_termYears * 12.0, loanNumberOfPaymentPeriods);

    [<TestMethod>]
    member this.Ctor_TotalPayment_ProperlyCalculated () =
        let loanTotalPayment = _loan.TotalPayment
        Assert.AreEqual(_monthlyPayment * _termYears * 12.0, loanTotalPayment);

    [<TestMethod>]
    member this.Ctor_TotalInterestPaid_ProperlyCalculated () =
        let loanTotalPayment = _loan.TotalInterestPaid
        Assert.AreEqual(_monthlyPayment * _termYears * 12.0 - _amount, loanTotalPayment);

    [<TestMethod>]
    member this.Ctor_InterestRate_ProperlyCalculated () =
        let loanInterestRate = _loan.InterestRate
        Assert.AreEqual(0.05065, loanInterestRate);
