module DinnerScribe.Core.Parser.ComponentParser
open DinnerScribe.Core.Parser.ComponentEntryParser

open DinnerScribe.Core.RecipeModel
open Types
open FParsec
open HelperParsers

let componentTitleParser = namedFieldParser "Component" id
let endComponent = pstringCI "EndComponent"

let componentParser = componentTitleParser .>>. componentEntryListParser .>> endComponent .>> spaces |>> fun (title, entryList) -> {Title = title; Entries = entryList} 