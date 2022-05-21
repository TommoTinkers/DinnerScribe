module DinnerScribe.Core.Parser.StepParser

open FParsec
open DinnerScribe.Core.RecipeModel.Types
open HelperParsers

let StepParser = unsignedIntParser .>> spaces1 .>>. AtLeastOneCharAndRestOfTheLine |>> fun (num, dir) -> { Number = num; Direction = dir }
