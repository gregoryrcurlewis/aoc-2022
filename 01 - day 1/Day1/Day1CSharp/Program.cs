internal class Program
{
    private const string InputFile = @"C:\git\repos\public\aoc-2022\01 - day 1\input_part_1.txt";

    private static async Task Main(string[] args)
    {
        // https://adventofcode.com/2022/day/1

        await using var fs = new FileStream(InputFile, FileMode.Open);
        using var sr = new StreamReader(fs);

        var max = 0;
        var cur = 0;

        while (!sr.EndOfStream)
        {
            var line = await sr.ReadLineAsync();

            if (line == string.Empty)
            {
                if (cur > max)
                {
                    max = cur;
                }

                cur = 0;
                continue;
            }

            cur += int.Parse(line!);
        }

        Console.WriteLine($"Solution: {max}");
        Console.ReadKey();
    }
}