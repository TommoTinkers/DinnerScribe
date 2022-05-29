module DinnerScribe.Core.Tests.Parser.ComponentEntryListParserTests

open FParsec
open DinnerScribe.Core.Parser.ComponentEntryParser
open NUnit.Framework

[<TestFixture>]
type ComponentEntryListParserTests () =
    
    [<Test>]
    [<TestCase("\t1. Stand upright\n\t+ 300g Chicken Thigh", 2)>]
    [<TestCase("\t+ 300g Chicken Thigh", 1)>]
    member this.GivenManyComponentsReturnsAList input expectedCount =
        let result = run componentEntryListParser input
        match result with
        | Success (value, _, _) when List.length value = expectedCount -> Assert.Pass ()
        | _ -> Assert.Fail ()
        
    [<Test>]
    [<TestCase("\t1. Stand upright\n\n\t+ 300g Chicken Thigh", 2)>]
    [<TestCase("\n\t1. Stand upright\n\n\t+ 300g Chicken Thigh", 2)>]
    
    member this.GivenManyComponentsWithBlankLinesReturnsAList input expectedCount =
        let result = run componentEntryListParser input
        match result with
        | Success (value, _, _) when List.length value = expectedCount -> Assert.Pass ()
        | _ -> Assert.Fail ()

    [<Test>]
    member this.GivenEmptyStringReturnsFailure () =
        let result = run componentEntryListParser ""
        match result with
        | Failure _ -> Assert.Pass ()
        | _ -> Assert.Fail ()

    [<Test>]
    [<TestCase("\t1. Stand upright\n\t+ 300g Chicken Thigh\nLOL")>]
    member this.GivenManyComponentsFollowedByInvalidInputReturnsAListOfComponents input =
        let result = run componentEntryListParser input
        match result with
        | Success _ -> Assert.Pass ()
        | _ -> Assert.Fail ()
        
    [<Test>]
    [<TestCase("LOL")>]
    [<TestCase("I am invalid input\n\t+ 300g Chicken Thigh")>]
    member this.GivenInvalidInputReturnsAnError input =
        let result = run componentEntryListParser input
        match result with
        | Failure _ -> Assert.Pass ()
        | _ -> Assert.Fail ()        