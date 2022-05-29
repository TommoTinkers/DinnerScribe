module DinnerScribe.Core.Parser.RecipeParser

open DinnerScribe.Core.Parser
open DinnerScribe.Core.RecipeModel
open Types
open TitleParser
open ComponentParser
open FParsec

let recipeParser = titleParser .>>. many1 componentParser |>> fun (title, components) -> {Title = title; Components = components}