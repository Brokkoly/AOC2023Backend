namespace AOC2023Backend.Days
{
    public class DaySix : Day
    {
        public override string PartOne(string input)
        {
            var lines = ParseInput(input);
            var races = Race.MakeRacesFromInput(lines[0], lines[1]);
            var results = races.Select(x => GetNumWays(x));
            long result = 1;

            foreach (var item in results)
            {
                result *= item;
            }
            return result.ToString();
        }

        public long GetNumWays(Race race)
        {
            var numWays = 0;
            for (var i = 0; i < race.Time; i++)
            {
                if (i * (race.Time - i) > race.Record)
                {
                    numWays++;
                }
            }
            return numWays;
        }

        public override string PartTwo(string input)
        {
            var lines = ParseInput(input);
            var race = Race.MakeRaceFromInputP2(lines[0], lines[1]);

            var sqrt = Math.Sqrt(Math.Pow(race.Time, 2) - 4 * race.Record);
            var x1 = (race.Time + sqrt) / 2;
            var x2 = (race.Time - sqrt) / 2;
            return (x1-x2).ToString();
        }
    }

    public class Race
    {
        public static List<Race> MakeRacesFromInput(string times, string distances)
        {
            var timeList = times.Split(':')[1].Trim().Split(' ').Where(val => val.Length > 0).Select(val => long.Parse(val)).ToList();
            var distanceList = distances.Split(':')[1].Trim().Split(' ').Where(val => val.Length > 0).Select(val => long.Parse(val)).ToList();
            List<Race> races = new();
            for (int i = 0; i < timeList.Count; i++)
            {
                races.Add(new Race(timeList[i], distanceList[i]));
            }
            return races;
        }

        public static Race MakeRaceFromInputP2(string times, string distances)
        {
            var time = long.Parse(string.Join("", times.Split(':')[1].Where(val => val != ' ')));
            var distance = long.Parse(string.Join("", distances.Split(':')[1].Where(val => val != ' ')));
            return new Race(time, distance);
        }

        public long Time { get; set; }
        public long Record { get; set; }
        public Race(long time, long record)
        {
            this.Time = time; this.Record = record;
        }

    }
}
