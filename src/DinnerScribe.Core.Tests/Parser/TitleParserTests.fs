module DinnerScribe.Core.Tests.Parser.TitleParserTests

open DinnerScribe.Core.Parser.TitleParser
open DinnerScribe.Core.RecipeModel.Types
open NUnit.Framework

[<TestFixture>]
type TitleParserTests () =
    
    [<Test>]
    [<TestCase("Title: Beef & Broccoli\n", "Beef & Broccoli")>]
    [<TestCase("Title: Beef & Broccoli\r\n", "Beef & Broccoli")>]
    [<TestCase("Title: Beef & Broccoli\r\n More Content Here", "Beef & Broccoli")>]
    [<TestCase("Title: Beef & Broccoli\n More Content Here  Too ", "Beef & Broccoli")>]
    [<TestCase("Title: Mashed Potatoes", "Mashed Potatoes")>]
    member this.ValidInputReturnsValidTitle input expected =
        let result = TitleParser input
        let expectedResult:Result<Title, string> = Ok {Title = expected}
        
        Assert.AreEqual(expectedResult, result)