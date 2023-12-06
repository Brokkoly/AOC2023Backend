using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AOC2023Backend.Days
{
    public class DayFive : Day
    {
        public override string PartOne(string input)
        {
            var lines = ParseInput(input);
            IEnumerable<long> seedNums = lines[0].Split(": ")[1].Split(' ').Select(val => long.Parse(val));
            var currentIndex = 3;

            //seed-to-soil
            var seedToSoilMaps = new GardenMapStep(new List<GardenMap>());
            while (lines[currentIndex].Length > 0)
            {
                seedToSoilMaps.Maps.Add(new GardenMap(lines[currentIndex].Trim()));
                currentIndex++;
            }
            currentIndex += 2;


            //soil-to-fertilizer
            var soilToFertilizerMaps = new GardenMapStep(new List<GardenMap>());
            while (lines[currentIndex].Length > 0)
            {
                soilToFertilizerMaps.Maps.Add(new GardenMap(lines[currentIndex].Trim()));
                currentIndex++;
            }
            currentIndex += 2;

            //fertilizer-to-water
            var fetrilizerToWaterMaps = new GardenMapStep(new List<GardenMap>());
            while (lines[currentIndex].Length > 0)
            {
                fetrilizerToWaterMaps.Maps.Add(new GardenMap(lines[currentIndex].Trim()));
                currentIndex++;
            }
            currentIndex += 2;

            //water-to-light
            var waterToLightMaps = new GardenMapStep(new List<GardenMap>());
            while (lines[currentIndex].Length > 0)
            {
                waterToLightMaps.Maps.Add(new GardenMap(lines[currentIndex].Trim()));
                currentIndex++;
            }
            currentIndex += 2;

            //light-to-temperature
            var lightToTemperatureMaps = new GardenMapStep(new List<GardenMap>());
            while (lines[currentIndex].Length > 0)
            {
                lightToTemperatureMaps.Maps.Add(new GardenMap(lines[currentIndex].Trim()));
                currentIndex++;
            }
            currentIndex += 2;

            //temperature-to-humidity
            var temperatureToHumidityMaps = new GardenMapStep(new List<GardenMap>());
            while (lines[currentIndex].Length > 0)
            {
                temperatureToHumidityMaps.Maps.Add(new GardenMap(lines[currentIndex].Trim()));
                currentIndex++;
            }
            currentIndex += 2;

            //humidity-to-location
            var humidityToLocationMap = new GardenMapStep(new List<GardenMap>());
            while (currentIndex < lines.Count && lines[currentIndex].Length > 0)
            {
                humidityToLocationMap.Maps.Add(new GardenMap(lines[currentIndex].Trim()));
                currentIndex++;
            }


            seedNums = (IEnumerable<long>)seedNums.Select(seed => seedToSoilMaps.TranslateNum(seed));
            seedNums = (IEnumerable<long>)seedNums.Select(seed => soilToFertilizerMaps.TranslateNum(seed));
            seedNums = (IEnumerable<long>)seedNums.Select(seed => fetrilizerToWaterMaps.TranslateNum(seed));
            seedNums = (IEnumerable<long>)seedNums.Select(seed => waterToLightMaps.TranslateNum(seed));
            seedNums = (IEnumerable<long>)seedNums.Select(seed => lightToTemperatureMaps.TranslateNum(seed));
            seedNums = (IEnumerable<long>)seedNums.Select(seed => temperatureToHumidityMaps.TranslateNum(seed));
            seedNums = (IEnumerable<long>)seedNums.Select(seed => humidityToLocationMap.TranslateNum(seed));





            var lowestSeedNum = long.MaxValue;
            foreach (var seedNum in seedNums)
            {
                if (seedNum < lowestSeedNum)
                {
                    lowestSeedNum = seedNum;
                }
            }
            return lowestSeedNum.ToString();
        }

        public override string PartTwo(string input)
        {
            var lines = ParseInput(input);
            IEnumerable<long> seedNums = lines[0].Split(": ")[1].Split(' ').Select(val => long.Parse(val));
            var currentIndex = 3;
            var seedNumList = seedNums.ToList();
            var seedRanges = new List<GardenMap>();
            for (var i = 0; i < seedNumList.Count; i += 2)
            {
                seedRanges.Add(new GardenMap(seedNumList[i], seedNumList[i], seedNumList[i + 1]));
            }

            //seed-to-soil
            var seedToSoilMaps = new GardenMapStep(new List<GardenMap>());
            while (lines[currentIndex].Length > 0)
            {
                seedToSoilMaps.Maps.Add(new GardenMap(lines[currentIndex].Trim()));
                currentIndex++;
            }
            currentIndex += 2;


            //soil-to-fertilizer
            var soilToFertilizerMaps = new GardenMapStep(new List<GardenMap>());
            while (lines[currentIndex].Length > 0)
            {
                soilToFertilizerMaps.Maps.Add(new GardenMap(lines[currentIndex].Trim()));
                currentIndex++;
            }
            currentIndex += 2;

            //fertilizer-to-water
            var fetrilizerToWaterMaps = new GardenMapStep(new List<GardenMap>());
            while (lines[currentIndex].Length > 0)
            {
                fetrilizerToWaterMaps.Maps.Add(new GardenMap(lines[currentIndex].Trim()));
                currentIndex++;
            }
            currentIndex += 2;

            //water-to-light
            var waterToLightMaps = new GardenMapStep(new List<GardenMap>());
            while (lines[currentIndex].Length > 0)
            {
                waterToLightMaps.Maps.Add(new GardenMap(lines[currentIndex].Trim()));
                currentIndex++;
            }
            currentIndex += 2;

            //light-to-temperature
            var lightToTemperatureMaps = new GardenMapStep(new List<GardenMap>());
            while (lines[currentIndex].Length > 0)
            {
                lightToTemperatureMaps.Maps.Add(new GardenMap(lines[currentIndex].Trim()));
                currentIndex++;
            }
            currentIndex += 2;

            //temperature-to-humidity
            var temperatureToHumidityMaps = new GardenMapStep(new List<GardenMap>());
            while (lines[currentIndex].Length > 0)
            {
                temperatureToHumidityMaps.Maps.Add(new GardenMap(lines[currentIndex].Trim()));
                currentIndex++;
            }
            currentIndex += 2;

            //humidity-to-location
            var humidityToLocationMap = new GardenMapStep(new List<GardenMap>());
            while (currentIndex < lines.Count && lines[currentIndex].Length > 0)
            {
                humidityToLocationMap.Maps.Add(new GardenMap(lines[currentIndex].Trim()));
                currentIndex++;
            }
            long currentOutput = 0;
            var seedToSoilFull = new GardenMapStep(GardenMap.GetIntersectionResult(seedRanges, seedToSoilMaps.Maps));
            var currentOutput1 = seedToSoilFull.TranslateNum(82);
            var seedToFertilizer = new GardenMapStep(GardenMap.GetIntersectionResult(seedToSoilFull.Maps, soilToFertilizerMaps.Maps));
            var currentOutput2 = seedToFertilizer.TranslateNum(82);
            var seedToWater = new GardenMapStep(GardenMap.GetIntersectionResult(seedToFertilizer.Maps, fetrilizerToWaterMaps.Maps));
            var currentOutput3 = seedToWater.TranslateNum(82);
            var seedToLight = new GardenMapStep(GardenMap.GetIntersectionResult(seedToWater.Maps, waterToLightMaps.Maps));
            var currentOutput4 = seedToLight.TranslateNum(82);
            var seedToTemperature = new GardenMapStep(GardenMap.GetIntersectionResult(seedToLight.Maps, lightToTemperatureMaps.Maps));
            var currentOutput5 = seedToTemperature.TranslateNum(82);
            var seedToHumidity = new GardenMapStep(GardenMap.GetIntersectionResult(seedToTemperature.Maps, temperatureToHumidityMaps.Maps));
            var currentOutput6 = seedToHumidity.TranslateNum(82);
            var seedToLocation = new GardenMapStep(GardenMap.GetIntersectionResult(seedToHumidity.Maps, humidityToLocationMap.Maps));
            var currentOutput7 = seedToLocation.TranslateNum(82);

            var sorted = seedToLocation.Maps.OrderBy(map => map.DestRangeStart).ToList();

            return sorted[0].DestRangeStart.ToString();
        }
        private async Task<long> FullTranslate(long seedNum, List<GardenMapStep> mapSteps)
        {
            return await Task.Run(() =>
            {
                var currentSeedNum = seedNum;
                foreach (var step in mapSteps)
                {
                    currentSeedNum = step.TranslateNum(currentSeedNum);
                }
                return currentSeedNum;
            });
        }
    }



    public class GardenMap
    {
        public long DestRangeStart { get; set; }
        public long SourceRangeStart { get; set; }
        public long RangeLength { get; set; }

        public long SourceRangeEnd { get; set; }
        public long DestRangeEnd { get; set; }
        public long SourceToDestOffset { get; set; }

        public GardenMap(long destRangeStart, long sourceRangeStart, long rangeLength)
        {
            DestRangeStart = destRangeStart;
            SourceRangeStart = sourceRangeStart;
            RangeLength = rangeLength;
            SourceRangeEnd = sourceRangeStart + rangeLength - 1;
            DestRangeEnd = destRangeStart + rangeLength - 1;
            SourceToDestOffset = DestRangeStart - SourceRangeStart;
        }
        public GardenMap(string line)
        {

            var nums = line.Split(' ');
            DestRangeStart = long.Parse(nums[0]);
            SourceRangeStart = long.Parse(nums[1]);
            RangeLength = long.Parse(nums[2]);
            SourceRangeEnd = SourceRangeStart + RangeLength - 1;
            DestRangeEnd = DestRangeStart + RangeLength - 1;
            SourceToDestOffset = DestRangeStart - SourceRangeStart;
        }

        public long? GetNumberResult(long inputNum)
        {
            if (inputNum > SourceRangeStart + RangeLength - 1 || inputNum < SourceRangeStart)
            {
                return null;
            }

            return (long)(inputNum - SourceRangeStart + DestRangeStart);
        }

        public static List<GardenMap> GetIntersectionResult(List<GardenMap> inputRanges, List<GardenMap> nextRanges)
        {
            var input = SortRangeListDest(inputRanges);
            var next = SortRangeListSource(nextRanges);

            //A = input begin, B = input end, C = next begin, D = next end
            for (int inputIndex = 0, nextIndex = 0; inputIndex < input.Count && nextIndex < next.Count;)
            {
                var nextRange = next[nextIndex];
                var inputRange = input[inputIndex];
                if (nextRange.SourceRangeEnd < inputRange.DestRangeStart)
                {
                    //No overlap
                    nextIndex++;
                }
                else if (nextRange.SourceRangeStart > inputRange.DestRangeEnd)
                {
                    //No overlap
                    inputIndex++;
                }
                else if (nextRange.SourceRangeStart <= inputRange.DestRangeStart && nextRange.SourceRangeEnd >= inputRange.DestRangeEnd)
                {
                    //input engulfed by next
                    input[inputIndex] = new GardenMap(destRangeStart: inputRange.DestRangeStart + nextRange.SourceToDestOffset, inputRange.SourceRangeStart, inputRange.RangeLength);
                    inputIndex++;
                }
                else
                {
                    if (nextRange.SourceRangeStart <= inputRange.DestRangeStart)
                    {
                        //next overlaps to the left
                        //inputIndex++ (point to the remaining input on the right
                        //nextIndex++
                        var leftRange = nextRange.SourceRangeEnd - inputRange.DestRangeStart+1;
                        var rightRange = inputRange.RangeLength - leftRange;
                        var leftDestStart = inputRange.DestRangeStart;// + nextRange.SourceToDestOffset;
                        var rightDestStart = inputRange.DestRangeStart + leftRange;
                        var leftSourceStart = inputRange.SourceRangeStart;
                        var rightSourceStart = inputRange.SourceRangeStart + leftRange;
                        var LeftInput = new GardenMap(leftDestStart, leftSourceStart, leftRange);

                        var RightInputRemaining = new GardenMap(rightDestStart, rightSourceStart, rightRange);
                        input[inputIndex] = LeftInput;
                        input.Insert(inputIndex + 1, RightInputRemaining);
                        inputIndex++;
                        nextIndex++;
                    }
                    else if (nextRange.SourceRangeEnd >= inputRange.DestRangeEnd)
                    {
                        //(point to the remaining input on the right
                        //nothing changes for source on the left
                        //next overlaps to the right
                        var leftRange = nextRange.SourceRangeStart - inputRange.DestRangeStart;
                        var rightRange = inputRange.RangeLength - leftRange;
                        var leftDestStart = inputRange.DestRangeStart;
                        var rightDestStart = inputRange.DestRangeStart + nextRange.SourceToDestOffset;
                        var leftSourceStart = inputRange.SourceRangeStart;
                        var rightSourceStart = inputRange.SourceRangeStart + leftRange;
                        var LeftInput = new GardenMap(leftDestStart, leftSourceStart, leftRange);
                        var RightInputRemaining = new GardenMap(rightDestStart, rightSourceStart, rightRange);
                        input[inputIndex] = LeftInput;
                        input.Insert(inputIndex + 1, RightInputRemaining);
                        inputIndex += 2;
                    }
                    else
                    {
                        //next in between
                        //inputIndex+=2
                        //nextIndex++
                        //nothing changes for input on the left
                        //input in the middle is affected
                        //handle in next loop

                        var leftRange = nextRange.SourceRangeStart - inputRange.DestRangeStart;
                        var rightRange = inputRange.RangeLength - leftRange - nextRange.RangeLength;
                        var leftDestStart = inputRange.DestRangeStart;
                        var rightDestStart = inputRange.DestRangeStart + leftRange + nextRange.RangeLength;
                        var leftSourceStart = inputRange.SourceRangeStart;
                        var rightSourceStart = inputRange.SourceRangeStart + leftRange + nextRange.RangeLength;
                        var LeftInput = new GardenMap(leftDestStart, leftSourceStart, leftRange);
                        var MiddleInput = new GardenMap(nextRange.DestRangeStart, nextRange.SourceRangeStart, nextRange.RangeLength);
                        var RightInputRemaining = new GardenMap(rightDestStart, rightSourceStart, rightRange);
                        input[inputIndex] = LeftInput;
                        input.Insert(inputIndex + 1, MiddleInput);
                        input.Insert(inputIndex + 2, RightInputRemaining);
                        inputIndex += 2;
                    }
                }
            }
            return input;


        }
        public static List<GardenMap> SortRangeListSource(List<GardenMap> toSort)
        {
            return toSort.OrderBy(i => i.SourceRangeStart).ToList();
        }
        public static List<GardenMap> SortRangeListDest(List<GardenMap> toSort)
        {
            return toSort.OrderBy(i => i.DestRangeStart).ToList();
        }
    }

    public class GardenMapStep
    {
        public List<GardenMap> Maps { get; set; }

        public GardenMapStep(List<GardenMap> maps)
        {
            Maps = maps;
        }

        public long TranslateNum(long inputNum)
        {
            long? resultNum = null;
            foreach (var map in Maps)
            {
                resultNum = map.GetNumberResult(inputNum);
                if (resultNum != null)
                {
                    break;
                }
            }
            if (resultNum == null)
            {
                return inputNum;
            }
            return (long)resultNum;
        }
    }

    public class GardenMapRange : GardenMap
    {
        public long Start { get; set; }
        public long End { get; set; }
        public long ResultOffset { get; set; }
        GardenMapRange(long destRangeStart, long sourceRangeStart, long rangeLength) : base(destRangeStart, sourceRangeStart, rangeLength)
        {
            Start = sourceRangeStart;
            End = rangeLength;
            ResultOffset = destRangeStart - sourceRangeStart;
        }


        public static List<GardenMapRange> SortRangeList(List<GardenMapRange> toSort)
        {
            return toSort.OrderBy(i => i.Start).ToList();
        }
    }
}