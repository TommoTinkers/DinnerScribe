module DinnerScribe.Core.Tests.MarkdownConverter.IngredientListConverterTests

open DinnerScribe.Core.Parser
open FParsec
open NUnit.Framework

[<TestFixture>]
type IngredientListConverterTests () =
    [<Test>]
    [<TestCase("+ 4 eggs\n+ 250g Self raising flour\n+ 250g Caster Sugar\n+ 250g Butter", "- 250g Butter\n- 250g Caster Sugar\n- 250g Self raising flour\n- 4 eggs\n\n")>]
    member this.GivenValidIngredientsReturnsCorrectMarkdown input expected =
        let parser = many IngredientParser.ingredientParser
        let result = run parser input
        match result with
        | Success(ingredients, _, _) ->
            let convertedMarkdown = DinnerScribe.Core.MarkdownConverter.Primitives.convertIngredientList ingredients
            Assert.AreEqual(expected, convertedMarkdown)
        | _ -> Assert.Fail ()

    [<Test>]
    [<TestCase("+ 500g Cheese\n+ 250g Cream\n", "- 500g Cheese\n- 250g Cream\n")>]
    [<TestCase("+ 250g Cheese\n+ 500g Cream\n", "- 500g Cream\n- 250g Cheese\n")>]
    [<TestCase("+ 250g Cheese\n+ 1kg Cream\n", "- 1kg Cream\n- 250g Cheese\n")>]
    [<TestCase("+ 250g Cheese\n+ 1kg Cream\n+ 5 Bricks\n", "- 1kg Cream\n- 250g Cheese\n- 5 Bricks\n")>]
    [<TestCase("+ 250g Cheese\n+ 20cg Xanthan Gum\n+ 1kg Cream\n+ 5 Bricks\n", "- 1kg Cream\n- 250g Cheese\n- 20cg Xanthan Gum\n- 5 Bricks\n")>]
    member this.GivenValidIngredientsReturnsSortedMarkdown input expected =
        let parser = many IngredientParser.ingredientParser
        let result = run parser input
        match result with
        | Success(ingredients, _, _) ->
            let convertedMarkdown = DinnerScribe.Core.MarkdownConverter.Primitives.convertIngredientList ingredients
            Assert.AreEqual(expected, convertedMarkdown)
        | _ -> Assert.Fail ()


        