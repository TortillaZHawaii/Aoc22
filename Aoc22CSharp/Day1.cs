namespace Aoc22CSharp;

internal class Day1
{
    private List<List<int>> ReadElfCalories(string path)
    {
        var lines = File.ReadAllLines(path);
        var result = new List<List<int>>();
        var elf = new List<int>();
        foreach (var line in lines)
        {
            if (line == "")
            {
                result.Add(elf);
                elf = new List<int>();
            }
            else
            {
                elf.Add(int.Parse(line));
            }
        }
        result.Add(elf);
        return result;
    }

    public int Solve1()
    {
        var elfCalories = ReadElfCalories("input.txt");

        return elfCalories.Select(elf => elf.Sum()).Max();
    }

    public int Solve2()
    {
        var elfCalories = ReadElfCalories("input.txt");

        return elfCalories.Select(elf => elf.Sum()).OrderByDescending(x => x).Take(3).Sum();
    }
}
