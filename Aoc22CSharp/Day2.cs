namespace Aoc22CSharp;

internal class Day2
{
    enum Shape
    {
        Rock,
        Paper,
        Scissors
    }

    enum Result
    {
        Win,
        Lose,
        Draw
    }

    record struct StrategyPlay
    (
        Shape Opponent,
        Shape Player
    );

    record struct StrategyPlayResult
    (
        Shape Opponent,
        Result Result
    );

    public int Solve1()
    {
        var path = "input.txt";
        var strategies = ReadFromInput(path);

        return strategies.Select(GetScoreForPlay).Sum();
    }

    public int Solve2()
    {
        var path = "input.txt";
        var strategiesResults = ReadResultsFromInput(path);

        return strategiesResults.Select(GetScoreForPlayResult).Sum();
    }

    private int GetScoreForPlay(StrategyPlay play)
    {
        return GetScoreForOutcome(play) + GetScoreForPlayerShape(play.Player);
    }

    private int GetScoreForPlayResult(StrategyPlayResult playResult)
    {
        var playerShape = GetPlayerShape(playResult);
        return GetScoreForOutcome(playResult.Result) + GetScoreForPlayerShape(playerShape);
    }

    private Shape GetPlayerShape(StrategyPlayResult playResult)
    {
        return playResult.Result switch
        {
            Result.Draw => playResult.Opponent,
            Result.Win => GetWinningShape(playResult.Opponent),
            Result.Lose => GetLosingShape(playResult.Opponent),
            _ => throw new Exception()
        };
    }

    private Shape GetWinningShape(Shape opponentShape)
    {
        return opponentShape switch
        {
            Shape.Rock => Shape.Paper,
            Shape.Paper => Shape.Scissors,
            Shape.Scissors => Shape.Rock,
            _ => throw new NotImplementedException()
        };
    }

    private Shape GetLosingShape(Shape opponentShape)
    {
        return opponentShape switch
        {
            Shape.Rock => Shape.Scissors,
            Shape.Paper => Shape.Rock,
            Shape.Scissors => Shape.Paper,
            _ => throw new ArgumentOutOfRangeException(nameof(opponentShape), opponentShape, null)
        };
    }

    private int GetScoreForOutcome(Result result)
    {
        return result switch
        {
            Result.Win => 6,
            Result.Draw => 3,
            Result.Lose => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(result), result, null)
        };
    }

    private int GetScoreForOutcome(StrategyPlay play)
    {
        int losingScore = 0;
        int drawScore = 3;
        int winningScore = 6;

        if (play.Player == play.Opponent)
        {
            return drawScore;
        }

        if (play.Player == Shape.Rock && play.Opponent == Shape.Scissors)
        {
            return winningScore;
        }

        if (play.Player == Shape.Paper && play.Opponent == Shape.Rock)
        {
            return winningScore;
        }

        if (play.Player == Shape.Scissors && play.Opponent == Shape.Paper)
        {
            return winningScore;
        }

        return losingScore;
    }

    private int GetScoreForPlayerShape(Shape playerShape)
    {
        return playerShape switch
        {
            Shape.Rock => 1,
            Shape.Paper => 2,
            Shape.Scissors => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(playerShape), playerShape, null)
        };
    }

    private List<StrategyPlay> ReadFromInput(string path)
    {
        var lines = File.ReadAllLines(path);
        var result = new List<StrategyPlay>();

        foreach (var line in lines)
        {
            var play = ReadPlayFromLine(line);
            result.Add(play);
        }

        return result;
    }

    private List<StrategyPlayResult> ReadResultsFromInput(string path)
    {
        var lines = File.ReadAllLines(path);
        var result = new List<StrategyPlayResult>();

        foreach (var line in lines)
        {
            var playResult = ReadPlayResultFromLine(line);
            result.Add(playResult);
        }

        return result;
    }

    private StrategyPlayResult ReadPlayResultFromLine(string line)
    {
        var parts = line.Split(" ");
        var play = ParseOpponentShape(parts[0]);
        var result = ReadResultFromLine(parts[1]);

        return new StrategyPlayResult(play, result);
    }

    private Result ReadResultFromLine(string line)
    {
        return line switch
        {
            "X" => Result.Lose,
            "Y" => Result.Draw,
            "Z" => Result.Win,
            _ => throw new ArgumentOutOfRangeException(nameof(line), line, null)
        };
    }

    private StrategyPlay ReadPlayFromLine(string line)
    {
        var parts = line.Split(" ");

        var opponentShape = ParseOpponentShape(parts[0]);
        var playerShape = ParsePlayerShape(parts[1]);

        var play = new StrategyPlay(opponentShape, playerShape);
        return play;
    }

    private Shape ParseOpponentShape(string shape)
    {
        return shape switch
        {
            "A" => Shape.Rock,
            "B" => Shape.Paper,
            "C" => Shape.Scissors,
            _ => throw new Exception("Unknown shape")
        };
    }

    private Shape ParsePlayerShape(string shape)
    {
        return shape switch
        {
            "X" => Shape.Rock,
            "Y" => Shape.Paper,
            "Z" => Shape.Scissors,
            _ => throw new Exception("Unknown shape")
        };
    }
}
