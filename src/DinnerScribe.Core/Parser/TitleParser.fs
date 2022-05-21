module DinnerScribe.Core.Parser.TitleParser

open DinnerScribe.Core.RecipeModel.Types
open HelperParsers
open FParsec

let TitleParser = pstringCI "Title:" >>. spaces >>. AtLeastOneCharAndRestOfTheLine |>> fun x -> {Title = x}

