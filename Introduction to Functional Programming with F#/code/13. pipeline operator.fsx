let add a b = a + b

3 |> add 4


List.reduce (fun a i -> a + "," + i) 
            (List.map(fun i-> i.ToString()) 
                     (List.filter(fun i -> i > 3) 
                                 (List.map(fun i -> i * 2) 
                                           [1;2;3])))

//#region |>

[ 1; 2; 3]
|> List.map(fun i -> i * 2)
|> List.filter(fun i -> i > 3)
|> List.map(fun i-> i.ToString())
|> List.reduce(fun a i -> a + "," + i)


let toCsvLine items = String.concat "," items

[ 1; 2; 3]
|> List.map(fun i -> i * 2)
|> List.filter(fun i -> i > 3)
|> List.map(fun i-> i.ToString())
|> toCsvLine


//#endregion

//#region not only lists...

open System.IO

Directory.GetCurrentDirectory()
|> Directory.GetCreationTime
|> printfn "Directory was created on %A"


(*

getOrder orderId
|> validateOrderState
|> getOrderLines
|> verifyStockAvailability
|> notifyDispatchers "Dispatch message"
|> addToAudit

*)

//#endregion