module DinnerScribe.Core.Tests.MarkdownConverter.StepListConverterTests

open DinnerScribe.Core.Parser
open FParsec
open NUnit.Framework
open NUnit.Framework.Internal

[<TestFixture>]
type StepListConverterTests () =
    [<Test>]
    [<TestCase("1. Open Pack\n2. Pour pack into bowl\n3. Pour milk into bowl\n4. Eat ","1. Open Pack\n2. Pour pack into bowl\n3. Pour milk into bowl\n4. Eat \n")>]
    member this.GivenListOfStepsReturnsCorrectMarkdown input expected =
        let parser = many StepParser.stepParser
        let result = run parser input
        match result with
        | Success (stepList, _, _) ->
            let convertedMarkdown = DinnerScribe.Core.MarkdownConverter.Primitives.convertStepList stepList
            Assert.AreEqual(expected, convertedMarkdown)
        | _ -> Assert.Fail ()
        
        
    [<Test>]
    [<TestCase("2. Open Pack\n1. Pour pack into bowl", "1. Pour pack into bowl\n2. Open Pack\n")>]
    member this.GivenValidInputReturnsMarkdownWithSortedSteps input expected =
        let parser = many StepParser.stepParser
        let result = run parser input
        match result with
        | Success (stepList, _, _) ->
            let convertedMarkdown = DinnerScribe.Core.MarkdownConverter.Primitives.convertStepList stepList
            Assert.AreEqual(expected, convertedMarkdown)
        | _ -> Assert.Fail ()
        
                