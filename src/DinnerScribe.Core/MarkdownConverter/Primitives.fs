module DinnerScribe.Core.MarkdownConverter.Primitives
open DinnerScribe.Core.RecipeModel.Types
open DinnerScribe.Core.Parser.MassParser
let headerOne = '#'

let titlePrefix = headerOne

let unorderedListPrefix = '-'

let convertTitle (title:Title) = $"{titlePrefix} {title.Title}"


let convertAmount amount =
    match amount with
    | Mass m -> $"{m.Amount}{massCharByUnit m.Unit}"
    | Quantity q -> $"{q.Amount}"


let convertIngredient ingredient = $"{unorderedListPrefix} {convertAmount ingredient.Amount} {ingredient.Name}"