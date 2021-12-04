# time

let input = 
  (System.IO.File.ReadAllText "data/day06.txt").Split(',')
  |> Array.map int

let familytree =
  Array.unfold 
    (fun (school, days) ->
      if days = 256 then None
      else
        let updatedSchool = Array.append (Array.tail school) [| school[0] |]
        updatedSchool[6] <- updatedSchool[6] + school[0]
        Some ((school, days), (updatedSchool, days + 1)))
    ([| 1L; 0L; 0L; 0L; 0L; 0L; 0L; 0L; 0L |], 0)
  |> Array.map (fst >> Array.sum)

[80; 256]
|> List.iteri (fun i cutOffDays ->
  input
  |> Array.sumBy (fun days -> familytree[cutOffDays - days])
  |> printfn "Day6-%i %i" (i+1))