module DinnerScribe.Core.Tests.Parser.ComponentParserTests

open FParsec
open NUnit.Framework

open DinnerScribe.Core.Parser.ComponentParser

[<TestFixture>]
type ComponentParserTests () =
    [<Test>]
    [<TestCase("Component: Chicken Stock\n\t1. Put Chicken Bones in pressure cooker\n\t2. Cook for 90 minutes on full pressure\n\t+ 1kg Chicken bones")>]
    [<TestCase("component: Chicken Stock\n\t1. Put Chicken Bones in pressure cooker\n\t2. Cook for 90 minutes on full pressure\n\t+ 1kg Chicken bones")>]
    [<TestCase("component: Cheese on toast\n\t1. Make Toast\n\t2. Put Cheese on the toast\n\t+ 120g Bread\n\t+ 120g Cheese\n\t+ 400kg No self respect")>]
    member this.GivenValidInputReturnsAComponent input =
        match run componentParser input with
        | Success _ -> Assert.Pass ()
        | Failure _ -> Assert.Fail ()
         