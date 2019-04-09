open System;


type ProductId =
| GadgetId of int
| GizmoId of string

let i = GizmoId "PGG99"


//#region pretty print

printfn "%A" i

//#endregion


//#region Unions equality

let pId1 = GadgetId 12
let pId2 = GadgetId 12

pId1 = pId2

type ItemId =
| OldId of int
| NewId of int

let item1 = OldId 3
let item2 = NewId 3

item1 = item2

//#endregion


//#region HashCode

pId1.GetHashCode()
pId2.GetHashCode()

let d = new System.Collections.Generic.HashSet<ProductId>()
d.Add pId1
d.Add pId2


item1.GetHashCode()
item2.GetHashCode()

let d' = new System.Collections.Generic.HashSet<ItemId>()
d'.Add item1
d'.Add item2

//#endregion


//#region Generic unions

type Result<'a> = 
  | Success of 'a
  | ErrorMessage of string

let parseNumber str =
    match Int32.TryParse str with
    | true, v -> Success v
    | false, _ -> ErrorMessage (sprintf "String %s is not a number" str)

parseNumber "347"
parseNumber "347a"

//#endregion


//#region Object Hierarchies

type Shape =
    | Square of side : float
    | Rectangle of width : float * length : float
    | Circle of radius : float

let calculateArea shape =
    match shape with
    | Square a -> a * a
    | Rectangle (w , l) -> w * l
    | Circle r -> System.Math.PI * r * r 


//#endregion


//#region Single cases

// Type aliases
module TypeAliasing =
    type CustomerId = int
    type OrderId = int

    let printOrderId (orderId:OrderId) = 
       printfn "The orderId is %i" orderId

    let custId = 1
    // passing wrong type to the function !!
    printOrderId custId 

module SingleUnions =
    type CustomerId = CustomerId of int
    type OrderId = OrderId of int

    let printOrderId (OrderId orderId) =
       printfn "The orderId is %i" orderId

    let custId = CustomerId 1
    // compiler doesn't allow invalid type !!
    printOrderId custId 

    printOrderId (OrderId 99)


//#endregion


//#region Recursive Unions

type Tree =
    | Tip
    | Node of int * Tree * Tree

(* 
     0
     /\
    /  \
   1    4
   /\
  /  \
 2    3
*)

let myTree = Node(0, Node(1, Node(2, Tip, Tip), Node(3, Tip, Tip)), Node(4, Tip, Tip))

let rec sumTree tree =
    match tree with
    | Tip -> 0
    | Node(value, left, right) ->
        value + sumTree(left) + sumTree(right)

sumTree myTree



type Operator = 
| Plus
| Minus

and  Operand =
| Number of int
| Decimal of decimal

and Operation =
| Operand of Operand
| Formula of Operation * Operator * Operation

let n1 = Operand(Number 3)
let n2 = Operand(Number 4)
let c1 = Formula(n1, Plus, n2)
let c2 = Formula(n1, Plus, Formula(Operand(Decimal 7.3M), Minus, Operand(Number 2)))

//#endregion