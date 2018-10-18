module App.View

open Elmish
open Fable.Helpers.React.Props
open Type
open State
open Fable.Helpers.React

type Model = {
    Counters: Type.Model list
}

type Msg =
    | Insert
    | Remove
    | Modify of int*Type.Msg

let init (): Model =
    { Counters = []}

//View
let view model dispatch =

  let counterDispatch i msg = dispatch (Modify (i, msg))

  let counters =
    model.Counters
    |> List.mapi ( fun i c -> State.root c (counterDispatch i) )

  div [] [
          yield button [ OnClick (fun _ -> dispatch Remove) ] [  str "Remove" ]
          yield button [ OnClick (fun _ -> dispatch Insert) ] [ str "Add" ]
          yield! counters]

// UPDATE
let update (msg:Msg) (model:Model) =
    match msg with
    | Insert ->
        { Counters = Type.init() :: model.Counters }
    | Remove ->
        { Counters =
            match model.Counters with
            | [] -> []
            | x :: rest -> rest }
    | Modify (id, counterMsg) ->
        { Counters =
            model.Counters
            |> List.mapi (fun i counterModel ->
                if i = id then
                    State.update counterMsg counterModel
                else
                    counterModel) }

open Elmish.React
open Fable.AST.Babel

// App
Program.mkSimple init update view
|> Program.withReact "elmish-app"
|> Program.withConsoleTrace
|> Program.run
