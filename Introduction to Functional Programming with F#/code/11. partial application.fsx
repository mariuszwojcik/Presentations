let add a b = a + b

let calculateAndPrint calcFunc x y =
    let r = calcFunc x y
    printfn "Result is: %i" r
    r

calculateAndPrint add 3 4

//#region Partial application

let addAndPrint = calculateAndPrint add
addAndPrint 3 4


let addTo3AndPrint = calculateAndPrint add 3
addTo3AndPrint 4


let multiplyAndPrint = calculateAndPrint (fun a b -> a * b)
multiplyAndPrint 3 4

//#endregion