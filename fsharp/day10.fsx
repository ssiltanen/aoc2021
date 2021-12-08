# time

let input =
  System.IO.File.ReadAllLines "data/day10.txt"
  |> Array.map (seq >> Seq.toList)

let validate brackets =
  let rec parse parsed leftover =
    match leftover with
    | [] -> List.rev parsed, None
    | next :: tail ->
      if next = ')' || next = '}' || next = ']' || next = '>' then
        match parsed with
        | [] -> List.rev parsed, Some next
        | last :: _ ->
          match last, next with
          | '(', ')' | '{', '}' | '[', ']' | '<', '>' -> parse (List.tail parsed) tail
          | _ -> List.rev parsed, Some next
      else
        parse (next :: parsed) tail
  parse [] brackets

input 
|> Array.choose (validate >> snd)
|> Array.sumBy (function ')' -> 3 | ']' -> 57 | '}' -> 1197 | '>' -> 25137 | _ -> 0)
|> printfn "Day10-1 %i"

input 
|> Array.choose (validate >> function
  | unfinished, None -> Some unfinished
  | _-> None)
|> Array.map (
  List.rev 
  >> List.map (function '(' -> 1L | '[' -> 2L | '{' -> 3L | '<' -> 4L | _ -> 0L)
  >> List.reduce (fun a b -> a * 5L + b))
|> Array.sort
|> fun arr -> arr[Array.length arr / 2]
|> printfn "Day10-2 %i"