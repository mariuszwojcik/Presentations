
let v1 = Some 3
let v2 = None


//#region Opertaing on options

let add a b = a + b

add 3 4
add v1 4

add v1.Value 4  // DON'T DO THIS !!
add v2.Value 4  // EXCEPTION !!

//#region Option.map

let add' a b =
    match a with
    | Some v -> Some (v + b)
    | None -> None

add' v1 4
add' v2 4

v1 |> Option.map (add 4)
v2 |> Option.map (add 4)

//#endregion

//#region Option.iter

printfn "%A" v1

v1 |> Option.iter(fun v -> printfn "%A" v)
v2 |> Option.iter(fun v -> printfn "%A" v)

//#endregion

//#region Option.bind

let isEven v = if v%2 = 1 then Some v else None
v1 |> Option.map isEven
v1 |> Option.bind isEven

//#endregion

//#endregion

