module DinnerScribe.Core.Tests.Parser.TitleParserTests

open DinnerScribe.Core.Parser.TitleParser
open DinnerScribe.Core.RecipeModel.Types
open NUnit.Framework

[<TestFixture>]
type TitleParserTests () =
    
    [<Test>]
    [<TestCase("Title: Beef & Broccoli", "Beef & Broccoli")>]
    [<TestCase("Title: Mashed Potatoes", "Mashed Potatoes")>]
    member this.ValidInputReturnsValidTitle input expected =
        let result = TitleParser input
        let expectedResult:Result<Title, string> = Ok {Title = expected}
        
        Assert.AreEqual(expectedResult, result)