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

            var resultThreads = new List<Task<long>>();
            var inputs = new List<long>();
            List<GardenMapStep> allMaps = new() { seedToSoilMaps, soilToFertilizerMaps, fetrilizerToWaterMaps, waterToLightMaps, lightToTemperatureMaps, temperatureToHumidityMaps, humidityToLocationMap };
            var currentList = seedNums.ToList();
            for (long i = 0; i < currentList[1]; i++)
            {
                inputs.Add(currentList[0] + i);
            }
            for (long i = 0; i < currentList[3]; i++)
            {
                inputs.Add(currentList[2] + i);
            }
            var lowestSeedNum = long.MaxValue;
            foreach (long i in inputs)
            {
                var task = FullTranslate(i, allMaps);
                task.Wait();
                var result = task.Result;
                if (result < lowestSeedNum)
                {
                    lowestSeedNum = result;
                }
            }

            //seedNums = (IEnumerable<long>)seedNums.Select(seed => seedToSoilMaps.TranslateNum(seed));
            //seedNums = (IEnumerable<long>)seedNums.Select(seed => soilToFertilizerMaps.TranslateNum(seed));
            //seedNums = (IEnumerable<long>)seedNums.Select(seed => fetrilizerToWaterMaps.TranslateNum(seed));
            //seedNums = (IEnumerable<long>)seedNums.Select(seed => waterToLightMaps.TranslateNum(seed));
            //seedNums = (IEnumerable<long>)seedNums.Select(seed => lightToTemperatureMaps.TranslateNum(seed));
            //seedNums = (IEnumerable<long>)seedNums.Select(seed => temperatureToHumidityMaps.TranslateNum(seed));
            //seedNums = (IEnumerable<long>)seedNums.Select(seed => humidityToLocationMap.TranslateNum(seed));
            //List<long> results;
            //try
            //{
            //    results = Task.WhenAll(resultThreads).GetAwaiter().GetResult().ToList();
            //}
            //catch (TaskCanceledException)
            //{
            //    throw new Exception("Tasks were cancelled");
            //}


            //foreach (var seedNum in results)
            //{
            //    if (seedNum < lowestSeedNum)
            //    {
            //        lowestSeedNum = seedNum;
            //    }
            //}
            return lowestSeedNum.ToString();
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
        //private void SortByLowest(List<GardenMapStep> mapSteps)
        //{
        //    mapSteps.Sort((a, b) =>
        //    {

        //    })
        //}
    }



    public class GardenMap
    {
        public long DestRangeStart { get; set; }
        public long SourceRangeStart { get; set; }
        public long RangeLength { get; set; }

        public GardenMap(long destRangeStart, long sourceRangeStart, long rangeLength)
        {
            DestRangeStart = destRangeStart;
            SourceRangeStart = sourceRangeStart;
            RangeLength = rangeLength;
        }
        public GardenMap(string line)
        {
            var nums = line.Split(' ');
            DestRangeStart = long.Parse(nums[0]);
            SourceRangeStart = long.Parse(nums[1]);
            RangeLength = long.Parse(nums[2]);
        }

        public long? GetNumberResult(long inputNum)
        {
            if (inputNum > SourceRangeStart + RangeLength - 1 || inputNum < SourceRangeStart)
            {
                return null;
            }

            return (long)(inputNum - SourceRangeStart + DestRangeStart);
        }
    }

    public class GardenMapStep
    {
        public List<GardenMap> Maps { get; set; }

        public Dictionary<long, long> PreviousValues { get; set; }
        public GardenMapStep(List<GardenMap> maps)
        {
            Maps = maps;
            PreviousValues = new Dictionary<long, long>();
        }

        public long TranslateNum(long inputNum)
        {
            if (PreviousValues.TryGetValue(inputNum, out var value))
            {
                return value;
            }
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
                PreviousValues.Add(inputNum, inputNum);
                return inputNum;
            }

            PreviousValues.Add(inputNum, (long)resultNum);
            return (long)resultNum;
        }
    }
}
