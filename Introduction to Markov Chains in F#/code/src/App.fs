module App

(**
 Introduction to Markov Chains.

*)

open Elmish
open Elmish.React
open Fable.Helpers.React
open Fable.Helpers.React.Props
open TextPredictor
open Model
open View


let getSuggestions predictor str =
    let word = getLastWord str 
    predictor word |> Array.map(fun (w,_) -> w) |> Array.filter(fun w -> not(System.String.IsNullOrEmpty w))

let getPredictor selectedCorpora sentMessages =
    let updatedCorpora = loadCorpora selectedCorpora + "\n" + (sentMessages |> String.concat "\n")
    CreatePredictor updatedCorpora

let update (command) (model:Model) =
    match command with
    | UpdateSuggestions str -> 
        let suggestions =
            if str = "" || not (endsWithWhitespace str) then
                model.suggestions
            else
                str |> getSuggestions model.predictor           
        
        { model with 
              messageText = str
              suggestions = suggestions
        }

    | AddSuggestion suggestion ->
        let separator = 
            if endsWithWhitespace model.messageText then ""
            else " "
        
        let newContent = sprintf "%s%s%s" model.messageText separator suggestion
        { model with 
            messageText = newContent
            suggestions = newContent |> getSuggestions model.predictor
        }

    | SendMessage ->
        let sentMessages = model.sentMessages |> Array.append [| model.messageText |]
        { model with 
            messageText = ""
            suggestions = [||]
            sentMessages = sentMessages
            predictor = getPredictor model.selectedCorpora sentMessages
        }

    | ChangeCorpora corpora -> 
        { model with 
            messageText = ""
            suggestions = [||]
            selectedCorpora = corpora
            predictor = getPredictor corpora model.sentMessages
        }

// App
Program.mkSimple initModel update view
|> Program.withReact "elmish-app"
|> Program.withConsoleTrace
|> Program.run
