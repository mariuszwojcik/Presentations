let add a b = a + b

let printId id =
    printfn "The value of ID is: %s" id


//#region Influencing type inference

//add 3u 4u
//add 3.0 4.0
//add "abc" "def"

let add' (a: uint32) b = a + b

let add'' a b = (a : int64) + b

let add''' a b : string = a + b

let replace (s : string) =
    s.Replace("A", "a")

//#endregion


//#region Automating generalisation

let makeTuple a b = (a,b)

let addToList a l =
    List.append l [a]

//#endregion