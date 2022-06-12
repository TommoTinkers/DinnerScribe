module DinnerScribe.Core.RecipeModel.Types

type MassUnit = 
    | Milligrams = 0
    | Centigrams = 1
    | Grams = 3
    | Kilograms = 4

type Title = { Title: string }

type Step = { Number: uint; Direction: string }

type Quantity = {Amount : uint}
type Mass = { Amount : uint; Unit : MassUnit;  }

type Amount = | Mass of Mass
              | Quantity of Quantity
              
type Ingredient = { Amount : Amount; Name : string; }

type ComponentEntry = | Step of Step
                      | Ingredient of Ingredient
type Component = { Title: string; Entries: ComponentEntry List }

type Recipe = {Title: Title; Components: Component list}