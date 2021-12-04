# time

let input = 
  (System.IO.File.ReadAllText "data/day07.txt").Split(',')
  |> Array.map int
  |> Array.sort

let task1 = 
  let median = input[ Array.length input / 2 ]
  input |> Array.sumBy ((-) median >> abs)

let task2 =
  let triangular x = x * (x + 1) / 2
  [| Array.head input .. Array.last input |]
  |> Array.map (fun a -> input |> Array.sumBy ((-) a >> abs >> triangular))
  |> Array.min

task1 |> printfn "Day7-1 %i"
task2 |> printfn "Day7-2 %i"