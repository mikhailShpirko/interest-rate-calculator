namespace InterestRateCalculatorService.Models

type Response<'T> private (value : 'T, isSuccess : bool, errorMessage : string) = 
    member this.IsSuccess = isSuccess
    member this.Value = value
    member this.ErrorMessage = errorMessage

    static member Success<'a>(value : 'a) = Response<'a>(value, true, null)
    static member Error<'a>(errorMessage : string) = Response<'a>(Unchecked.defaultof<'a>, false, errorMessage)
    

