module DinnerScribe.Core.Parser.HelperParsers

open FParsec

let AtLeastOneCharAndRestOfTheLine:Parser<string, unit> = anyChar .>>. restOfLine true |>> fun (a, b) -> $"{a}{b}"

let nonZeroDigit = Seq.ofList ['1'..'9'] |> Seq.map pchar |> Seq.reduce (<|>)

let positiveIntParser:Parser<uint, unit> = nonZeroDigit .>>. many digit |>> fun (x,y) -> x :: y |> System.String.Concat |> uint

let namedFieldParser field mapper = pstringCI field .>> pchar ':' >>. spaces >>. AtLeastOneCharAndRestOfTheLine |>> mapper