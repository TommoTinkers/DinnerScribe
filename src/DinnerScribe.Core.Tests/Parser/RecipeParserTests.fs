module DinnerScribe.Core.Tests.Parser.RecipeParserTests

open System.IO
open FParsec
open NUnit.Framework
open DinnerScribe.Core.Parser.RecipeParser

[<TestFixture>]
type RecipeParserTests () =
    [<Test>]
    [<TestCase("Samples/valid_01.dinnerscribe")>]
    member this.GivenValidInputReturnsRecipe filename =
        let contents = File.ReadAllText (filename)
        let result = run recipeParser contents
        match result with
        | Success _ -> Assert.Pass ()
        | Failure _ -> Assert.Fail () 