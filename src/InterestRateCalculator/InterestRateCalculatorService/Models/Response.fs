namespace InterestRateCalculatorService.Models

open System

type Response<'T> private (value : 'T, isSuccess : bool, errorMessage : string) = 
    member this.IsSuccess = isSuccess
    member this.Value = value
    member this.ErrorMessage = errorMessage

    static member Success<'T>(value : 'T) = Response<'T>(value, true, null)
    static member Error<'T>(errorMessage : string) = Response<'T>(Unchecked.defaultof<'T>, false, errorMessage)
    

