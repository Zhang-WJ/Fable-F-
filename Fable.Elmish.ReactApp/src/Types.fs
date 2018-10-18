module Type

type Msg =
  | Increment
  | Decrement

type Model = int

let init () : Model = 0
