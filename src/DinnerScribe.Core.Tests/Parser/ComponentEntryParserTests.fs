module DinnerScribe.Core.Tests.Parser.ComponentEntryParserTests

open DinnerScribe.Core.Parser.ComponentEntryParser
open DinnerScribe.Core.RecipeModel.Types
open FParsec
open NUnit.Framework

[<TestFixture>]
type ComponentEntryParserTests () =
    
    [<Test>]
    [<TestCase("\t+ 300g Chicken Thigh")>]
    [<TestCase("\t1. Stand upright")>]
    [<TestCase("\t1. Stand upright\r\n")>]
    [<TestCase("\t1. Stand upright\n")>]
    [<TestCase("\t+ 300g Chicken Thigh\r\n")>]
    [<TestCase("\t+ 300g Chicken Thigh\n")>]
    [<TestCase(" \t+ 300g Chicken Thigh\n")>]
    member this.ValidInputGivesValidComponentEntry input =
        let result = run componentEntryParser input
        match result with
        | Success (result,_,_) ->
            match result with
            | Ingredient _ -> Assert.Pass()
            | Step _ -> Assert.Pass()
        | Failure _ -> Assert.Fail ()
        
    [<Test>]
    [<TestCase("")>]
    [<TestCase("   ")>]
    [<TestCase("\t  ")>]
    [<TestCase("\t  JIBBERISH")>]
    [<TestCase("+ 300g Chicken Thigh")>]
    [<TestCase("+ 300g Chicken Thigh")>]
    [<TestCase("1. Stand upright")>]
    member this.InvalidInputGivesFailureResult input =
        let result = run componentEntryParser input
        match result with
        | Failure _ -> Assert.Pass ()
        | _ -> Assert.Fail ()