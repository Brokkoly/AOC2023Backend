using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace AOC2023Backend.Days
{
    public class DayTwelve : Day
    {
        public override string PartOne(string input)
        {
            var lines = ParseInput(input);

            var springs = lines.Select(line => new Spring(line));

            var nums = springs.Select(spring =>
            {
                return GetNumPermutations(spring.InfoString, spring.DamagedSprings);
            }).ToList();
            var arrangementSum = 0;
            foreach (var num in nums)
            {
                arrangementSum += num;
            }
            return arrangementSum.ToString();
        }

        public override string PartTwo(string input)
        {
            var lines = ParseInput(input);

            var springs = lines.Select(line => new SpringPart2(line));

            //var nums = springs.Select(spring =>
            //{
            //    Dictionary<string, List<string>> previousData = new();
            //   // return GenerateVariationsPart2(spring.InfoString, ref previousData).Count;
            //}).ToList();
            var arrangementSum = 0;
            //foreach (var num in nums)
            //{
            //    arrangementSum += num;
            //}
            return arrangementSum.ToString();
        }

        //public int GeneratePossibleCombinations(string input, List<int> sequences)
        //{
        //    var cache = new List<List<List<int>>>();
        //    for (int inputIndex = 0; inputIndex < input.Length; inputIndex++)
        //    {
        //        for (int sequenceIndex = 0; sequenceIndex < sequences.Count; sequenceIndex++)
        //        {
        //            for(int )
        //        }
        //    }
        //}

        //public List<string> GenerateValidPermutations(string inputString, List<int> sequences, ref Dictionary<string, List<List<int>>> results)
        //{
        //    var stringsToTry = inputString.Contains('.') ? inputString.Split('.').Where(x => !string.IsNullOrEmpty(x)).ToList() : new List<string> { inputString };
        //    foreach (string s in stringsToTry)
        //    {
        //        var validSequenceRemains = new List<List<int>>();

        //        var ReturnedSequences = new List<List<int>>();

        //        if (results.ContainsKey(s))
        //        {
        //            ReturnedSequences = results[s];
        //        }
        //        else
        //        {
        //            //ReturnedSequences = //DoSomething
        //        }



        //    }
        //}
        //public int GenerateSequenceVariationDynamic(string inputString, List<int> remainingSequences, ref Dictionary<string, List<List<int>>> sequenceVariations)
        //{
        //    int retVal;
        //    if (sequenceVariations.ContainsKey(inputString))
        //    {
        //        return sequenceVariations[inputString].Count;
        //    }
        //    else
        //    {
        //        switch (inputString[0])
        //        {
        //            case '.':
        //                break;
        //            case '#':
        //                break;
        //            case '?':
        //                break;
        //            default:
        //                throw new Exception();
        //        }
        //        sequenceVariations.Add(inputString, retVal);
        //    }
        //}

        //public int GetSequenceVariationIfDot(string inputString, ref Dictionary<string, int> sequenceVariations)
        //{

        //}

        //public List<string> GenerateVariationsPart2(string inputString, string previousString, List<int> remainingSequences)
        //{
        //    var retStrings = new List<string>();

        //    if (inputString.Length == 0)
        //    {
        //        retStrings.Add("");
        //        return retStrings;
        //    }
        //    var brokenSequenceLength = previousString.Split('.').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Length).OrderByDescending(x => x).ToList();
        //    var brokenSequenceLengthAssumption = previousString.Length;
        //    if (!inputString.Contains('#') || !inputString.Contains('?') && remainingSequences.Count == 0)
        //    {
        //        retStrings.Add(inputString);
        //        return retStrings;
        //    }

        //    else if (inputString.Contains('#') && remainingSequences.Count == 0)
        //    {
        //        return retStrings;
        //    }
        //    else if (remainingSequences.Count > 0 && brokenSequenceLengthAssumption > remainingSequences[0])
        //    {
        //        return retStrings;
        //    }
        //    var firstChar = inputString[0];
        //    if (firstChar == '?')
        //    {
        //        if (remainingSequences.Count > 0)
        //        {
        //            var possiblesBroken = GenerateVariationsPart2(inputString[1..], previousString + '#', remainingSequences);
        //            retStrings.AddRange(possiblesBroken.Select(x => '#' + x));
        //        }

        //        //var brokenSequenceLength = previousString.Split('.').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Length).OrderByDescending(x => x).ToList();
        //        if (brokenSequenceLength.Count == 0 || remainingSequences.Count == 0)
        //        {
        //            //Nothing to check for this layer
        //            var recursionResult = GenerateVariationsPart2(inputString[1..], "", remainingSequences);
        //            retStrings.AddRange(recursionResult.Select(x => '.' + x));
        //        }
        //        else if (brokenSequenceLength[0] == remainingSequences[0])
        //        {
        //            //This sequence has been valid
        //            var recursionResult = GenerateVariationsPart2(inputString[1..], "", remainingSequences.GetRange(1, remainingSequences.Count - 1));
        //            retStrings.AddRange(recursionResult.Select(x => '.' + x));
        //        }
        //    }
        //    else
        //    {
        //        if (firstChar == '.')
        //        {
        //            //var brokenSequenceLength = previousString.Split('.').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Length).OrderByDescending(x => x).ToList();
        //            if (brokenSequenceLength.Count == 0 || remainingSequences.Count == 0)
        //            {
        //                //Nothing to check for this layer
        //                var recursionResult = GenerateVariationsPart2(inputString[1..], "", remainingSequences);
        //                retStrings.AddRange(recursionResult.Select(x => '.' + x));
        //            }
        //            else if (brokenSequenceLength[0] == remainingSequences[0])
        //            {
        //                //This sequence has been valid
        //                var recursionResult = GenerateVariationsPart2(inputString[1..], "", remainingSequences.GetRange(1, remainingSequences.Count - 1));
        //                retStrings.AddRange(recursionResult.Select(x => '.' + x));
        //            }
        //            else
        //            {
        //                return retStrings;
        //                //This check is bad
        //            }
        //        }
        //        else
        //        {
        //            if (remainingSequences.Count > 0)
        //            {
        //                var possiblesBroken = GenerateVariationsPart2(inputString[1..], previousString + '#', remainingSequences);
        //                retStrings.AddRange(possiblesBroken.Select(x => '#' + x));
        //            }
        //        }
        //    }
        //    return retStrings;
        //}

        //public List<List<int>> GenerateBrokenSequences(string inputString, ref Dictionary<string, List<List<int>>> previousResults)
        //{
        //    if (previousResults.ContainsKey(inputString))
        //    {
        //        return previousResults[inputString];
        //    }
        //    else
        //    {
        //        var retSequences = GenerateVariations(inputString)

        //    }
        //}

        public List<string> GenerateVariations(string inputString)
        {
            var retStrings = new List<string>();

            if (inputString.Length == 0)
            {
                retStrings.Add("");
                return retStrings;
            }
            var firstChar = inputString[0];
            if (firstChar == '?')
            {
                var possibles = GenerateVariations(inputString[1..]);
                foreach (var possible in possibles)
                {
                    retStrings.Add('#' + possible.ToString());
                    retStrings.Add('.' + possible.ToString());
                }
            }
            else
            {
                var possibles = GenerateVariations(inputString[1..]);
                foreach (var possible in possibles)
                {
                    retStrings.Add(firstChar + possible.ToString());
                }
            }
            return retStrings;
        }
        public List<int> GetBrokenLengths(string inputString)
        {
            var brokenSequences = inputString.Split('.').Where(x => !String.IsNullOrEmpty(x)).Select(x => x.Length).ToList();
            return brokenSequences;
        }
        public int GetNumPermutations(string inputSpas, List<int> brokenLengths)
        {
            var variations = GenerateVariations(inputSpas);
            //brokenLengths = brokenLengths.OrderBy(x => x).ToList();
            var variationLengthsAll = variations.Select(x => GetBrokenLengths(x));
            var sameNumAsBrokenLenghts = variationLengthsAll.Where(x => x.Count == brokenLengths.Count).ToList();

            var filtered = sameNumAsBrokenLenghts.Where(x =>
            {
                for (int i = 0; i < x.Count; i++)
                {
                    if (brokenLengths[i] != x[i])
                    {
                        return false;
                    }
                }
                return true;
            }).ToList();
            return filtered.Count;

        }
    }

    public class Spring
    {
        public List<int> DamagedSprings { get; set; }
        public string InfoString { get; set; }
        public Spring(string input)
        {
            var springsAndInfo = input.Split(' ').ToList();
            InfoString = springsAndInfo[0];
            DamagedSprings = springsAndInfo[1].Split(',').Select(x => int.Parse(x)).ToList();
            InfoString += '.';
        }
    }
    public class SpringPart2
    {
        public List<int> DamagedSprings { get; set; }
        public string InfoString { get; set; }
        public SpringPart2(string input)
        {
            var springsAndInfo = input.Split(' ').ToList();

            var infoStringStart = springsAndInfo[0];
            var startDamagedSprings = springsAndInfo[1].Split(',').Select(x => int.Parse(x)).ToList();
            DamagedSprings = startDamagedSprings.ToList();
            InfoString = infoStringStart;
            for (int i = 0; i < 4; i++)
            {
                InfoString += '?' + infoStringStart;
                DamagedSprings.AddRange(startDamagedSprings);
            }
            InfoString += '.';
        }
    }

}
