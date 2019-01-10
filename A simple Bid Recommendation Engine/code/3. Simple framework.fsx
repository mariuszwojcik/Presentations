// Based on great article "Understanding Parser Combinators" by Scott Wlaschin
// https://fsharpforfunandprofit.com/posts/understanding-parser-combinators/

open System


type Result<'a> =
    | Success of 'a
    | Failure of string 

type Parser<'T> = Parser of (string -> Result<'T * string>)


let run parser input = 
    // unwrap parser to get inner function
    let (Parser innerFn) = parser 
    // call inner function with input
    innerFn input

let pchar charToMatch = 
    // define a nested inner function
    let innerFn str =
        if String.IsNullOrEmpty(str) then
            Failure "No more input"
        else
            let first = str.[0] 
            if first = charToMatch then
                let remaining = str.[1..]
                Success (charToMatch,remaining)
            else
                let msg = sprintf "Expecting '%c'. Got '%c'" charToMatch first
                Failure msg
    // return the inner function
    Parser innerFn 



(*** PARSER TESTS ***)
"ABC" |> run (pchar 'A') 
"ABC" |> run (pchar 'Z') 










let andThen parser1 parser2 =
    let innerFn input =
        // run parser1 with the input
        let result1 = run parser1 input
        
        // test the result for Failure/Success
        match result1 with
        | Failure err -> 
            // return error from parser1
            Failure err  

        | Success (value1,remaining1) -> 
            // run parser2 with the remaining input
            let result2 =  run parser2 remaining1
            
            // test the result for Failure/Success
            match result2 with 
            | Failure err ->
                // return error from parser2 
                Failure err 
            
            | Success (value2,remaining2) -> 
                // combine both values as a pair
                let newValue = (value1,value2)
                // return remaining input after parser2
                Success (newValue,remaining2)

    // return the inner function
    Parser innerFn 
let ( .>>. ) = andThen



(*** PARSER TESTS ***)
let parseA = pchar 'A'
let parseB = pchar 'B'
let parseAThenB = parseA .>>. parseB 

"ABC" |> run parseAThenB    // Success (('A', 'B'), "C")
"ZBC" |> run parseAThenB    // Failure "Expecting 'A'. Got 'Z'"
"AZC" |> run parseAThenB    // Failure "Expecting 'B'. Got 'Z'"










let orElse parser1 parser2 =
    let innerFn input =
        // run parser1 with the input
        let result1 = run parser1 input

        // test the result for Failure/Success
        match result1 with
        | Success result -> 
            // if success, return the original result
            result1

        | Failure err -> 
            // if failed, run parser2 with the input
            let result2 = run parser2 input

            // return parser2's result
            result2 

    // return the inner function
    Parser innerFn 

let ( <|> ) = orElse



(*** PARSER TESTS ***)
let parseAOrElseB = parseA <|> parseB

"AZZ" |> run parseAOrElseB  // Success ('A', "ZZ")
"BZZ" |> run parseAOrElseB  // Success ('B', "ZZ")
"CZZ" |> run parseAOrElseB  // Failure "Expecting 'B'. Got 'C'"









let choice listOfParsers = 
    List.reduce ( <|> ) listOfParsers 

let anyOf listOfChars = 
    listOfChars
    |> List.map pchar // convert into parsers
    |> choice



(*** PARSER TESTS ***)

let parseLowercase = anyOf ['a'..'z']

run parseLowercase "aBC"    // Success ('a', "BC")
run parseLowercase "ABC"    // Failure "Expecting 'z'. Got 'A'"



let parseDigit = anyOf ['0'..'9']

run parseDigit "1ABC"       // Success ("1", "ABC")
run parseDigit "9ABC"       // Success ("9", "ABC")
run parseDigit "|ABC"       // Failure "Expecting '9'. Got '|'"
