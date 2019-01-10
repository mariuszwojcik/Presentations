#r "bin/FParsecCs.dll"
#r "bin/FParsec.dll"

#load "RuleAST.fsx"

open RuleAST.RuleAST
open FParsec


let metricParser (metricsList : string list) =
    let token = 
        metricsList
        |> List.map(fun m -> (spaces >>. stringReturn m (Metric m)) .>> spaces)
        |> List.reduce (<|>)
    
    token .>> spaces

let valueParser : Parser<Operator, unit> = 
    let p = pfloat .>> spaces .>> skipString "%" |>> decimal |>> ConstantType.Percent |>> Constant
    let d = pfloat |>> decimal |>> Number |>> Constant
    (attempt p) <|> d .>> spaces

let mathOperatorParser : Parser<ArithmenticOperator, unit> = 
    let token = 
            stringReturn "+" Add
        <|> stringReturn "-" Subtract
        <|> stringReturn "*" Multiply
        <|> stringReturn "/" Divide

    token .>> spaces

let comparisonOperatorParser : Parser<ComparisonOperator, unit> =  
    let token =  attempt (stringReturn "<=" LessOrEqual)
                    <|> attempt (stringReturn ">=" MoreOrEqual)
                    <|> attempt (stringReturn "<>" NotEqual)
                    <|> attempt (stringReturn "=" Equal)
                    <|> attempt (stringReturn "<" LessThan)
                    <|> attempt (stringReturn ">" MoreThan)
    token .>> spaces

let parseRule metricNames rule =
    let metricParser = metricParser metricNames
    let metricOrValueParser = attempt metricParser <|> attempt valueParser
    let formulaParser = (metricOrValueParser .>> spaces) .>>. (many1 (mathOperatorParser .>>. metricOrValueParser .>> spaces)) |>> Formula
    let metricOrValueOrFormulaParser = attempt formulaParser <|> attempt metricParser <|> attempt valueParser
    let ruleParser = pipe3 metricOrValueOrFormulaParser comparisonOperatorParser metricOrValueOrFormulaParser (fun a b c -> (a,b,c))
    let ruleSeparator = pstringCI("and") .>> spaces
    let rulesLineParser = spaces >>. (ruleParser .>> spaces) .>>. (many (ruleSeparator >>. ruleParser .>> spaces)) .>> eof


    run rulesLineParser rule




let metricNames = [ "Clicks" ; "Cost" ; "Margin" ; "ROI" ]

parseRule metricNames "Clicks > 50"
parseRule metricNames "50 < Clicks"
parseRule metricNames "Clicks / 7 > 100"
parseRule metricNames "Clicks / 7 > 100 AND Cost < 500"
