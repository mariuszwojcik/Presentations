#r "bin/FSharp.Compiler.CodeDom.dll"

open FSharp.Compiler.CodeDom
open System.CodeDom.Compiler

let MODULE_SOURCE_CODE = @"
module CodeDomTest

let ping = ""pong""
    "


let generateAssembly sourceCode =
    let provider = new FSharpCodeProvider()
    let options = CompilerParameters([||])
    options.GenerateInMemory <- true
    //options.ReferencedAssemblies.Add("TravelRepublic.Meta.BidRecommender.dll") |> ignore
    let compilationResult = provider.CompileAssemblyFromSource(options, [| sourceCode |])
    compilationResult.CompiledAssembly

let assembly = generateAssembly MODULE_SOURCE_CODE
let testType  = assembly.GetType("CodeDomTest")
testType.GetProperty("ping").GetValue(null)
