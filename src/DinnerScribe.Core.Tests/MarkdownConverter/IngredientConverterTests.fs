module DinnerScribe.Core.Tests.MarkdownConverter.IngredientConverterTests

open DinnerScribe.Core.Parser.IngredientParser
open NUnit.Framework
open DinnerScribe.Core.MarkdownConverter.Primitives
open FParsec

[<TestFixture>]
type IngredientConverterTests () = 
    [<Test>]
    [<TestCase("+ 300g Biscuits", "- 300g Biscuits\n")>]
    [<TestCase("+ 40kg Cream", "- 40kg Cream\n")>]
    [<TestCase("+ 3 Sausages", "- 3 Sausages\n")>]
    member this.ValidIngredientReturnsValidMarkdown input expected =
        let result = run ingredientParser input
        match result with
        | Failure _ -> Assert.Fail ()
        | Success (ingredient, _, _) ->
            let actual = convertIngredient ingredient
            Assert.AreEqual(expected, actual)
            