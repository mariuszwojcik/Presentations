// This example was taken from great article "Understanding Parser Combinators" by Scott Wlaschin
//  https://fsharpforfunandprofit.com/posts/understanding-parser-combinators/

open System

let A_Parser str =
    if String.IsNullOrEmpty(str) then
        (false,"")
    else if str.[0] = 'A' then
        let remaining = str.[1..]
        (true,remaining)
    else
        (false,str)


(*** PARSER TESTS ***)
A_Parser "ABC"
// val it: bool * string = (true, "BC")

A_Parser "ZBC"
// val it: bool * string = (false, "ZBC")
