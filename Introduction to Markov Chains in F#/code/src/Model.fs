module Model

open TextPredictor
open Corporas

type CorporaType =
| None
| MarketingMessages
| AroundTheWorldInEightyDays

type Model = {
    selectedCorpora : CorporaType
    messageText : string
    suggestions : string []
    predictor : string -> (string * float)[]
    sentMessages : string []
}

type Msg =
| UpdateSuggestions of string
| AddSuggestion of string
| ChangeCorpora of CorporaType
| SendMessage


let toCorporaType str =
    match str with
    | "---" -> None
    | "Marketing messages" -> MarketingMessages
    | "Around the World in 80 days" -> AroundTheWorldInEightyDays
    | _ -> None

let fromCorporaType corporaType =
    match corporaType with
    | None -> "---"
    | MarketingMessages -> "Marketing messages"
    | AroundTheWorldInEightyDays -> "Around the World in 80 days"    


let loadCorpora corporaType =
    match corporaType with
    | None -> ""
    | MarketingMessages -> Corporas.MarketingMessages.corpora
    | AroundTheWorldInEightyDays -> Corporas.AroundTheWorldInEightyDays.corpora    

let initModel() : Model =
    let corpora = loadCorpora MarketingMessages
    {
        selectedCorpora = MarketingMessages
        messageText = ""
        suggestions = [||]
        predictor = CreatePredictor corpora
        sentMessages = [||]
    }