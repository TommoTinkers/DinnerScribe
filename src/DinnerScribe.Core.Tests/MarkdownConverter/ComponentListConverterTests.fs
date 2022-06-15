module DinnerScribe.Core.Tests.MarkdownConverter.ComponentListConverterTests

open DinnerScribe.Core.Parser.ComponentEntryParser
open FParsec
open NUnit.Framework

[<TestFixture>]
type ComponentListConverterTests () =
    [<Test>]
    [<TestCase("1. Add sugar and butter to the bowl\n+ 250g Caster Sugar\n2. Mix on high setting for 5 minutes\n+ 250g Butter\n4. Sift flour into a separate bowl\n+ 250g Self Raising Flour\n3. Turn mixer on to low speed and add eggs one at a time.\n+ 4 Eggs\n+ 300g Cocoa Powder",
               "#### Ingredients\n- 300g Cocoa Powder\n- 250g Butter\n- 250g Caster Sugar\n- 250g Self Raising Flour\n- 4 Eggs\n\n#### Method\n1. Add sugar and butter to the bowl\n2. Mix on high setting for 5 minutes\n3. Turn mixer on to low speed and add eggs one at a time.\n4. Sift flour into a separate bowl\n")>]
    member this.GivenValidIngredientsReturnsCorrectMarkdown input expected =
        let parser = componentEntryListParser  
        let result = run parser input
        match result with
        | Success(componentEntries, _, _) ->
            let convertedMarkdown = DinnerScribe.Core.MarkdownConverter.Primitives.convertComponentEntryList componentEntries
            Assert.AreEqual(expected, convertedMarkdown)
        | _ -> Assert.Fail ()

