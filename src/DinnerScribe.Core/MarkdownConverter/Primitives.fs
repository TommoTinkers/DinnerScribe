module DinnerScribe.Core.MarkdownConverter.Primitives
open System
open DinnerScribe.Core.RecipeModel.Types
open DinnerScribe.Core.Parser.MassParser

let headerOne = '#'
let headerThree = "###"
let headerFour = "####"
let componentEntryPrefix = headerFour
let componentPrefix = headerThree


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
                                 
let massInMilligrams amount =
    match amount with
    | Quantity _ -> None
    | Mass m ->
        match m.Unit with
        | MassUnit.Milligrams -> Some m.Amount
        | MassUnit.Centigrams -> Some (m.Amount * 10ul)
        | MassUnit.Grams -> Some (m.Amount * 1000ul)
        | MassUnit.Kilograms -> Some (m.Amount * 1000ul * 1000ul)
        | _ -> None 


let sortIngredients ingredients =
    ingredients
    |> splitIngredients
    |> (fun (masses, quantities) -> (masses |> List.sortBy (fun m -> m.Name), quantities))
    |> (fun (masses, quantities) -> (masses |> List.sortByDescending (fun m -> massInMilligrams m.Amount) , quantities |> List.sortByDescending (fun q -> q.Amount)))
    |> fun (a, b) -> a @ b
    
let convertIngredientList ingredients = ingredients
                                        |> sortIngredients
                                        |> List.map convertIngredient
                                        |> List.fold (fun x y -> $"{x}{y}") String.Empty
                                        
                                        
type ComponentEntryConversionTable =
     {
         Ingredients : Ingredient list 
         Steps : Step list
     }
    
let addComponentEntryToConversionTable table entry =
    match entry with
    | Ingredient i -> {table with Ingredients = (i :: table.Ingredients) }
    | Step s -> {table with Steps = (s :: table.Steps)}
    
let createConversionTable entries = entries |> List.fold addComponentEntryToConversionTable {Ingredients = []; Steps = []}

let convertConversionTable table = (convertIngredientList table.Ingredients, convertStepList table.Steps )

let convertComponentEntryList entries =
    entries
    |> createConversionTable
    |> convertConversionTable
    |> fun (l, r) -> $"{componentEntryPrefix} Ingredients\n{l}\n{componentEntryPrefix} Method\n{r}"
    
let convertComponent (cmpnent:Component) = $"{componentPrefix} {cmpnent.Title}\n{convertComponentEntryList cmpnent.Entries}\n"

let convertRecipe recipe =
    let title = convertTitle recipe.Title
    let components = List.map convertComponent recipe.Components |> List.reduce (+)
    title + "\n" + components
    