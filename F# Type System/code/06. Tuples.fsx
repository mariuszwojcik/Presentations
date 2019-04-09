let a = (2, 3)
let b = 4, true
let triple = 1, false, "John"

// defining custom tuple type
type CoordsTuple = float * float
let myCoordsTuple : CoordsTuple = 3.234 , 3.47

let printCoordsTuple (ct : CoordsTuple) =
    printfn "coords: %A" myCoordsTuple
printCoordsTuple myCoordsTuple


// values separated by comma create tuple !!
let listOfInts = [ 1 ; 2 ; 3 ]
let listOfTuples = [ 1, 2, 3 ]




//#region Tuple deconstruction

let id, enabled, name = triple

// partial deconstruction
let id', _, name' = triple

// func parameters pattern matching
let printTriple (id, _, name) = printfn "id: %i, name: %s" id name
printTriple triple

//#endregion


//#region pretty print

printfn "%A" a
printfn "Triple value is %A" triple

//#endregion


//#region Tuples equality

let r1 = 1, true, "test"
let r2 = 1, true, "test"

r1 = r2

//#endregion


//#region usages

let i = ref 0
System.Int32.TryParse("1234", i)
i


let parseResult = System.Int32.TryParse("123")

let printValue s =
    match System.Int32.TryParse s with
    | true, v -> printfn "The value is %d" v
    | false, _ -> printfn "Could't parse value!"

printValue "345"
printValue "345a"

//#endregion