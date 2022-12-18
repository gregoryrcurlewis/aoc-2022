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
    | Round of them : Choice * mine : Choice

let playRound(round: Game) : int =
    match round with
    | Round(Choice.Rock, Choice.Rock)         -> int Choice.Rock     + int Outcome.Draw
    | Round(Choice.Paper, Choice.Rock)        -> int Choice.Rock     + int Outcome.Loss
    | Round(Choice.Scissors, Choice.Rock)     -> int Choice.Rock     + int Outcome.Win

    | Round(Choice.Rock, Choice.Paper)        -> int Choice.Paper    + int Outcome.Win
    | Round(Choice.Paper, Choice.Paper)       -> int Choice.Paper    + int Outcome.Draw
    | Round(Choice.Scissors, Choice.Paper)    -> int Choice.Paper    + int Outcome.Loss

    | Round(Choice.Rock, Choice.Scissors)     -> int Choice.Scissors + int Outcome.Loss
    | Round(Choice.Paper, Choice.Scissors)    -> int Choice.Scissors + int Outcome.Win
    | Round(Choice.Scissors, Choice.Scissors) -> int Choice.Scissors + int Outcome.Draw

    | case ->
        failwith $"Unexpected case: {case}";

let elfChoice = Dictionary<char, Choice>(
    [
        KeyValuePair('A', Choice.Rock);
        KeyValuePair('B', Choice.Paper);
        KeyValuePair('C', Choice.Scissors)
    ])

let myChoice = Dictionary<char, Choice>(
    [
        KeyValuePair('X', Choice.Rock);
        KeyValuePair('Y', Choice.Paper);
        KeyValuePair('Z', Choice.Scissors)
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
            let mine = myChoice.[line.[2]]
            let result = Round(them, mine) |> playRound
            score <- score + result

        return score
    }

    Console.WriteLine(x.Result.ToString())
    0