type Boolean = True | False
type Color = Red = 1 | Blue =2 | Green = 3


//#region Multiplied (AND) types: tuple and record

type Tuple = Boolean * Color
type Record = { b : Boolean; c : Color }

(*
     all possible values (2 x 6):
        (True, Red)  ; (True, Blue)  ; (True, Green)
        (False, Red) ; (False, Blue) ; (False, Green)
*)

//#endregion


//#region Sum (OR) types: disriminated unions

type Union = | B of Boolean | C of Color

(*
     all possible values (2 + 3):
        True ; False
        Red ; Green ; Blue
*)

//#endregion