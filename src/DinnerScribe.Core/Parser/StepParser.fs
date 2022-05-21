module DinnerScribe.Core.Parser.StepParser

open FParsec
open DinnerScribe.Core.RecipeModel.Types
open HelperParsers

let StepParser = unsignedIntParser .>> spaces .>>. AtLeastOneCharAndRestOfTheLine |>> fun (num, dir) -> { Number = num; Direction = dir }
