module DinnerScribe.Core.MarkdownConverter.Primitives
open DinnerScribe.Core.RecipeModel.Types
let headerOne = '#'

let titlePrefix = headerOne

let convertTitle (title:Title) = $"{titlePrefix} {title.Title}"