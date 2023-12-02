using System.Reflection.Metadata.Ecma335;

namespace AOC2023Backend.Days
{
    class Game
    {
        public int Id { get; set; }
        public int MaxBlue { get; set; } = 0;
        public int MaxGreen { get; set; } = 0;
        public int MaxRed { get; set; } = 0;
        public Game(string input)
        {
            var leftAndRight = input.Split(": ");
            Id = int.Parse(leftAndRight[0].Split(' ')[1]);
            var gameInstances = leftAndRight[1].Split("; ").Select(line => new GameInstance(line));
            foreach (GameInstance instance in gameInstances)
            {
                if (instance.Green > MaxGreen) MaxGreen = instance.Green;
                if (instance.Blue > MaxBlue) MaxBlue = instance.Blue;
                if (instance.Red > MaxRed) MaxRed = instance.Red;
            }
        }
    }
    class GameInstance
    {
        public int Blue { get; set; } = 0;
        public int Green { get; set; } = 0;
        public int Red { get; set; } = 0;
        public GameInstance(string input)
        {
            var cubes = input.Split(", ");
            foreach (var cube in cubes)
            {
                var numAndColor = cube.Split(' ');
                var num = int.Parse(numAndColor[0]);
                switch (numAndColor[1])
                {
                    case "red": Red = num; break;
                    case "blue": Blue = num; break;
                    case "green": Green = num; break;
                    default:
                        throw new Exception("Invalid Color");
                }
            }
        }
    }
    public class DayTwo : Day
    {
        public override string PartOne(string input)
        {
            var actualBlue = 14;
            var actualGreen = 13;
            var actualRed = 12;

            var inputLines = ParseInput(input);

            var games = inputLines.Select(line =>
            {
                return new Game(line);
            });

            var validGames = games.Where(game =>
            {
                if (game.MaxGreen > actualGreen) return false;
                if (game.MaxBlue > actualBlue) return false;
                if (game.MaxRed > actualRed) return false;
                return true;
            });

            var gamesTotal = 0;

            foreach (var game in validGames)
            {
                gamesTotal += game.Id;
            }
            return gamesTotal.ToString();
        }



        //private string WrapperForBoth(string input, Func<List<string>,string)

        public override string PartTwo(string input)
        {
            var inputLines = ParseInput(input);

            var games = inputLines.Select(line =>
            {
                return new Game(line);
            });

            var gamesTotal = 0;

            foreach (var game in games)
            {
                gamesTotal += game.MaxBlue * game.MaxGreen * game.MaxRed;
            }
            return gamesTotal.ToString();
        }
    }
}
