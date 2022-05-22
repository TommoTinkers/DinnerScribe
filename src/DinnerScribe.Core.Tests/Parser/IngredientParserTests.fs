module DinnerScribe.Core.Tests.Parser.IngredientParserTests

open DinnerScribe.Core.RecipeModel.Types
open DinnerScribe.Core.Parser.IngredientParser
open FParsec
open NUnit.Framework

[<TestFixture>]
type IngredientParserTests () =
    
    [<Test>]
    [<TestCase("+ 300g Chicken Thigh", 300u, MassUnit.Grams, "Chicken Thigh")>]
    [<TestCase("+ 2kg All Purpose Flour", 2u, MassUnit.Kilograms, "All Purpose Flour")>]
    [<TestCase("+ 2kg 2kg All Purpose Flour", 2u, MassUnit.Kilograms, "2kg All Purpose Flour")>]
    member this.GivenValidInputReturnsValidIngredient input expectedMass expectedUnit expectedName =
        let result = run ingredientParser input
        match result with
        | Success(ingredient, _, _) when ingredient.Name = expectedName && ingredient.Mass = { Amount = expectedMass; Unit = expectedUnit } -> Assert.Pass ()
        | _ -> Assert.Fail ()
        
                                        
    [<Test>]
    [<TestCase("/ 300g Chicken Thigh")>]
    [<TestCase("+5kg Onions")>]
    [<TestCase("")>]
    [<TestCase("  ")>]
    [<TestCase("+Cheese 250g")>]
    [<TestCase("+")>]
    [<TestCase("+ 593cg")>]
    [<TestCase("+ 10.1g Custard Powder")>]
    member this.GivenInvalidInpurReturnsFail input =
        let result = run ingredientParser input
        match result with
        | Failure _ -> Assert.Pass ()
        | _ -> Assert.Fail ()