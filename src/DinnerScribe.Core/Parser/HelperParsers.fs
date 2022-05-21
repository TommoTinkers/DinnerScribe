module DinnerScribe.Core.Parser.HelperParsers

open FParsec

let AtLeastOneCharAndRestOfTheLine:Parser<string, unit> = anyChar .>>. restOfLine true |>> fun (a, b) -> $"{a}{b}"
let unsignedIntParser:Parser<uint, unit> = many1 digit .>> pchar '.' |>> fun x -> x |> System.String.Concat |> uint    