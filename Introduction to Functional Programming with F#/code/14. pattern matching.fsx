type OrderLine = { itemId : int; price : decimal }

fun get orderId -> 
    if orderId = 1 then
        [ 
            { itemId = 1; price = 12.35M }
        ]
    else if orderId = 2 then 
        [ 
            { itemId = 1; price = 12.35M }
            { itemId = 2; price = 38.00M }
        ]
    else                 
        [ 
            { itemId = 1; price = 12.35M }
            { itemId = 2; price = 38.00M }
            { itemId = 3; price = 7.15M }
        ]


//#region Pattern matching version

fun get orderId -> 
    match orderId with
    | 1 ->
        [ 
            { itemId = 1; price = 12.35M }
        ]
    | 2 -> 
        [ 
            { itemId = 1; price = 12.35M }
            { itemId = 2; price = 38.00M }
        ]
    | _ ->               
        [ 
            { itemId = 1; price = 12.35M }
            { itemId = 2; price = 38.00M }
            { itemId = 3; price = 7.15M }
        ]
 
//#region more powerful patterns

type Shape =
    | Square of side : float
    | Rectangle of width : float * length : float
    | Circle of radius : float

let calculateArea shape =
    match shape with
    | Square a -> a * a
    | Rectangle (w , l) -> w * l
    | Circle r -> System.Math.PI * r * r 

//#region guards

let shapeName shape =
    match shape with
    | Square _ -> "Square"
    | Rectangle (w, l) when w = l -> "Square"
    | Rectangle _ -> "Rectangle"
    | Circle _ -> "Circle"

//#region Alternative syntaxt

let shapeName' = function
    | Square _ -> "Square"
    | Rectangle (w, l) when w = l -> "Sqaure"
    | Rectangle _ -> "Rectangle"
    | Circle _ -> "Circle"

//#endregion

//#endregion

//#endregion

//#endregion