module DinnerScribe.Core.Tests.Parser.MassUnitTests

open DinnerScribe.Core.RecipeModel.Types
open DinnerScribe.Core.Parser.MassParser
open FParsec

open NUnit.Framework

[<TestFixture>]
type MassUnitParserTests () =
    
    [<Test>]
    [<TestCase("g", MassUnit.Grams)>]
    [<TestCase("kg", MassUnit.Kilograms)>]
    [<TestCase("cg", MassUnit.Centigrams)>]
    [<TestCase("mg", MassUnit.Milligrams)>]
    [<TestCase("G", MassUnit.Grams)>]
    [<TestCase("KG", MassUnit.Kilograms)>]
    [<TestCase("CG", MassUnit.Centigrams)>]
    [<TestCase("MG", MassUnit.Milligrams)>]
    [<TestCase("Kg", MassUnit.Kilograms)>]
    [<TestCase("Cg", MassUnit.Centigrams)>]
    [<TestCase("Mg", MassUnit.Milligrams)>]
    [<TestCase("kG", MassUnit.Kilograms)>]
    [<TestCase("cG", MassUnit.Centigrams)>]
    [<TestCase("mG", MassUnit.Milligrams)>]
    member this.GivenValidInputReturnsValidMass input expected =
        let result = run massUnitParser input
        match result with
        | Success (unit, _, _) when unit = expected -> Assert.Pass ()
        | _ -> Assert.Fail ()
  
    [<Test>]
    [<TestCase("ml")>]
    [<TestCase("")>]
    [<TestCase("RJXIS(")>]
    [<TestCase("76")>]
    [<TestCase("    ")>]
    [<TestCase("   kg ")>]
    member this.GivenInvalidInputFails input =
        let result = run massUnitParser input
        match result with
        | Failure _ -> Assert.Pass ()
        | _ -> Assert.Fail ()