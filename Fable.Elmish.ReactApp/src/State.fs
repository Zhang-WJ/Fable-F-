module State
open Fable.Helpers.React.Props
open Type


let update msg (model:Model)=
  match msg with
  | Increment -> model + 1
  | Decrement -> model - 1

module R = Fable.Helpers.React
let root model dispatch =
    R.div []
        [R.button [OnClick (fun _ -> dispatch Decrement)] [R.str "-"]
         R.div [] [R.str (sprintf "%A" model)]
         R.button [OnClick (fun _ -> dispatch Increment)] [ R.str "+"]
        ]
