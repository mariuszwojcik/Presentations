
"RRRRSSSSRRRRRRRRRRRRRRRRRRRSSSSSRR"
|> Seq.windowed 2
|> Seq.groupBy(fun a -> a.[0])
|> Seq.map(fun (k, e) -> (k,e |> Seq.map(Seq.skip 1 >> Seq.head)))
|> Seq.map(fun (c, i) -> printfn "%A -> %s" c (i |> Seq.map(string) |> String.concat ""))

//#region build probability matrix

let calcTransitionProbability items =
    let total = items |> Seq.length
    items
    |> Seq.groupBy(id)
    |> Seq.map(fun (g,i) -> g, float (i |> Seq.length) / float total)

"RRRRSSSSRRRRRRRRRRRRRRRRRRRSSSSSRR"
|> Seq.windowed 2
|> Seq.groupBy(fun a -> a.[0])
|> Seq.map(fun (k, e) -> (k,e |> Seq.map(Seq.skip 1 >> Seq.head) |> calcTransitionProbability))

//#endregion


//#region Generator 

let generate observations =

    let transitions =
        observations
        |> Seq.windowed 2
        |> Seq.groupBy(fun a -> a.[0])
        |> Seq.map(fun (k, e) -> (k,e |> Seq.map(Seq.skip 1 >> Seq.head) |> Seq.toArray))
        |> Map.ofSeq

    let r = System.Random()

    let nextEvent c =
        let toStates = transitions.[c]
        toStates.[r.Next(toStates.Length)]

    Seq.unfold(fun s -> Some (s, nextEvent s)) 'R'

"RRRRSSSSRRRRRRRRRRRRRRRRRRRSSSSSRR" |> generate |> Seq.take 100 |> Seq.map(string) |> String.concat ""

//#endregion

