

let add a b = a + b



//#region Impure functions

let addWithJitter a b = 
    a + b + (System.DateTime.Now.Millisecond % 10)
addWithJitter 3 4



let a = 10
let getA () =
    a


let getUserName =
    System.Console.WriteLine("Enter your name:")
    System.Console.ReadLine()

//#endregion
