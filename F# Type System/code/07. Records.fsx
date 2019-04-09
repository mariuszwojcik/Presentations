type GeoCoord = { lat: float; long: float }
let myGeoCoord = { lat = 1.1; long = 2.2 }
let myGeoCoord' = { long = 2.2; lat = 1.1 }


//#region deconstruct

let { lat=myLat; long=myLong } = myGeoCoord

// partial deconstruction
let { lat=_; long=myLong2 } = myGeoCoord
let { long=myLong3 } = myGeoCoord
let long = myGeoCoord.long

// func parameters pattern matching
let show { lat=l1; long=l2 } = 
    printfn "Long %f x %f" l1 l2

show myGeoCoord

//#endregion


//#region pretty print

printfn "%A" myGeoCoord
printfn "My coordinates: %A" myGeoCoord

//#endregion


//#region Records equality

let coords1 = { lat = 1.1; long = 2.2 }
let coords2 = { lat = 1.1; long = 2.2 }

coords1 = coords2


type ComplexRecord = { id: int; name: string; polygon: GeoCoord list }

let r1 = { id = 3; name = "square"; polygon = [ { lat = 1.; long = 1. } ; { lat = 5.; long=5. } ] }
let r2 = { id = 3; name = "square"; polygon = [ { lat = 1.; long = 1. } ; { lat = 5.; long=5. } ] }

r1=r2

//#endregion


//#region HashCode

myGeoCoord.GetHashCode()


let c1 = { lat = 1.1; long = 2.2 }
let c2 = { lat = 1.1; long = 2.2 }

let d = new System.Collections.Generic.Dictionary<GeoCoord, int>()
d.Add(c1, 1)
d.[c1]

d.Add(c2, 2)

c1.GetHashCode()
c2.GetHashCode()

d.[c2]

//#endregion


//#region type inference

type CountryRecord = { id: int; name: string }
type EstabRecord = { id: int; name: string }


let france1 = { id = 334 ; name = "France"}

let france2 = { CountryRecord.id = 334 ; name = "France"}
let france3 : CountryRecord = { id = 334 ; name = "France"}

module DTOs =
    type CountryRecord = { id: int; name: string }

    let france4 = { id = 334 ; name = "France"}

france2 = france3
france2 = DTOs.france4

let france5 : CountryRecord = { id = 334 ; name = "France"}
france2 = france5

//#endregion

