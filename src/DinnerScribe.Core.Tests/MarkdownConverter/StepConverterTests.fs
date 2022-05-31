module DinnerScribe.Core.Tests.MarkdownConverter.StepConverterTests

open DinnerScribe.Core.Parser.StepParser
open NUnit.Framework
open DinnerScribe.Core.MarkdownConverter.Primitives
open FParsec

[<TestFixture>]
type StepConverterTests () = 
    [<Test>]
    [<TestCase("1. Crush Biscuits", "1. Crush Biscuits")>]
    [<TestCase("2. Eat", "2. Eat")>]
    member this.ValidStepReturnsValidMarkdown input expected =
        let result = run stepParser input
        match result with
        | Failure _ -> Assert.Fail ()
        | Success (step, _, _) ->
            let actual = convertStep step
            Assert.AreEqual(expected, actual)
            