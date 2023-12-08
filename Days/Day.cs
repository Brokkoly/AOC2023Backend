namespace AOC2023Backend.Days
{
    public abstract class Day
    {
        public static Day DayFactory(int number)
        {
            switch (number)
            {
                case 1: return new DayOne();
                case 2: return new DayTwo();
                case 3: return new DayThree();
                case 4: return new DayFour();
                case 5: return new DayFive();
                case 6: return new DaySix();
                case 7: return new DaySeven();
                case 8: return new DayEight();
                case 9: return new DayNine();
                case 10: return new DayTen();
                case 11: return new DayEleven();
                case 12: return new DayTwelve();
                case 13: return new DayThirteen();
                case 14: return new DayFourteen();
                case 15: return new DayFifteen();
                case 16: return new DaySixteen();
                case 17: return new DaySeventeen();
                case 18: return new DayEighteen();
                case 19: return new DayNineteen();
                case 20: return new DayTwenty();
                case 21: return new DayTwentyOne();
                case 22: return new DayTwentyTwo();
                case 23: return new DayTwentyThree();
                case 24: return new DayTwentyFour();
                default:
                    throw new Exception("That day isn't ready yet");
            }
        }

        public abstract string PartOne(string input);

        public abstract string PartTwo(string input);

        public static List<string> ParseInput(string input)
        {
            var lines = input.Split("\n").ToList();
            if (lines[lines.Count - 1].Length == 0)
            {
                lines.RemoveAt(lines.Count - 1);
            }
            return lines;
        }
        public static List<List<char>> ParseInput2D(string input)
        {
            var lines = ParseInput(input);
            return lines.Select(line =>
            {
                return line.ToList();
            }).ToList();
        }

    }

}
