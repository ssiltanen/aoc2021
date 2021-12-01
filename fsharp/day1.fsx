#time

let input = 
    System.IO.File.ReadAllLines "data/day01.txt"
    |> Array.map int

let countIncreasingValues =
    Array.pairwise 
    >> Array.where (fun (a,b) -> b > a) 
    >> Array.length

// Part 1
input |> countIncreasingValues |> printfn "Day1-1 %i"

// Part 2
input
|> Array.windowed 3 
|> Array.map Array.sum
|> countIncreasingValues
|> printfn "Day1-2 %i"