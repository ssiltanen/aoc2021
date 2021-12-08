# time

let input =
  System.IO.File.ReadAllLines "data/day09.txt"
  |> Array.mapi (fun y row -> row |> Seq.mapi (fun x c -> (x,y), System.Char.GetNumericValue(c) |> int))
  |> Seq.collect id
  |> Seq.toArray
  
let map = Map.ofArray input

let adjacents (x,y) = [| x, y-1; x, y+1; x-1, y; x+1, y |]

let lowpoints =
  input
  |> Array.choose (fun (coord, value) -> 
    adjacents coord
    |> Array.choose map.TryFind
    |> Array.forall (fun adj -> adj > value)
    |> function true -> Some coord | false -> None)

lowpoints
|> Array.sumBy (fun coord -> map.Item coord + 1)
|> printfn "Day9-1 %i"

let basin lowpoint =
  let rec spreadSearch basin coord =
    adjacents coord
    |> Array.where (map.TryFind >> Option.exists (fun value -> value <> 9 && value > map.Item coord))
    |> Array.except basin
    |> function
    | [||] -> basin
    | adj -> adj |> Array.fold spreadSearch (Array.append adj basin)
  spreadSearch [| lowpoint |] lowpoint

lowpoints
|> Array.map (basin >> Array.length)
|> Array.sortDescending
|> Array.take 3
|> Array.reduce (*)
|> printfn "Day9-2 %i"