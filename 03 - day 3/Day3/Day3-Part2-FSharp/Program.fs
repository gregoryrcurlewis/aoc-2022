open System
open System.Collections.Generic
open System.IO
open System.Linq

let input = @"C:\git\repos\public\aoc-2022\03 - day 3\input_part_1.txt"

[<EntryPoint>]
let main _ =
    let chars = seq { yield [|'a'..'z'|]; yield [|'A'..'Z'|] } |> Array.concat

    let result = task {
        let group = Array.zeroCreate<string> 3

        use fs = new FileStream(input, FileMode.Open)
        use sr = new StreamReader(fs)

        let mutable lineNumber, sumPriority = 1, 0

        while not(sr.EndOfStream) do
            let! line = sr.ReadLineAsync()

            if lineNumber % 3 = 0 then
                group.[2] <- line

                let h1 = HashSet<char>(group.[0].ToCharArray())
                let h2 = HashSet<char>(group.[1].ToCharArray())
                let h3 = HashSet<char>(group.[2].ToCharArray())

                h1.IntersectWith(h2)
                h1.IntersectWith(h3)

                sumPriority <- sumPriority + 1 + Array.findIndex(fun x -> x = h1.First()) chars
            else
                group.[lineNumber % 3 - 1] <- line

            lineNumber <- lineNumber + 1

        return sumPriority
    }
    Console.WriteLine($"Solution: {result.Result}")
    0