using Microsoft.AspNetCore.Components.Forms;

namespace AOC2023Backend.Days
{
    public class DayOne : Day
    {
        public override string PartOne(string input)
        {
            int maxElf = 0;
            int currentElf = 0;
            var inputLines = ParseInput(input);
            for (int i = 0; i <= inputLines.Count; i++)
            {
                if (i == inputLines.Count || inputLines[i].Length == 0)
                {
                    if (maxElf < currentElf)
                    {
                        maxElf = currentElf;
                    }
                    currentElf = 0;
                }
                else
                {
                    currentElf += int.Parse(inputLines[i]);
                }
            }

            return maxElf.ToString();
        }

        public override string PartTwo(string input)
        {
            List<int> elves = new List<int>();
            int currentElf = 0;
            var inputLines = ParseInput(input);
            for (int i = 0; i <= inputLines.Count; i++)
            {
                if (i == inputLines.Count || inputLines[i].Length == 0)
                {
                    elves.Add(currentElf);
                    currentElf = 0;
                }
                else
                {
                    currentElf += int.Parse(inputLines[i]);
                }
            }

            elves.Sort();

            return (elves[elves.Count - 1] + elves[elves.Count - 2] + elves[elves.Count - 3]).ToString();
        }
    }
}
