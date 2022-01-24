namespace Domain

type ILoanFactory =
    abstract member Create: amount : float * termYears : float * montlyPayment : float -> Loan

type LoanFactory() = 
    member this.Create(amount, termYears, montlyPayment) = (this :> ILoanFactory).Create(amount, termYears, montlyPayment)
    interface ILoanFactory with
        member this.Create(amount, termYears, montlyPayment) = new Loan(amount, termYears, montlyPayment)