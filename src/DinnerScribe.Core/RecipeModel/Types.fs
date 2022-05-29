module DinnerScribe.Core.RecipeModel.Types

type MassUnit = 
    | Milligrams = 0
    | Centigrams = 1
    | Grams = 3
    | Kilograms = 4

type Title = { Title: string }

type Step = { Number: uint; Direction: string }

type Mass = { Amount : uint; Unit : MassUnit }

type Ingredient = { Mass : Mass; Name : string; }

type ComponentEntry = | Step of Step
                      | Ingredient of Ingredient
type Component = { Title: string; Entries: ComponentEntry List }

type Recipe = {Title: Title; Components: Component list}