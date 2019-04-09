type Color =
   | Red = 0
   | Green = 1
   | Blue = 2

let col1 : Color = Color.Red

//#region pretty pring

printfn "%A" col1

//#endregion


//#region specifying underlying type

type UColor =
   | Red = 0u
   | Green = 1u
   | Blue = 2u

//#endregion


//#region  Conversion to an integral type.

let n = int col1

//#endregion


//#region Create enum from other value than predefined

let col2 = enum<Color>(3)
printfn "%A" col2

// for enums with unerlying type different than int
let col3 = Microsoft.FSharp.Core.LanguagePrimitives.EnumOfValue<uint32, UColor>(2u)

//#endregion