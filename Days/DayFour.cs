namespace AOC2023Backend.Days
{
    public class DayFour : Day
    {
        public override string PartTwo(string input)
        {
            var inputLines = ParseInput(input);
            var pointTotal = 0;
            var numCopies = new List<int>();
            for (var j = 0; j < inputLines.Count; j++) numCopies.Add(1);
            for (var i = 0; i < inputLines.Count; i++)
            {
                var game = inputLines[i].Split(": ")[1].Trim();
                var winningNumbersAndMyNumbers = game.Split(" | ");
                var winningNumbers = winningNumbersAndMyNumbers[0].Split(" ")
                    .Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToArray();
                var myNumbers = winningNumbersAndMyNumbers[1].Split(" ")
                    .Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToArray();

                Dictionary<string, int> winners = new();
                foreach (var win in winningNumbers)
                {
                    winners.TryAdd(win, 0);
                }
                foreach (var num in myNumbers)
                {
                    if (winners.TryGetValue(num, out int value))
                    {
                        winners[num] = value + 1;
                    }
                }
                var numMatches = 0;
                foreach (var points in winners)
                {
                    numMatches += points.Value;
                }
                for (var forwardIndex = i + 1; forwardIndex < inputLines.Count && forwardIndex <= i + numMatches; forwardIndex++)
                {
                    numCopies[forwardIndex] += numCopies[i];
                }
                pointTotal += numCopies[i];
            }
            return pointTotal.ToString();
        }

        public override string PartOne(string input)
        {
            var inputLines = ParseInput(input);
            var pointTotal = 0;
            foreach (var line in inputLines)
            {
                var game = line.Split(": ")[1].Trim();
                var winningNumbersAndMyNumbers = game.Split(" | ");
                var winningNumbers = winningNumbersAndMyNumbers[0].Split(" ")
                    .Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToArray();
                var myNumbers = winningNumbersAndMyNumbers[1].Split(" ")
                    .Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToArray();

                Dictionary<string, int> winners = new();
                foreach (var win in winningNumbers)
                {
                    winners.TryAdd(win, 0);
                }
                foreach (var num in myNumbers)
                {
                    if (winners.TryGetValue(num, out int value))
                    {
                        winners[num] = value + 1;
                    }
                }
                var pointsForGame = 0;
                foreach (var points in winners)
                {
                    if (points.Value > 0)
                    {
                        pointsForGame = pointsForGame == 0 ? 1 : pointsForGame * 2;
                    }
                }
                pointTotal += pointsForGame;
            }
            return pointTotal.ToString();
        }
    }
}
