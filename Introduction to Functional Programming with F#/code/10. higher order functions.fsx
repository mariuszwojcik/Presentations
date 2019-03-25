let add a b = a + b
let multiply a b = a * b

let calculateAndPrint calcFunc x y =
    let r = calcFunc x y
    printfn "Result of the calculation on %i and %i is: %i" x y r
    r

calculateAndPrint add 3 4
calculateAndPrint multiply 3 4




let createCalcFunc op =
    match op with
    | "+" -> add
    | "*" -> multiply
    | "-" -> fun a b -> a - b
    | "/" -> (/)
    | _ -> failwithf "Invalid operator: %s" op

calculateAndPrint (createCalcFunc "-") 7 3
