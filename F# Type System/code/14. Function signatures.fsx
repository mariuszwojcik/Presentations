

// single param function
let add1 a = a + 1

// multi param function
let add a b = a + b

// unpure function "without result"
let print x = printfn "%A" x

// unpure function getting string
let readLine = System.Console.ReadLine

//#region higher order functions

let lmap funcToApply l = List.map funcToApply l

let valToStr i = i.ToString()
let list1 = [ 1; 2; 3]
lmap valToStr list1


//#region map

let fmap predicateFunc l = List.filter predicateFunc l

let isEven i = i%2 = 0
let list2 = [1; 2; 3; 4]
fmap isEven list2

//#endregion

//#region bind

let bindOption binder opt = Option.bind binder opt

let parseId s = 
    match System.Int32.TryParse s with
    | true, v -> Some v
    | false, _ -> None

let validateId id = if id > 10 && id < 100 then Some id else None

let parsedId = parseId "12"
bindOption validateId parsedId

//#region using pipeline operator

"55"
|> parseId
|> bindOption validateId

//#endregion

//#endregion

//#endregion