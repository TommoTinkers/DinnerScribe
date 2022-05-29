module DinnerScribe.Core.Parser.HelperParsers

open FParsec

let AtLeastOneCharAndRestOfTheLine:Parser<string, unit> = anyChar .>>. restOfLine true |>> fun (a, b) -> $"{a}{b}"
let unsignedIntParser:Parser<uint, unit> = many1 digit |>> fun x -> x |> System.String.Concat |> uint

let namedFieldParser field mapper = pstringCI field .>> pchar ':' >>. spaces >>. AtLeastOneCharAndRestOfTheLine |>> mapper