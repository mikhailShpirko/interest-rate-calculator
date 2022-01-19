namespace Domain

type Loan(amount : float, termYears : float, montlyPayment : float) =    
    member this.Amount = amount
    member this.TermYears = termYears
    member this.MontlyPayment = montlyPayment
    member this.NumberOfPaymentPeriods = termYears * 12.0
    member this.TotalPayment = montlyPayment * this.NumberOfPaymentPeriods
    member this.TotalInterestPaid = this.TotalPayment - this.Amount
    //Financial.Rate returns monthly interest rate. Must be covnerted to yearly
    //and rounded up to 5 decimal places
    member this.InterestRate = System.Math.Round(12.0 * Excel.FinancialFunctions.Financial.Rate(this.NumberOfPaymentPeriods, 
        -this.MontlyPayment, 
        this.Amount, 
        0.0, 
        Excel.FinancialFunctions.PaymentDue.EndOfPeriod, 
        0.000001), 5)
