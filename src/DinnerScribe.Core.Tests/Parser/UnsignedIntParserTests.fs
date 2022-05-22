module DinnerScribe.Core.Tests.Parser.UnsignedIntParserTests

open DinnerScribe.Core.Parser.HelperParsers

open NUnit.Framework
open FParsec

[<TestFixture>]
type UnsignedIntParserTests () =
    [<Test>]
    [<TestCase("1", 1u)>]
    [<TestCase("34", 34u)>]
    [<TestCase("0099", 99u)>]
    member this.ValidInputReturnsValidInt input expected =
        let result = run unsignedIntParser input
        match result with
        | Success (value, _, _) when value = expected -> Assert.Pass ()
        | _ -> Assert.Fail ()
        
