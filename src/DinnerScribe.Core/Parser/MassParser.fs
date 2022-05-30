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

let massUnitParser:Parser<MassUnit, unit> = massCharParser |>> fun massChar ->
    massUnitTable
    |> List.find (fun e -> e |> fst = massChar.ToLowerInvariant ())
    |> snd

let massParser = positiveIntParser .>>. massUnitParser |>> fun (amount, unit) -> { Unit = unit ; Amount = amount }