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
    [<TestCase("title: Mashed Potatoes", "Mashed Potatoes")>]
    [<TestCase("tItle: Mashed Potatoes", "Mashed Potatoes")>]
    member this.ValidInputReturnsValidTitle input expected =
        let result = TitleParser input
        let expectedResult:Result<Title, string> = Ok {Title = expected}
        
        Assert.AreEqual(expectedResult, result)
        
    [<Test>]
    [<TestCase("Mashed Potatoes")>]
    [<TestCase("Tytle: Mashed Potatoes")>]
    [<TestCase("Title Mashed Potatoes")>]
    member this.InvalidInputReturnsError input =
        let result = TitleParser input
        match result with
        | Error _ -> Assert.Pass ()
        | _ -> Assert.Fail ()
         
        
    [<Test>]
    [<TestCase("Title:")>]
    [<TestCase("Title: ")>]
    [<TestCase("Title:  ")>]
    [<TestCase("Title:\n")>]
    [<TestCase("Title:\r\n")>]
    member this.MissingTitleReturnsError input =
        let result = TitleParser input
        match result with
        | Error _ -> Assert.Pass ()
        | _ -> Assert.Fail ()