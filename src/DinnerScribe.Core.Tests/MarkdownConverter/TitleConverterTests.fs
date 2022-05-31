module DinnerScribe.Core.Tests.MarkdownConverter.TitleConverterTests

open DinnerScribe.Core.Parser.TitleParser
open NUnit.Framework
open DinnerScribe.Core.MarkdownConverter.Primitives
open FParsec

[<TestFixture>]
type TitleConverterTests () = 
    [<Test>]
    [<TestCase("Title: Mashed Potatoes", "# Mashed Potatoes")>]
    [<TestCase("Title: 34y83", "# 34y83")>]
    member this.ValidTitleReturnsValidMarkDownHeaderOne input expected =
        let result = run titleParser input
        match result with
        | Failure _ -> Assert.Fail ()
        | Success (title, _, _) ->
            let actual = convertTitle title
            Assert.AreEqual(expected, actual)
            