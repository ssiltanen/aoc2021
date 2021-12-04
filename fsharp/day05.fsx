# time

let task1, task2 = 
  System.IO.File.ReadAllLines "data/day05.txt"
  |> Array.map (fun row ->
    let arr = row.Split([|" -> "; ","|], System.StringSplitOptions.None) |> Array.map int
    arr[0], arr[1], arr[2], arr[3])
  |> Array.partition (fun (x1,y1,x2,y2) -> x1 = x2 || y1 = y2)

let line (x1: int, y1: int, x2: int, y2: int) =
  let step(a,b) = if a < b then 1 else -1
  ([| x1..step(x1,x2)..x2 |], [| y1..step(y1,y2)..y2 |])
  ||> if x1 = x2 || y1 = y2 then Array.allPairs else Array.zip

let countDuplicates = 
  Array.countBy id
  >> Array.sumBy (fun (_,count) -> System.Convert.ToInt32(count >= 2))

[| task1; task2 |]
|> Array.fold 
  (fun prevLines coordinates ->
    let lines = coordinates |> Array.fold (fun acc c -> line(c) |> Array.append acc) Array.empty
    let overlaps = Array.append lines prevLines |> countDuplicates
    match prevLines with
    | [||] -> printfn "Day5-1 %i" overlaps
    | _ -> printfn "Day5-2 %i" overlaps
    lines)
  Array.empty