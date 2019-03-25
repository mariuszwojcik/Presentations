let sumOfNumbers =
    [1..5]
    |> Seq.fold (fun acc elem -> acc + elem) 0

// #region Using accummulation

let buildSentence =
    [ "The";"quick";"brown";"fox";"jumps";"over";"the";"lazy";"dog"]
    |> Seq.reduce(fun sentence word -> sentence + " " + word)


let longestWord =
    let chooseLongerWord (word1 : string) (word2 : string)  =
        if word1.Length >= word2.Length 
        then word1
        else word2

    [ "The";"quick";"brown";"fox";"jumps";"over";"the";"lazy";"dog"]
    |> Seq.reduce(fun longestWordSoFar word -> chooseLongerWord longestWordSoFar word)

//#endregion



// #region Value cannot change

let a = 3
a = 4

// #endregion


// #region Return new value from function

let number = 18
let nextNumber i = i + 1
nextNumber number

//#endregion


// #region Update record

type Customer = { Id : int ; Name : string; Orders: int list }

let c = { Id = 1; Name = "Jon White"; Orders = [1;2;3] }

let changeName customer newName =
    { customer with Name = newName }

let c' = changeName c "Jonathan White"

let addOrder customer orderNo =
    { customer with Orders = customer.Orders @ [orderNo] }

let c'' = addOrder c 7 

c''.Orders
c.Orders

//#endregion


//#region Thread state

let nextRecord state =
    let newState = state + 1
    let record = 
        {
            Id = newState
            Name = newState.ToString()
            Orders = [ 1..newState ]
        }
    (record, newState)

let initialState = 0
let (record1, state1) = nextRecord initialState
let (record2, state2) = nextRecord state1

//#endregion
