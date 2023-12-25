using Microsoft.Extensions.Configuration.CommandLine;
using System.Security.Cryptography.X509Certificates;

namespace AOC2023Backend.Days
{
    public class DayTwentyThree : Day
    {
        public override string PartOne(string input)
        {
            var lines = ParseInput(input);

            var hailstones = lines.Select(x =>
            {
                var posAndVel = x.Split(" @ ").Select(y => y.Split(", ").Select(z => int.Parse(z)).ToList()).ToList();
                return new Hailstone(posAndVel[0][0], posAndVel[0][1], posAndVel[0][2], posAndVel[1][0], posAndVel[1][1], posAndVel[1][2]);
            }).ToList();

            List<(Hailstone, Hailstone)> distinctPairs = new();

            foreach (var hailstone in hailstones)
            {
                foreach (var hailstone2 in hailstones)
                {
                    if (!distinctPairs.Any(x => (x.Item1 == hailstone && x.Item2 == hailstone2) || (x.Item1 == hailstone2 && x.Item2 == hailstone)))
                    {
                        distinctPairs.Add((hailstone, hailstone2));
                    }
                }
            }

            var numInside = 0;

            foreach (var pair in distinctPairs)
            {
                var intersectionTimes = pair.Item1.FindIntersectionTimes(pair.Item2);


            }

            return numInside.ToString();
        }

        public override string PartTwo(string input)
        {
            throw new NotImplementedException();
        }


    }

    public class Hailstone
    {
        public float XIndex, YIndex, ZIndex, DX, DY, DZ;

        public Hailstone(float x, float y, float z, float dx, float dy, float dz)
        {
            XIndex = x;
            YIndex = y;
            ZIndex = z;
            DX = dx;
            DY = dy;
            DZ = dz;
        }

        public (float, float) FindIntersectionTimes(Hailstone otherHailstone)
        {
            var intersectionTimeX = FindSameTime(XIndex, otherHailstone.XIndex, DX, otherHailstone.DX);

            var intersectionTimeY = FindSameTime(YIndex, otherHailstone.YIndex, DY, otherHailstone.DY);

            return (intersectionTimeX, intersectionTimeY);
        }

        public (float, float) FindPositionAtTime(float time)
        {
            return (XIndex + DX * time, YIndex + DY * time);
        }

        private float FindSameTime(float p0, float p1, float d0, float d1)
        {
            var result = p0 - p1;
            result /= d1 - d0;

            return result;
        }
    }
}
