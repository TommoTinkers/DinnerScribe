module DinnerScribe.Core.Tests.Parser.ComponentParserTests

open FParsec
open NUnit.Framework

open DinnerScribe.Core.Parser.ComponentParser

[<TestFixture>]
type ComponentParserTests () =
    [<Test>]
    [<TestCase("Component: Chicken Stock\n\t1. Put Chicken Bones in pressure cooker\n\t2. Cook for 90 minutes on full pressure\n\t+ 1kg Chicken bones\nEndComponent")>]
    [<TestCase("component: Chicken Stock\n\t1. Put Chicken Bones in pressure cooker\n\t2. Cook for 90 minutes on full pressure\n\t+ 1kg Chicken bones\nEndComponent")>]
    [<TestCase("component: Cheese on toast\n\t1. Make Toast\n\t2. Put Cheese on the toast\n\t+ 120g Bread\n\t+ 120g Cheese\n\t+ 400kg No self respect\nEndComponent")>]
    [<TestCase("component: Cheese on toast\n\t1. Make Toast\n\t2. Put Cheese on the toast\n\t+ 120g Bread\n\t+ 120g Cheese\n\t+ 400kg No self respect\nEndcomponent")>]
    member this.GivenValidInputReturnsAComponent input =
        match run componentParser input with
        | Success _ -> Assert.Pass ()
        | Failure _ -> Assert.Fail ()
         
         
    [<Test>]
    [<TestCase("")>]
    [<TestCase("\nComponent: Chicken Stock\n\t1. Put Chicken Bones in pressure cooker\n\t2. Cook for 90 minutes on full pressure\n\t+ 1kg Chicken bones\nEndComponent")>]
    [<TestCase("Component: Chicken Stock\n\t1. Put Chicken Bones in pressure cooker\n\t2. Cook for 90 minutes on full pressure\n\t+ 1kg Chicken bones EndComponent")>]
    [<TestCase("Component: Chicken Stock\n\t1. Put Chicken Bones in pressure cooker\n\t2. Cook for 90 minutes on full pressure\n\t+ 1kg Chicken bones\n")>]
    member this.GivenInvalidInputReturnsFailure input =
        match run componentParser input with
        | Failure _ -> Assert.Pass ()
        | _ -> Assert.Fail ()