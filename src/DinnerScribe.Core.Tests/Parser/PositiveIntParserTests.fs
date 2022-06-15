module DinnerScribe.Core.Tests.Parser.UnsignedIntParserTests

open DinnerScribe.Core.Parser.HelperParsers

open NUnit.Framework
open FParsec

[<TestFixture>]
type PositiveIntParserTests () =
    [<Test>]
    [<TestCase("1", 1u)>]
    [<TestCase("34", 34u)>]
    
    member this.ValidInputReturnsValidInt input expected =
        let result = run positiveIntParser input
        match result with
        | Success (value, _, _) when value = expected -> Assert.Pass ()
        | _ -> Assert.Fail ()
        
    [<Test>]
    [<TestCase("0")>]
    [<TestCase("0099")>]
    [<TestCase("00 99")>]
    [<TestCase(" 99")>]
    [<TestCase("-99")>]
    member this.InvalidInputReturnsError input =
        let result = run positiveIntParser input
        match result with
        | Failure _ -> Assert.Pass ()
        | _ -> Assert.Fail ()
        
