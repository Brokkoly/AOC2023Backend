using AOC2023Backend.DataTypes;

namespace AOC2023Backend.Days
{
    public class DaySixteen : Day
    {
        public override string PartOne(string input)
        {
            var list = ParseInput2D(input).Select(x => x.Select(y => new LaserValue(y)).ToList()).ToList();
            var laserMatrix = new LaserMatrix(list);
            var firstNode = laserMatrix.GetNode(0, 0);
            if (firstNode == null)
            {
                throw new Exception();
            }
            laserMatrix.PropagateLaser(firstNode, Direction.Right);

            for (int xIndex = 0; xIndex < list[0].Count; xIndex++)
            {

            }
            for (int yIndex = 0; yIndex < list.Count; yIndex++)
            {

            }

            return laserMatrix.CalculateEnergized().ToString();
        }

        public override string PartTwo(string input)
        {
            var list2D = ParseInput2D(input);
            var maxEnergized = 0;
            for (int xIndex = 0; xIndex < list2D[0].Count; xIndex++)
            {
                var list = list2D.Select(x => x.Select(y => new LaserValue(y)).ToList()).ToList();
                var laserMatrix = new LaserMatrix(list);
                laserMatrix.PropagateLaser(laserMatrix.GetNode(xIndex, 0) ?? throw new Exception(), Direction.Down);
                maxEnergized = Math.Max(laserMatrix.CalculateEnergized(), maxEnergized);
            }
            for (int xIndex = 0; xIndex < list2D[0].Count; xIndex++)
            {
                var list = list2D.Select(x => x.Select(y => new LaserValue(y)).ToList()).ToList();
                var laserMatrix = new LaserMatrix(list);
                laserMatrix.PropagateLaser(laserMatrix.GetNode(xIndex, list.Count - 1) ?? throw new Exception(), Direction.Up);
                maxEnergized = Math.Max(laserMatrix.CalculateEnergized(), maxEnergized);
            }
            for (int yIndex = 0; yIndex < list2D.Count; yIndex++)
            {
                var list = list2D.Select(x => x.Select(y => new LaserValue(y)).ToList()).ToList();
                var laserMatrix = new LaserMatrix(list);
                laserMatrix.PropagateLaser(laserMatrix.GetNode(list[0].Count - 1, yIndex) ?? throw new Exception(), Direction.Left);
                maxEnergized = Math.Max(laserMatrix.CalculateEnergized(), maxEnergized);
            }
            for (int yIndex = 0; yIndex < list2D.Count; yIndex++)
            {
                var list = list2D.Select(x => x.Select(y => new LaserValue(y)).ToList()).ToList();
                var laserMatrix = new LaserMatrix(list);
                laserMatrix.PropagateLaser(laserMatrix.GetNode(0, yIndex) ?? throw new Exception(), Direction.Right);
                maxEnergized = Math.Max(laserMatrix.CalculateEnergized(), maxEnergized);
            }

            return maxEnergized.ToString();
        }
    }

    public class LaserValue
    {
        public char Value { get; set; }
        public bool Energized { get; set; } = false;

        public HashSet<Direction> AlreadyHit { get; set; } = new();
        public LaserValue(char value) { Value = value; }

        public List<Direction> GetNextDirection(Direction inputDir)
        {
            if (AlreadyHit.Contains(inputDir))
            {
                return new();
            }
            else
            {
                AlreadyHit.Add(inputDir);
            }
            switch (Value)
            {
                case '|':
                    if (inputDir == Direction.Up || inputDir == Direction.Down)
                    {
                        return new List<Direction> { inputDir };
                    }
                    else
                    {
                        return new List<Direction> { Direction.Up, Direction.Down };
                    }
                case '-':
                    if (inputDir == Direction.Up || inputDir == Direction.Down)
                    {
                        return new List<Direction> { Direction.Left, Direction.Right };
                    }
                    else
                    {
                        return new List<Direction> { inputDir };
                    }
                case '\\':
                    switch (inputDir)
                    {
                        case Direction.Up:
                            return new List<Direction> { Direction.Left };
                        case Direction.Down:
                            return new List<Direction> { Direction.Right };
                        case Direction.Left:
                            return new List<Direction> { Direction.Up };
                        case Direction.Right:
                            return new List<Direction> { Direction.Down };
                        default:
                            throw new Exception();
                    }
                case '/':
                    switch (inputDir)
                    {
                        case Direction.Up:
                            return new List<Direction> { Direction.Right };

                        case Direction.Down:
                            return new List<Direction> { Direction.Left };
                        case Direction.Left:
                            return new List<Direction> { Direction.Down };
                        case Direction.Right:
                            return new List<Direction> { Direction.Up };
                        default:
                            throw new Exception();
                    }
                case '.':
                    return new List<Direction> { inputDir };
            }
            throw new Exception();
        }
    }
    public class LaserMatrix : Matrix<LaserValue>
    {
        public LaserMatrix(List<List<LaserValue>> input, bool reverseYAxis = false) : base(input, reverseYAxis)
        {
        }


        public int CalculateEnergized()
        {
            var energized = 0;
            foreach (var row in Values)
            {
                foreach (var node in row)
                {
                    if (node.Value.Energized)
                    {
                        energized++;
                    }
                }
            }
            return energized;
        }

        public void PropagateLaser(MatrixNode<LaserValue> firstNode, Direction firstDirection)
        {
            Queue<(MatrixNode<LaserValue>, Direction)> currentNodes = new();
            currentNodes.Enqueue((firstNode, firstDirection));
            while (currentNodes.Count > 0)
            {
                var nextInput = currentNodes.Dequeue();

                var nextNodeInputs = GetNextNodesAndDirections(nextInput);
                if (nextNodeInputs.Count > 0)
                {
                    foreach (var newNode in nextNodeInputs)
                    {
                        currentNodes.Enqueue(newNode);

                    }
                }
            }
        }
        public List<(MatrixNode<LaserValue>, Direction)> GetNextNodesAndDirections((MatrixNode<LaserValue>, Direction) input)
        {
            var node = input.Item1;
            node.Value.Energized = true;
            var retList = new List<(MatrixNode<LaserValue>, Direction)>();
            var newDirections = node.Value.GetNextDirection(input.Item2);
            foreach (var direction in newDirections)
            {
                var nextNode = node.GetFromDirection(direction);
                if (nextNode != null)
                {
                    retList.Add((nextNode, direction));
                }
            }
            return retList;
        }
    }

}
