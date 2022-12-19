open System
open System.IO

let input = @"C:\git\repos\public\aoc-2022\03 - day 3\input_part_1.txt"

[<EntryPoint>]
let main _ =
    let chars = seq { yield [|'a'..'z'|]; yield [|'A'..'Z'|] } |> Array.concat

    let result = task {
        let! lines = File.ReadAllLinesAsync(input)

        let sumPriority = Seq.sum <| seq {
            for s in lines do
                let mutable i, j, found = 0, s.Length / 2, false
                let mutable item : char option = None

                while i < s.Length / 2 && not(found) do
                    while j < s.Length && not(found) do
                        if s.[i] = s.[j] then
                            found <- true
                            item  <- Some(s.[j])
                        j <- j + 1

                    j <- s.Length / 2
                    i <- i + 1

                yield 1 + Array.findIndex(fun x -> x = item.Value) chars
        }
        return sumPriority
    }
    Console.WriteLine($"Solution: {result.Result}")
    0