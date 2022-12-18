open System
open System.Collections.Generic
open System.IO

type Outcome   =
    | Loss     = 0
    | Draw     = 3
    | Win      = 6

type Choice    =
    | Rock     = 1
    | Paper    = 2
    | Scissors = 3

type Game =
    | Round of them : Choice * outcome : Outcome

let playRound(round: Game) : int =
    match round with
    | Round(Choice.Rock, Outcome.Loss)     -> int Choice.Scissors + int Outcome.Loss
    | Round(Choice.Paper, Outcome.Loss)    -> int Choice.Rock     + int Outcome.Loss
    | Round(Choice.Scissors, Outcome.Loss) -> int Choice.Paper    + int Outcome.Loss

    | Round(Choice.Rock, Outcome.Draw)     -> int Choice.Rock     + int Outcome.Draw
    | Round(Choice.Paper, Outcome.Draw)    -> int Choice.Paper    + int Outcome.Draw
    | Round(Choice.Scissors, Outcome.Draw) -> int Choice.Scissors + int Outcome.Draw

    | Round(Choice.Rock, Outcome.Win)      -> int Choice.Paper    + int Outcome.Win
    | Round(Choice.Paper, Outcome.Win)     -> int Choice.Scissors + int Outcome.Win
    | Round(Choice.Scissors, Outcome.Win)  -> int Choice.Rock     + int Outcome.Win

    | case ->
        failwith $"Unexpected case: {case}";

let elfChoice = Dictionary<char, Choice>(
    [
        KeyValuePair('A', Choice.Rock);
        KeyValuePair('B', Choice.Paper);
        KeyValuePair('C', Choice.Scissors)
    ])

let requiredOutcome = Dictionary<char, Outcome>(
    [
        KeyValuePair('X', Outcome.Loss);
        KeyValuePair('Y', Outcome.Draw);
        KeyValuePair('Z', Outcome.Win)
    ])

let input = @"C:\git\repos\public\aoc-2022\02 - day 2\input_part_1.txt"

[<EntryPoint>]
let main argv =
    let x = task {
        use fs = new FileStream(input, FileMode.Open)
        use sr = new StreamReader(fs)

        let mutable score = 0

        while not(sr.EndOfStream) do
            let! line = sr.ReadLineAsync()
            let them = elfChoice.[line.[0]]
            let mine = requiredOutcome.[line.[2]]
            let result = Round(them, mine) |> playRound
            score <- score + result

        return score
    }

    Console.WriteLine(x.Result.ToString())
    0