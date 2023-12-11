namespace AOC2023Backend.Days
{
    public class DayEleven : Day
    {
        public override string PartOne(string input)
        {
            var rows = ParseInput2D(input);
            var galaxies = new List<Galaxy>();
            for (int yIndex = 0; yIndex < rows.Count; yIndex++)
            {
                var noGalaxies = !rows[yIndex].Contains('#');
                if (noGalaxies)
                {
                    var newRow = new List<char>();
                    for (int i = 0; i < rows[yIndex].Count; i++)
                    {
                        newRow.Add('.');
                    }
                    rows.Insert(yIndex, newRow);
                    yIndex++;
                }
            }
            for (int xIndex = 0; xIndex < rows[0].Count; xIndex++)
            {
                var noGalaxies = true;
                for (int yIndex = 0; yIndex < rows.Count; yIndex++)
                {
                    if (rows[yIndex][xIndex] == '#')
                    {
                        noGalaxies = false;
                    }
                }
                if (noGalaxies)
                {
                    foreach (var row in rows)
                    {
                        row.Insert(xIndex, '.');
                    }
                }
                xIndex++;
            }
            for (int yIndex = 0; yIndex < rows.Count; yIndex++)
            {
                for (int xIndex = 0; xIndex < rows[yIndex].Count; xIndex++)
                {
                    if (rows[yIndex][xIndex] == '#')
                    {
                        galaxies.Add(new Galaxy(xIndex, yIndex));
                    }

                }
            }
            var galaxyDistances = new Dictionary<(string, string), int>();
            for (int i = 0; i < galaxies.Count; i++)
            {
                for (int j = 1; j < galaxies.Count; j++)
                {
                    if (i == j)
                    {
                        continue;


                    }
                    var iKeyBigger = galaxies[i].Coords.CompareTo(galaxies[j].Coords) == 1;
                    var key = (iKeyBigger ? galaxies[i].Coords : galaxies[j].Coords, !iKeyBigger ? galaxies[i].Coords : galaxies[j].Coords);
                    if (galaxyDistances.ContainsKey(key))
                    {
                        continue;
                    }

                    var distance = Math.Abs(galaxies[i].YIndex - galaxies[j].YIndex) + Math.Abs(galaxies[j].XIndex - galaxies[i].XIndex);// + 1;
                    galaxyDistances.Add(key, distance);
                }
            }

            var distanceSum = 0;
            foreach (var distance in galaxyDistances.Values)
            {
                distanceSum += distance;
            }
            return distanceSum.ToString();
        }

        public override string PartTwo(string input)
        {
            var multiplier = 1000000;
            var rows = ParseInput2D(input);
            var galaxies = new List<Galaxy>();
            var yExtras = new List<int>();
            var xExtras = new List<int>();
            //Expand number of rows and add extra if needed
            for (int yIndex = 0; yIndex < rows.Count; yIndex++)
            {
                yExtras.Add(1);
                var noGalaxies = !rows[yIndex].Contains('#');
                if (noGalaxies)
                {
                    var newRow = new List<char>();
                    for (int i = 0; i < rows[yIndex].Count; i++)
                    {
                        newRow.Add('.');
                    }
                    rows.Insert(yIndex, newRow);
                    yIndex++;
                    yExtras.Add(multiplier - 1);
                }
            }
            //Expand columns and add extra as needed
            for (int xIndex = 0; xIndex < rows[0].Count; xIndex++)
            {
                xExtras.Add(1);
                var noGalaxies = true;
                for (int yIndex = 0; yIndex < rows.Count; yIndex++)
                {
                    if (rows[yIndex][xIndex] == '#')
                    {
                        noGalaxies = false;
                        break;
                    }
                }
                if (noGalaxies)
                {
                    foreach (var row in rows)
                    {
                        row.Insert(xIndex, '.');
                    }
                    xIndex++;
                    xExtras.Add(multiplier - 1);
                }
            }
            //Check For Galaxies with correct coordinates
            for (int yIndex = 0; yIndex < rows.Count; yIndex++)
            {
                for (int xIndex = 0; xIndex < rows[yIndex].Count; xIndex++)
                {
                    if (rows[yIndex][xIndex] == '#')
                    {
                        galaxies.Add(new Galaxy(xIndex, yIndex));
                    }

                }
            }
            //Use dictionary of tuples to avoid running twice on pairs
            var galaxyDistances = new Dictionary<(string, string), long>();
            for (int i = 0; i < galaxies.Count; i++)
            {
                for (int j = 1; j < galaxies.Count; j++)
                {
                    if (i == j)
                    {
                        continue;


                    }
                    var iKeyBigger = galaxies[i].Coords.CompareTo(galaxies[j].Coords) == 1;
                    var key = (iKeyBigger ? galaxies[i].Coords : galaxies[j].Coords, !iKeyBigger ? galaxies[i].Coords : galaxies[j].Coords);
                    if (galaxyDistances.ContainsKey(key))
                    {
                        continue;
                    }

                    var distance = CalculateDistance(galaxies[i], galaxies[j], xExtras, yExtras);
                    galaxyDistances.Add(key, distance);
                }
            }

            long distanceSum = 0;
            foreach (var distance in galaxyDistances.Values)
            {
                distanceSum += distance;
            }
            return distanceSum.ToString();
        }
        public long CalculateDistance(Galaxy galaxy1, Galaxy galaxy2, List<int> xCosts, List<int> yCosts)
        {

            long distance = 0;
            for (int i = galaxy1.XIndex; galaxy2.XIndex >= galaxy1.XIndex ? i < galaxy2.XIndex : i > galaxy2.XIndex; i += galaxy2.XIndex >= galaxy1.XIndex ? 1 : -1)
            {
                distance += xCosts[i];
            }
            for (int i = galaxy1.YIndex; galaxy2.YIndex >= galaxy1.YIndex ? i < galaxy2.YIndex : i > galaxy2.YIndex; i += galaxy2.YIndex >= galaxy1.YIndex ? 1 : -1)
            {
                distance += yCosts[i];
            }

            return distance;
        }
    }

    public class Galaxy
    {
        public int XIndex { get; set; }
        public int YIndex { get; set; }
        public string Coords { get => string.Join(',', new List<int> { XIndex, YIndex }); }
        public Galaxy(int x, int y)
        {
            XIndex = x;
            YIndex = y;
        }
    }
}
