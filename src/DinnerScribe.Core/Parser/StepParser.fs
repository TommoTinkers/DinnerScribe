module DinnerScribe.Core.Parser.StepParser

open FParsec
open DinnerScribe.Core.RecipeModel.Types
open HelperParsers

let stepNumberParser = positiveIntParser .>> pchar '.'

let stepParser = stepNumberParser .>> spaces1 .>>. AtLeastOneCharAndRestOfTheLine |>> fun (num, dir) -> { Number = num; Direction = dir }
