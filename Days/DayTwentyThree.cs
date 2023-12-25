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

        public (float, float) FindIntersection(Hailstone otherHailstone)
        {

        }

        private float FindSameTime(float p0, float p1, float d0, float d1)
        {
            var result = p0 - p1;
            result /= d1 - d0;
        }
    }
}
