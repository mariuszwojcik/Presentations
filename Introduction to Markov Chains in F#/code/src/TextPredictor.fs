module TextPredictor

open System
open System.Text.RegularExpressions
open Fable.Import


let splitWords text =
    Regex.Split(text, "\W+")

let getLastWord text =
    text |> splitWords |> Array.filter(fun i -> i <> "") |> Array.tryLast |> Option.defaultValue ""

let endsWithWhitespace text =
    Regex.IsMatch(text, ".*\W$")

let toCanonical (word : string) =
    word.ToLower()


let createModel corpora =
    corpora
    |> splitWords
    |> Array.map toCanonical
    |> Array.windowed 2
    |> Array.groupBy (fun i -> i |> Array.head)
    |> Array.map(fun (word, followWords) ->
        let words = followWords |> Array.map(Array.tail) |> Array.collect id
        let counted = words |> Array.countBy id
        let total = Array.sumBy snd counted
        (word, counted |> Array.map (fun (w, i) -> (w, float i / float total)))
    )
    |> Map.ofArray

let predictNextWords (model : Map<string, (string * float) []>) word =
    let canonicalWord = word |> toCanonical
    
    model
    |> Map.tryFind canonicalWord
    |> Option.defaultValue [| ("", 1.0) |]
    |> Array.sortByDescending(fun (_,b) -> b)
    |> Array.truncate 10

let CreatePredictor corpora =
    let model = createModel corpora
    predictNextWords model