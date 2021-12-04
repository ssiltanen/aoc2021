#time

let input =
    System.IO.File.ReadAllLines "data/day02.txt"

let (|Up|Down|Forward|) (input: string) =
    match input.Split(' ') with
    | [| "up"; value |] -> Up(int value)
    | [| "down"; value |] -> Down(int value)
    | [| "forward"; value |] -> Forward(int value)
    | _ -> failwithf "Unexpected input: %s" input

input
|> Array.fold
    (fun (x, y) cmd ->
        match cmd with
        | Up value -> x, max (y - value) 0
        | Down value -> x, y + value
        | Forward value -> x + value, y)
    (0, 0)
|> fun (x, y) -> x * y
|> printfn "Day2-1 %i"

input
|> Array.fold
    (fun (x, y, aim) cmd ->
        match cmd with
        | Up value -> x, y, max (aim - value) 0
        | Down value -> x, y, aim + value
        | Forward value -> x + value, y + aim * value, aim)
    (0, 0, 0)
|> fun (x, y, _) -> x * y
|> printfn "Day2-2 %i"
