using System.Runtime.CompilerServices;

namespace AOC2023Backend.Days
{
    public class DayNine : Day
    {
        public override string PartOne(string input)
        {
            var lines = ParseInput(input);

            var full = lines.Select(line => line.Trim().Split(' ').Select(x => int.Parse(x))).ToList().Select(x => FindDifferencesAndNextValue(x.ToList()));
            var result = 0;
            foreach (var line in full)
            {
                result += line[line.Count - 1];
            }
            return result.ToString();
        }

        public override string PartTwo(string input)
        {
            var lines = ParseInput(input);

            var full = lines.Select(line => line.Trim().Split(' ').Select(x => int.Parse(x))).ToList().Select(x => FindDifferencesAndPreviousValue(x.ToList()));
            var result = 0;
            foreach (var line in full)
            {
                result += line[0];
            }
            return result.ToString();
        }

        public List<int> FindDifferencesAndNextValue(List<int> input)
        {
            var result = new List<int>();
            var AllLists = new List<List<int>> { input };
            var allZeroes = false;
            var currentLayer = 0;
            while (!allZeroes)
            {
                AllLists.Add(new List<int>());
                allZeroes = true;
                for (int i = 0; i < AllLists[currentLayer].Count - 1; i++)
                {
                    var diff = AllLists[currentLayer][i + 1] - AllLists[currentLayer][i];
                    if (diff != 0)
                    {
                        allZeroes = false;
                    }
                    AllLists[currentLayer + 1].Add(diff);
                }
                currentLayer++;
            }
            for (int i = AllLists.Count - 1; i > 0; i--)
            {
                var nextDiff = AllLists[i][AllLists[i].Count - 1];
                AllLists[i - 1].Add(AllLists[i - 1][AllLists[i - 1].Count - 1] + nextDiff);
            }
            return AllLists[0];
        }


        public List<int> FindDifferencesAndPreviousValue(List<int> input)
        {
            var result = new List<int>();
            var AllLists = new List<List<int>> { input };
            var allZeroes = false;
            var currentLayer = 0;
            while (!allZeroes)
            {
                AllLists.Add(new List<int>());
                allZeroes = true;
                for (int i = 0; i < AllLists[currentLayer].Count - 1; i++)
                {
                    var diff = AllLists[currentLayer][i] - AllLists[currentLayer][i + 1];
                    if (diff != 0)
                    {
                        allZeroes = false;
                    }
                    AllLists[currentLayer + 1].Add(diff);
                }
                currentLayer++;
            }
            for (int i = AllLists.Count - 1; i > 0; i--)
            {
                var nextDiff = AllLists[i][0];

                AllLists[i - 1].Insert(0, AllLists[i - 1][0] + AllLists[i][0]);
            }
            return AllLists[0];
        }
    }
}
