module DinnerScribe.Core.Tests.Parser.MassParserTests


open DinnerScribe.Core.RecipeModel.Types
open DinnerScribe.Core.Parser.MassParser
open FParsec
open NUnit.Framework

[<TestFixture>]
type MassParserTests () =
    
    [<Test>]
    [<TestCase("300g", MassUnit.Grams, 300u)>]
    [<TestCase("2kg", MassUnit.Kilograms, 2u)>]
    [<TestCase("30CG", MassUnit.Centigrams, 30u)>]
    member this.GivenValidInputReturnsValidMass input expectedUnit expectedMass =
        let result = run massParser input
        match result with
        | Success (value, _, _) when value.Amount = expectedMass && value.Unit = expectedUnit -> Assert.Pass ()
        | _ -> Assert.Fail ()
        
        
    [<Test>]
    [<TestCase("300")>]
    [<TestCase("")>]
    [<TestCase("300 g")>]
    member this.GivenInvalidInputReturnsFailure input =
        let result = run massParser input
        match result with
        | Failure _ -> Assert.Pass ()
        | _ -> Assert.Fail ()