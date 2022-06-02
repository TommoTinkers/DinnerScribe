module DinnerScribe.Core.MarkdownConverter.Primitives
open System
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

let convertStep step = $"{step.Number}. {step.Direction}\n"

let convertStepList steps = steps
                            |> List.sortBy (fun s -> s.Number)
                            |> List.map convertStep
                            |> List.fold (fun x y -> $"{x}{y}") String.Empty