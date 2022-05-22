module DinnerScribe.Core.Parser.IngredientParser

open DinnerScribe.Core.RecipeModel
open FParsec
open Types
open HelperParsers
open MassParser

let ingredientPrefix = pchar '+'

let ingredientParser = ingredientPrefix >>. spaces1 >>. massParser .>>. spaces1 .>>. AtLeastOneCharAndRestOfTheLine |>> fun (a, b) -> { Mass = fst a; Name = b }