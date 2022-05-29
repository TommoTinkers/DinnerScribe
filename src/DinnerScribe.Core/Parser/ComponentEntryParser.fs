module DinnerScribe.Core.Parser.ComponentEntryParser

open DinnerScribe.Core.Parser
open DinnerScribe.Core.RecipeModel.Types
open IngredientParser
open StepParser
open FParsec

let ingredientComponentEntryParser = ingredientParser |>> Ingredient
let stepComponentEntryParser = stepParser |>> Step

let componentEntryParser = spaces1 >>. (stepComponentEntryParser <|> ingredientComponentEntryParser)

let blankLine = many (pchar ' ' <|> pchar '\t') >>. newline

let componentEntryListParser = many1 (many (attempt blankLine) >>. componentEntryParser)



