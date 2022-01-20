namespace Domain

open System

type Loan(amount : float, termYears : float, montlyPayment : float) = 
    do
        if (amount <= 0.) then raise (ValidationException "Loan Amount must be greater than 0")
        if (termYears <= 0.) then raise (ValidationException "Loan Term must be greater than 0")
        if (montlyPayment <= 0.) then raise (ValidationException "Monthly Payment must be greater than 0")
    member this.Amount = amount
    member this.TermYears = termYears 
    member this.MontlyPayment = montlyPayment 
    member this.NumberOfPaymentPeriods = this.TermYears * 12.0
    member this.TotalPayment = this.MontlyPayment * this.NumberOfPaymentPeriods
    member this.TotalInterestPaid = this.TotalPayment - this.Amount
    //Financial.Rate returns monthly interest rate. Must be covnerted to yearly
    //and rounded up to 5 decimal places
    member this.InterestRate = System.Math.Round(12.0 * Excel.FinancialFunctions.Financial.Rate(this.NumberOfPaymentPeriods, 
        -this.MontlyPayment, 
        this.Amount, 
        0.0, 
        Excel.FinancialFunctions.PaymentDue.EndOfPeriod, 
        0.000001), 5)
