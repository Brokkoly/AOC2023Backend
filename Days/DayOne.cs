using Microsoft.AspNetCore.Components.Forms;

namespace AOC2023Backend.Days
{
    public class DayOne : Day
    {
        public override string PartOne(string input)
        {
            return WrapperForBoth(input, GetNumbersForLine);
        }

        private static string WrapperForBoth(string input, Func<string, int> numberGetter)
        {
            var lines = ParseInput(input);
            var nums = lines.Select(line =>
            {
                return numberGetter(line);
            });
            var total = 0;
            foreach (int num in nums)
            {
                total += num;
            }
            return total.ToString();
        }

        private static int GetNumbersForLine(string input)
        {
            char? front = null;
            char? back = null;

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsNumber(input[i]))
                {
                    if (front == null)
                    {
                        front = input[i];
                    }
                    back = input[i];
                }
            }
            if (front == null || back == null)
            {
                throw new Exception("There were no numbers in the line");
            }
            char[] chars = { (char)front, (char)back };
            return int.Parse(new string(chars));
        }

        public override string PartTwo(string input)
        {
            return WrapperForBoth(input, GetNumbersOrStringsFromLine);
        }
        private static int GetNumbersOrStringsFromLine(string inputLine)
        {
            char? front = null;
            char? back = null;
            List<string> validStarts = new() { "one", "1", "two", "2", "three", "3", "four", "4", "five", "5", "six", "6", "seven", "7", "eight", "8", "nine", "9", };
            for (int i = 0; i < inputLine.Length; i++)
            {
                var currentString = inputLine.Substring(i);
                foreach (var numAsStringOrDigit in validStarts)
                {
                    if (currentString.StartsWith(numAsStringOrDigit))
                    {
                        char newChar = GetCharFromString(numAsStringOrDigit);
                        if (front == null)
                        {
                            front = newChar;
                        }
                        break;
                    }
                }
                if (front != null)
                {
                    break;
                }
            }
            for (int i = inputLine.Length - 1; i >= 0; i--)
            {
                var currentString = inputLine.Substring(i);
                foreach (var numAsStringOrDigit in validStarts)
                {
                    if (currentString.StartsWith(numAsStringOrDigit))
                    {
                        char newChar = GetCharFromString(numAsStringOrDigit);
                        if (back == null)
                        {
                            back = newChar;
                        }
                        break;
                    }
                }
                if (back != null)
                {
                    break;
                }
            }
            if (front == null || back == null)
            {
                throw new Exception(String.Format("There were no numbers in the line ${0}", inputLine));
            }
            char[] chars = { (char)front, (char)back };
            return int.Parse(new string(chars));
        }

        private static char GetCharFromString(string input)
        {
            if (input.Length == 1)
            {
                return (char)input[0];
            }
            switch (input)
            {
                case "zero": return '0';
                case "one": return '1';
                case "two": return '2';
                case "three": return '3';
                case "four": return '4';
                case "five": return '5';
                case "six": return '6';
                case "seven": return '7';
                case "eight": return '8';
                case "nine": return '9';
                default: throw new Exception("A valid number was not passed");
            }
        }
    }
}
