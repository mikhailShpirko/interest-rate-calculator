namespace Domain.Tests

open Microsoft.VisualStudio.TestTools.UnitTesting
open Domain

[<TestClass>]
type LoanFactoryTests () =

    [<TestMethod>]
    member this.Create_ProperArguments_DoesNotThrowAnyException () =
        let factory = new LoanFactory()
        (factory.Create(1.0, 2.0, 3.0))
        |> ignore