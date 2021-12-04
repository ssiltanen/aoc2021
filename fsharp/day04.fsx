# time

open System
type WinningOrder = First | Last
type BoardNumber = Marked | Unmarked of int with
  static member Unwrap = function Unmarked num -> num | _ -> 0
  static member MarkMatch picked = function 
    | Unmarked num when num = picked -> Marked 
    | x -> x

let input =
    (IO.File.ReadAllText "data/day04.txt").Split(Environment.NewLine)

let parseRow (row: string) = 
  row.Split(' ') 
  |> Array.where (String.IsNullOrEmpty >> not)
  |> Array.map (int >> Unmarked)

let width = input[2] |> parseRow |> Array.length

let picks = input[0].Split(',') |> Array.map int
let initialBoards = 
  input[2..]
  |> Array.where (String.IsNullOrEmpty >> not)
  |> Array.chunkBySize width
  |> Array.map (Array.map parseRow >> array2D)

let isWinningBoard (board: BoardNumber[,]) =
  let allMarked = Array.forall (function Marked -> true | _ -> false)
  let num2d = board |> Seq.cast<BoardNumber> |> Seq.chunkBySize width |> Array.ofSeq
  let hasWinningRow = num2d |> Array.exists allMarked
  let hasWinningCol = num2d |> Array.transpose |> Array.exists allMarked
  hasWinningRow || hasWinningCol

let getFinalScoreOfBoardthatWins order =
  let folder = fun (boards, win) pick ->
    match win with
    | Some _ -> boards, win
    | None ->
        let updatedBoards = boards |> Array.map (Array2D.map (BoardNumber.MarkMatch pick))
        match updatedBoards |> Array.tryFind isWinningBoard, order with
        | None, _ -> updatedBoards, None
        | Some win, First -> updatedBoards, Some (win, pick)
        | Some win, Last ->
          if Array.length boards = 1 then updatedBoards, Some (win, pick)
          else updatedBoards |> Array.where (isWinningBoard >> not), None

  picks
  |> Array.fold folder (initialBoards, None)
  |> snd
  |> Option.map (fun (board, lastPick) ->
    let unmarkedSum = board |> Seq.cast<BoardNumber> |> Seq.sumBy BoardNumber.Unwrap
    unmarkedSum * lastPick)

getFinalScoreOfBoardthatWins First |> Option.iter (printfn "Day4-1 %i")
getFinalScoreOfBoardthatWins Last |> Option.iter (printfn "Day4-2 %i")