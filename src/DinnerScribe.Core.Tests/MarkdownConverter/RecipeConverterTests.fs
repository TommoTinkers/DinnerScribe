module DinnerScribe.Core.Tests.MarkdownConverter.RecipeConverterTests


open System.IO
open FParsec
open NUnit.Framework
open DinnerScribe.Core.Parser.RecipeParser
open DinnerScribe.Core.MarkdownConverter.Primitives

[<TestFixture>]
type RecipeParserTests () =
    [<Test>]
    [<TestCase("Samples/valid_01.dinnerscribe", "Samples/expected_01.md")>]
    //[<TestCase("Samples/valid_02.dinnerscribe", "expected_02.md")>]
    //[<TestCase("Samples/valid_03.dinnerscribe", "expected_03.md")>]
    member this.GivenValidInputReturnsRecipe filename expectedFileName =
        let contents = File.ReadAllText (filename)
        let expected = (File.ReadAllText (expectedFileName)).ReplaceLineEndings "\n"
        let result = run recipeParser contents
        match result with
        | Failure _ -> Assert.Fail ()
        | Success(recipe, _, _) -> Assert.AreEqual (expected, convertRecipe recipe)