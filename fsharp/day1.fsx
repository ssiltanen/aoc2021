#time

let input =
    System.IO.File.ReadAllLines "data/day01.txt"
    |> Array.map int

let countIncreasingValues =
    Array.pairwise
    >> Array.where (fun (a, b) -> b > a)
    >> Array.length

input
|> countIncreasingValues
|> printfn "Day1-1 %i"

input
|> Array.windowed 3
|> Array.map Array.sum
|> countIncreasingValues
|> printfn "Day1-2 %i"
