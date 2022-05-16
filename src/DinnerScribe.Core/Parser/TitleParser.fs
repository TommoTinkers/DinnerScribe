module DinnerScribe.Core.Parser.TitleParser

open DinnerScribe.Core.RecipeModel.Types
open FParsec

let TitleParser input =
    let parser = pstring "Title:" >>. spaces >>. restOfLine true |>> fun x -> {Title = x}
    let result = run parser input
    match result with
    | Success(title, _, _) -> FSharp.Core.Ok title
    | Failure (msg, _, _) -> FSharp.Core.Error msg 