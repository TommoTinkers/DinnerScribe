module DinnerScribe.Core.Tests.Parser.StepParserTests

open FParsec
open DinnerScribe.Core.Parser.StepParser
open DinnerScribe.Core.RecipeModel.Types

open NUnit.Framework

[<TestFixture>]
type StepParserTests () =
    
    [<Test>]
    [<TestCase("1. Stand upright", 1u, "Stand upright")>]
    [<TestCase("2. Sit down again", 2u, "Sit down again")>]
    [<TestCase("562.Turn around", 562u, "Turn around")>]
    member this.ValidInputReturnsValidStep input expectedNum expectedDirection =
        let result = run StepParser input
        match result with
        | Success (value, _, _) when value.Number = expectedNum && value.Direction = expectedDirection -> Assert.Pass ()
        | _ -> Assert.Fail ()
        
