using AOC2023Backend.DataTypes;

namespace AOC2023Backend.Days
{
    public class DayFourteen : Day
    {
        public override string PartOne(string input)
        {
            var input2d = ParseInput2D(input);
            var boulderMatrix = new BoulderMatrix(input2d);

            boulderMatrix.RollNorth();

            boulderMatrix.PrintMatrix();
            return CalculateLoad(boulderMatrix).ToString();
        }

        public override string PartTwo(string input)
        {
            var input2d = ParseInput2D(input);
            var boulderMatrix = new BoulderMatrix(input2d);
            var cycle = new List<Direction> { Direction.Up, Direction.Left, Direction.Down, Direction.Right };
            var loads = new List<int>();
            for (int cycleNum = 1; cycleNum <= 1000; cycleNum++)
            {
                for (int i = 0; i < cycle.Count; i++)
                {
                    boulderMatrix.RollInAnyDirection(cycle[i]);
                }
                loads.Add(CalculateLoad(boulderMatrix));

                //Console.Write(String.Format("Cycle {0}:", cycleNum));
                //boulderMatrix.PrintMatrix();
            }
            var cycleLength = FindCycle(loads);


            if (cycleLength == -1)
            {
                throw new Exception();
            }
            else
            {
                var cycleIndex = cycleLength - (1000000000 - 1000) % cycleLength;
                return loads[loads.Count - cycleIndex - 1].ToString();
            }

            return CalculateLoad(boulderMatrix).ToString();

        }

        public int FindCycle(List<int> loads)
        {
            var current = loads[loads.Count - 1];

            for (int i = loads.Count - 2; i >= 500; i--)
            {
                if (loads[i] == current)
                {
                    var isCycle = true;
                    var subsetToCheck = loads.GetRange(i + 1, loads.Count - i - 1);
                    for (int j = subsetToCheck.Count - 1; j >= 0; j--)
                    {
                        if (subsetToCheck[j] != loads[i - (subsetToCheck.Count - 1 - j)])
                        {
                            isCycle = false;
                            break;
                        }
                    }
                    if (isCycle)
                    {
                        for (int j = subsetToCheck.Count - 1; j >= 0; j--)
                        {
                            if (subsetToCheck[j] != loads[i - subsetToCheck.Count - (subsetToCheck.Count - 1 - j)])
                            {
                                isCycle = false;
                                break;
                            }
                        }
                    }
                    if (isCycle)
                    {
                        return subsetToCheck.Count;
                    }
                }
            }
            return -1;
        }

        public int CalculateLoad(BoulderMatrix boulderMatrix)
        {
            var retSum = 0;
            for (var yIndex = 0; yIndex < boulderMatrix.Values.Count; yIndex++)
            {
                for (int xIndex = 0; xIndex < boulderMatrix.Values[yIndex].Count; xIndex++)
                {
                    if (boulderMatrix.GetNode(xIndex, yIndex)?.Value == 'O')
                    {
                        retSum += boulderMatrix.Values.Count - yIndex;
                    }
                }
            }
            return retSum;
        }
    }


    public class BoulderMatrix : Matrix<char>
    {
        public BoulderMatrix(List<List<char>> input, bool reverseYAxis = false) : base(input, reverseYAxis)
        {
        }

        public void RollNorth()
        {
            var direction = Direction.Up;
            //TODO: make this generic 
            for (int yIndex = 1; yIndex < Values.Count; yIndex++)
            {
                for (int xIndex = 0; xIndex < Values[yIndex].Count; xIndex++)
                {
                    var node = this.GetNode(xIndex, yIndex);
                    if (node?.Value != 'O')
                    {
                        continue;
                    }
                    var previousNode = node;
                    var actuallyMoved = false;
                    var nextNode = node?.GetFromDirection(direction);
                    if (node == null || nextNode == null)
                    {
                        throw new Exception();
                    }
                    while (nextNode?.Value == '.')
                    {
                        actuallyMoved = true;
                        previousNode = nextNode;
                        nextNode = nextNode.GetFromDirection(direction);
                    }
                    if (previousNode == null)
                    {
                        throw new Exception();
                    }
                    if (actuallyMoved)
                    {
                        previousNode.Value = 'O';
                        node.Value = '.';
                    }
                }
            }
        }

        public void RollInAnyDirection(Direction direction)
        {
            if (!(new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right }).Contains(direction))
            {
                throw new Exception("Invalid Direction");
            }
            //TODO: make this generic 
            switch (direction)
            {
                case Direction.Up:
                    for (int yIndex = 1; yIndex < Values.Count; yIndex++)
                    {
                        for (int xIndex = 0; xIndex < Values[yIndex].Count; xIndex++)
                        {
                            var node = this.GetNode(xIndex, yIndex);
                            if (node != null)
                            {
                                RollInDirection(node, direction);
                            }
                        }
                    }
                    break;
                case Direction.Down:
                    for (int yIndex = Values.Count - 2; yIndex >= 0; yIndex--)
                    {
                        for (int xIndex = 0; xIndex < Values[yIndex].Count; xIndex++)
                        {
                            var node = this.GetNode(xIndex, yIndex);
                            if (node != null)
                            {
                                RollInDirection(node, direction);
                            }
                        }
                    }
                    break;
                case Direction.Left:
                    for (int xIndex = 1; xIndex < Values[0].Count; xIndex++)
                    {
                        for (int yIndex = 0; yIndex < Values.Count; yIndex++)
                        {
                            var node = this.GetNode(xIndex, yIndex);
                            if (node != null)
                            {
                                RollInDirection(node, direction);
                            }
                        }
                    }
                    break;
                case Direction.Right:
                    for (int xIndex = Values[0].Count - 2; xIndex >= 0; xIndex--)
                    {
                        for (int yIndex = 0; yIndex < Values.Count; yIndex++)
                        {
                            var node = this.GetNode(xIndex, yIndex);
                            if (node != null)
                            {
                                RollInDirection(node, direction);
                            }
                        }
                    }
                    break;
                default:
                    throw new Exception();
            }
        }
        private void RollInDirection(MatrixNode<char> node, Direction direction)
        {
            if (node?.Value != 'O')
            {
                return;
            }
            var previousNode = node;
            var actuallyMoved = false;
            var nextNode = node?.GetFromDirection(direction);
            if (node == null || nextNode == null)
            {
                throw new Exception();
            }
            while (nextNode?.Value == '.')
            {
                actuallyMoved = true;
                previousNode = nextNode;
                nextNode = nextNode.GetFromDirection(direction);
            }
            if (previousNode == null)
            {
                throw new Exception();
            }
            if (actuallyMoved)
            {
                previousNode.Value = 'O';
                node.Value = '.';
            }
        }
    }
}
