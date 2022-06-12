module DinnerScribe.Core.Parser.TitleParser
open FParsec
open DinnerScribe.Core.RecipeModel.Types
open HelperParsers


let titleParser = namedFieldParser "Title" (fun x -> {Title = x})

