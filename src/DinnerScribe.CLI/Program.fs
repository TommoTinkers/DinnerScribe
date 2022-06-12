open System.IO
open Argu
open DinnerScribe.Core.Parser
open FParsec
open DinnerScribe.Core.MarkdownConverter.Primitives

type Arguments =
    | [<Mandatory>] [<AltCommandLine("-f")>][<Unique>] Filename of path:string

    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Filename _ -> "The file you want to convert into a recipe"
            

let fileWriter filename content = File.WriteAllText (filename, content)
            
let DisplayError error = printfn $"{error}"

let generateOutputFilename f = Path.ChangeExtension (f, ".md")

[<EntryPoint>]
let main args =
    
    let parser = ArgumentParser.Create<Arguments>(programName = "DinnerScribe.CLI.exe")
    
    let results = parser.Parse args
    
    let filename = results.TryGetResult Filename
    
    match filename with
    | None -> DisplayError ("Please enter a filename.")  
    | Some f ->
        let outputFile = generateOutputFilename f
        match File.Exists(f) with
        | false -> DisplayError("Could not find the file specifiec")
        | true ->
            let parseResult = f |> File.ReadAllText |> run RecipeParser.recipeParser
            match parseResult with
            | Success(recipe, _, _) -> recipe |> convertRecipe |> fileWriter outputFile
            | Failure(s, parserError, unit) -> s |> DisplayError
            ()
    
    printfn $"{filename}"
    0