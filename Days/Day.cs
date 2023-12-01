namespace AOC2023Backend.Days
{
    public abstract class Day
    {
        public static Day DayFactory(int number)
        {
            switch (number)
            {
                case 1: return new DayOne();
                //case 2: return new DayTwo();
                //case 3: return new DayThree();
                default:
                    throw new Exception("That day isn't ready yet");
            }
        }

        public Answer PartOneAndTwo(string input)
        {
            return new Answer()
            {
                PartOneAnswer = PartOne(input),
                PartTwoAnswer = PartTwo(input)
            };
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

    public class Answer
    {
        public string PartOneAnswer { get; set; }
        public string PartTwoAnswer { get; set; }
    }
}
