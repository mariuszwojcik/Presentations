open System
let add a b = a + b


// #region Fixing function parameters

let add5 = add 5
add5 3
List.map add5 [1; 2; 3]


let add5ToEach = List.map (add5)
add5ToEach [1; 2; 3]

let filterEvens = List.filter (fun i -> i%2 = 0)
filterEvens [1;2;3;4]

//#endregion


//#region Dependency Injection

type OrderId = int
type OrderTotal = decimal
type OrderLine = { itemId : int; price : decimal }
type OrderRepo =
    {
        get : OrderId -> OrderLine list
    }

let calculateOrderTotal orderRepo orderId =
    let orderItems = orderRepo.get orderId
    let result : OrderTotal = orderItems |> List.sumBy(fun i -> i.price);
    result;


let MockRepo =
    {
        get = 
            fun orderId -> 
                if orderId = 1 then
                    [ 
                        { itemId = 1; price = 12.35M }
                    ]
                else
                    [ 
                        { itemId = 1; price = 12.35M }
                        { itemId = 2; price = 38.00M }
                    ]
                
        }

let calculateMockOrderTotal = calculateOrderTotal MockRepo

calculateMockOrderTotal 2


// inject logger
type Logger = { log : string -> unit }

let _getOrderItems logger repo orderId =
    logger.log (sprintf "** Gettig order %d" orderId)
    let orderItems = repo.get orderId
    logger.log (sprintf "** Order has %d lines" (orderItems.Length))
    orderItems

let getOrderItems = 
    _getOrderItems { log = fun s -> System.Console.WriteLine(s) } MockRepo 

getOrderItems 2


// create new calcTotal function with injected logger
let calculateMockOrderTotal' = calculateOrderTotal { get = getOrderItems }
calculateMockOrderTotal' 2

//#endregion