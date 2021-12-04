#time

let input =
    System.IO.File.ReadAllLines "data/day01.txt"
    |> Array.map int

let length = Array.length input

[ 1; 3 ]
|> List.iteri (fun i window -> 
    Array.zip input[..length - window - 1] input[window..]
    |> Array.sumBy (fun (x,y) -> System.Convert.ToInt32(x<y))
    |> printfn "Day1-%i %i" (i+1))