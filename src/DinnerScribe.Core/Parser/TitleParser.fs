module DinnerScribe.Core.Parser.TitleParser

open DinnerScribe.Core.RecipeModel.Types
open HelperParsers


let TitleParser = namedFieldParser "Title" (fun x -> {Title = x})

