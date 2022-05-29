module DinnerScribe.Core.Parser.ComponentParser
open DinnerScribe.Core.Parser.ComponentEntryParser

open DinnerScribe.Core.RecipeModel
open Types
open FParsec
open HelperParsers

let componentTitleParser = namedFieldParser "Component" id

let componentParser = componentTitleParser .>>. componentEntryListParser |>> fun (title, entryList) -> {Title = title; Entries = entryList}