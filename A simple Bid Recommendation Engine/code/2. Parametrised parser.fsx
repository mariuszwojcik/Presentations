// Based on great article "Understanding Parser Combinators" by Scott Wlaschin
// https://fsharpforfunandprofit.com/posts/understanding-parser-combinators/

open System

let pchar charToMatch str =
    if String.IsNullOrEmpty(str) then
        let msg = "No more input"
        (msg,"")
    else 
        let first = str.[0] 
        if first = charToMatch then
            let remaining = str.[1..]
            let msg = sprintf "Found %c" charToMatch
            (msg,remaining)
        else
            let msg = sprintf "Expecting '%c'. Got '%c'" charToMatch first
            (msg,str)


(*** PARSER TESTS ***)
"ABC" |> pchar 'A' 
// val it : string * string = ("Found A", "BC")

"ZBC" |> pchar 'A' 
// val it : string * string = ("Expecting 'A'. Got 'Z'", "ZBC")
