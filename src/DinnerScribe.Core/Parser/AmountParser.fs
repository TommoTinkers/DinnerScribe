module DinnerScribe.Core.Parser.MassParser

open FParsec
open DinnerScribe.Core.Parser
open DinnerScribe.Core.RecipeModel.Types
open HelperParsers


let massUnitTable = [ "g", MassUnit.Grams
                      "kg", MassUnit.Kilograms
                      "mg", MassUnit.Milligrams
                      "cg", MassUnit.Centigrams ]

let massCharParser = massUnitTable
                     |> List.map fst
                     |> List.map pstringCI
                     |> List.reduce (<|>)
                     

let massUnitByChar (massChar:string) =
    massUnitTable
    |> List.find (fun e -> e |> fst = massChar.ToLowerInvariant ())
    |> snd

let massCharByUnit (massUnit:MassUnit) =
    massUnitTable
    |> List.find (fun e -> e |> snd = massUnit)
    |> fst

let massUnitParser:Parser<MassUnit, unit> = massCharParser |>> massUnitByChar

let massParser = positiveIntParser .>>. massUnitParser |>> fun (amount, unit) -> { Unit = unit ; Amount = amount } 

let amountParserMass = massParser |>> Mass

let quantityParser = positiveIntParser |>> fun q -> {Amount = q}

let amountParserQuantity = quantityParser |>> Quantity

let amountParser = attempt amountParserMass <|> amountParserQuantity