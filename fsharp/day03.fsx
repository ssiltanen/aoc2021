#time

type Rating = Oxygen | CO2

let input =
    System.IO.File.ReadAllLines "data/day03.txt"
    |> Array.map (seq >> Array.ofSeq)
  
let bitsToInt (bits: string) = 
  System.Convert.ToInt32(bits, 2)

let multiplyWithInvertedBits value =
  let allOnes =
    bitsToInt((System.String input[0]).Replace("0","1"))
  value * (value ^^^ allOnes)
  
let countBits = Array.countBy id
let mostCommon = Array.maxBy snd >> fst
let leastCommon = Array.minBy snd >> fst
let bitCriteria rating bits = 
  let counts = countBits bits
  let even = snd counts[0] = snd counts[1]
  match rating with
  | Oxygen when even -> '1'
  | Oxygen -> mostCommon counts
  | CO2 when even -> '0'
  | CO2 -> leastCommon counts

input
|> Array.transpose
|> Array.map (countBits >> mostCommon)
|> System.String
|> bitsToInt
|> multiplyWithInvertedBits
|> printfn "Day3-1 %i"

let calculateRating rating =
  let folder = 
    fun report i ->
      if Array.length report = 1 then report
      else
        let transpose = Array.transpose report
        let bit = bitCriteria rating transpose[i]
        report |> Array.where (fun bits -> bits[i] = bit)

  [| 0 .. Array.length input[0] - 1 |]
  |> Array.fold folder input
  |> Array.head
  |> System.String
  |> bitsToInt

calculateRating Oxygen * calculateRating CO2
|> printfn "Day3-2 %i"
