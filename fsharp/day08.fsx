# time

let input =
  System.IO.File.ReadAllLines "data/day08.txt"
  |> Array.map (fun arr -> arr.Split([| " "; "|" |], System.StringSplitOptions.RemoveEmptyEntries))

input
|> Array.collect (fun arr -> arr[10..])
|> Array.sumBy (String.length >> function 2 | 3 | 4 | 7 -> 1 | _ -> 0)
|> printfn "Day8-1 %i"

let getCodeNumber length intersectWithOne intersectWithFour extraInOne extraInFour =
  match length, intersectWithOne, intersectWithFour, extraInOne, extraInFour with
  | 6, 2, 3, 0, 1 -> 0
  | 5, 1, 2, 1, 2 -> 2
  | 5, 2, 3, 0, 1 -> 3
  | 5, 1, 3, 1, 1 -> 5
  | 6, 1, 3, 1, 1 -> 6
  | 6, 2, 4, 0, 0 -> 9
  | _ -> failwith "Invalid mapping"

input
|> Array.sumBy (fun row ->
    let sort = Seq.sort >> Seq.toArray >> System.String
    let pattern = row[..9] |> Array.map sort
    let output = row[10..] |> Array.map sort
    let mapping = Array.create 10 ""
    // Map first the obvious ones
    pattern |> Array.iter (fun code ->
      match String.length code with
      | 2 -> mapping[1] <- code
      | 3 -> mapping[7] <- code
      | 4 -> mapping[4] <- code
      | 7 -> mapping[8] <- code
      | _ -> ())

    // Map the harder ones next
    let one = Set.ofSeq mapping[1]
    let four = Set.ofSeq mapping[4]
    pattern 
    |> Array.where (fun code -> 
      let length = String.length code 
      length = 5 || length = 6)
    |> Array.iter (fun code ->
      let length = String.length code
      let characters = Set.ofSeq code
      let intersectWithOne = Set.intersect characters one |> Set.count
      let intersectWithFour = Set.intersect characters four |> Set.count
      let extraInOne = Set.difference one characters |> Set.count
      let extraInFour = Set.difference four characters |> Set.count
      let codeIndex = getCodeNumber length intersectWithOne intersectWithFour extraInOne extraInFour
      mapping[codeIndex] <- code)

    // Map output
    output
    |> Array.map (fun code -> mapping |> Array.findIndex ((=) code) |> string)
    |> String.concat ""
    |> int)
|> printfn "Day8-2 %i"