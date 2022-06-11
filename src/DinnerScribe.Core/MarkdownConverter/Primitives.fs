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

let convertIngredient ingredient = $"{unorderedListPrefix} {convertAmount ingredient.Amount} {ingredient.Name}\n"

let convertStep step = $"{step.Number}. {step.Direction}\n"

let convertStepList steps = steps
                            |> List.sortBy (fun s -> s.Number)
                            |> List.map convertStep
                            |> List.fold (fun x y -> $"{x}{y}") String.Empty
              


let ingredientPartitionPredicate ingredient =
    match ingredient.Amount with
    | Mass _ -> true
    | Quantity _ -> false

let splitIngredients ingredients = ingredients |> List.partition ingredientPartitionPredicate 
                                 

let sortIngredients ingredients =
    ingredients
    |> splitIngredients
    |> (fun (masses, quantities) -> (masses |> List.sortByDescending (fun m -> m.Amount), quantities |> List.sortByDescending (fun q -> q.Amount)))
    |> fun (a, b) -> a @ b
    
let convertIngredientList ingredients = ingredients
                                        |> sortIngredients
                                        |> List.map convertIngredient
                                        |> List.fold (fun x y -> $"{x}{y}") String.Empty                          