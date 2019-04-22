module View

open Fable.Helpers.React
open Fable.Helpers.React.Props
open Model

let view (model:Model) dispatch =

  div [ Class "container" ]
      [
        nav [ Class "navbar navbar-dark bg-dark" ] 
            [
              span [ Class "navbar-brand mb-0 h1" ] [ str "Messages" ]
              span [ Class "navbar-brand" ]
                   [
                      div [ ]
                          [
                            label 
                              [ ]
                              [ str "Select corpora:" ]
                            str " "
                            select 
                              [ 
                                Value (model.selectedCorpora |> fromCorporaType)
                                OnChange(fun e -> dispatch(ChangeCorpora (toCorporaType e.Value)))
                              ]
                              [
                                option [ ] [ str "---" ]
                                option [ ] [ str "Marketing messages" ]
                                option [ ] [ str "Around the World in 80 days" ]
                              ]  
                          ]
                   ]
            ]

        p [] [ ]

        div 
          [ Class "Predictions" ]          
          [
            for s in model.suggestions -> 
              a [ Class "btn btn-outline-primary btn-sm" ; Href "#" ; OnClick(fun _ -> dispatch (AddSuggestion s))] [ str s ]
            
          ]
        div [ Class "Message" ; Id "messageBox" ]
            [
              textarea [
                  Value model.messageText
                  Cols 70.
                  Rows 4.
                  OnChange(fun e -> dispatch (UpdateSuggestions e.Value))
                ] [ ]
              br []
              button [ OnClick(fun _ -> dispatch SendMessage)] [ str "Send"]     
              p [] []         
            ]

        div [ Class "messages" ]
          [
            for m in (model.sentMessages |> Array.truncate 5) ->
              p [ Class "navbar navbar-light bg-light" ] [ str m ]
          ]          
      ]