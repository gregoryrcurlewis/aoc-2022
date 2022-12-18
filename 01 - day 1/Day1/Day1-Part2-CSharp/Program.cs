internal class Program
{
    private const string InputFile = @"C:\git\repos\public\aoc-2022\01 - day 1\input_part_1.txt";

    private static async Task Main(string[] args)
    {
        // https://adventofcode.com/2022/day/1

        await using var fs = new FileStream(InputFile, FileMode.Open);
        using var sr = new StreamReader(fs);

        var top = new[] { 0, 0, 0 };
        var cur = 0;

        void UpdateTop3(int newVal)
        {
            for (var i = 0; i < top.Length; i++)
            {
                if (newVal <= top[i]) 
                    continue;
                
                for (var j = top.Length - 1; j > i; j--)
                {
                    top[j] = top[j - 1];
                }

                top[i] = newVal;
                break;
            }
        }

        while (!sr.EndOfStream)
        {
            var line = await sr.ReadLineAsync();

            if (line == string.Empty)
            {
                UpdateTop3(cur);
                cur = 0;
                continue;
            }

            cur += int.Parse(line!);
        }

        Console.WriteLine($"Solution: #1: {top[0]}, #2: {top[1]}, #3: {top[2]}");
        Console.WriteLine($"Total: {top.Sum()}");
        Console.ReadKey();
    }
}