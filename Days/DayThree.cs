using AOC2023Backend.DataTypes;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AOC2023Backend.Days
{
    public class DayThree : Day
    {
        public override string PartOne(string input)
        {
            var inputMatrix = new Matrix<char>(ParseInput2D(input));

            List<int> actualPartNums = new();

            for (var yIndex = 0; yIndex < inputMatrix.YCount; yIndex++)
            {
                //Each Line
                List<char> currentNum = new();
                bool isPartNum = false;
                MatrixNode<char>? currentNode = inputMatrix.GetNode(0, yIndex);
                if (currentNode == null)
                {
                    throw new Exception("Node shouldn't be null");
                }
                do
                {
                    var nextNode = currentNode.GetFromDirection(Direction.Right);
                    if (Char.IsDigit(currentNode.Value))
                    {
                        if (currentNum.Count == 0)
                        {
                            isPartNum = SymbolBehindNode(currentNode);
                        }
                        currentNum.Add(currentNode.Value);
                        isPartNum |= SymbolAboveOrBelowNode(currentNode);
                        if (nextNode?.Value == null || !Char.IsDigit(nextNode.Value))
                        {
                            isPartNum |= SymbolAheadOfNode(currentNode);
                            if (isPartNum)
                            {
                                actualPartNums.Add(int.Parse(string.Join("", currentNum)));
                            }
                            currentNum = new();
                            isPartNum = false;
                        }
                    }
                    currentNode = nextNode;


                } while (currentNode != null);

            }
            var numSum = 0;
            foreach (var num in actualPartNums)
            {
                numSum += num;
            }

            return numSum.ToString();
        }

        private bool SymbolBehindNode(MatrixNode<char> node)
        {
            var directions = new List<Direction> { Direction.UpLeft, Direction.Left, Direction.DownLeft };
            return CheckSymbolAnyDirection(node, directions);
        }

        private bool SymbolAboveOrBelowNode(MatrixNode<char> node)
        {
            var directions = new List<Direction> { Direction.Up, Direction.Down };
            return CheckSymbolAnyDirection(node, directions);
        }

        private bool SymbolAheadOfNode(MatrixNode<char> node)
        {
            var directions = new List<Direction> { Direction.UpRight, Direction.Right, Direction.DownRight };
            return CheckSymbolAnyDirection(node, directions);
        }

        private bool CheckSymbolAnyDirection(MatrixNode<char> node, List<Direction> directions)
        {
            foreach (var direction in directions)
            {
                var checkingNode = node.GetFromDirection(direction);
                if (checkingNode != null && checkingNode.Value != '.' && !Char.IsDigit(checkingNode.Value))
                {
                    return true;
                }
            }
            return false;
        }

        public override string PartTwo(string input)
        {
            List<List<char>> input2D = ParseInput2D(input);
            var input2DAsNodes = input2D.Select(line => line.Select(row =>
            new MatrixNumberNodeValue { Value = row }).ToList()).ToList();

            Matrix<MatrixNumberNodeValue> inputMatrix = new(input2DAsNodes);
            var allNumberStarts = new List<MatrixNode<MatrixNumberNodeValue>>();
            var potentialGears = new List<MatrixNode<MatrixNumberNodeValue>>();
            for (var yIndex = 0; yIndex < inputMatrix.YCount; yIndex++)
            {
                //Each Line
                MatrixNode<MatrixNumberNodeValue>? currentStart = null;
                MatrixNode<MatrixNumberNodeValue>? currentNode = inputMatrix.GetNode(0, yIndex);
                if (currentNode == null)
                {
                    throw new Exception("Node shouldn't be null");
                }
                do
                {
                    var nextNode = currentNode.GetFromDirection(Direction.Right);
                    if (Char.IsDigit(currentNode.Value.Value))
                    {
                        if (currentStart == null)
                        {
                            currentStart = currentNode;
                            allNumberStarts.Add(currentNode);
                        }
                        currentNode.Value.NumberParentNode = currentStart;
                    }
                    else
                    {
                        currentStart = null;
                    }
                    if (currentNode.Value.Value == '*')
                    {
                        potentialGears.Add(currentNode);
                    }
                    currentNode = nextNode;
                } while (currentNode != null);

            }

            var actualGearRatios = potentialGears.Select(gear =>
            {
                List<MatrixNode<MatrixNumberNodeValue>> potentialNumbers = new();
                foreach (var dir in Enum.GetValues(typeof(Direction)).Cast<Direction>())
                {
                    var node = gear.GetFromDirection(dir);
                    if (node != null && node.Value.NumberParentNode != null && !potentialNumbers.Contains(node.Value.NumberParentNode))
                    {
                        potentialNumbers.Add(node.Value.NumberParentNode);
                    }
                }
                if (potentialNumbers.Count != 2)
                {
                    return 0;
                }
                var firstNum = GetNumberFromMatrixNumberNode(potentialNumbers[0]);
                var secondNum = GetNumberFromMatrixNumberNode(potentialNumbers[1]);
                return firstNum * secondNum;
            }).ToList();



            var gearTotal = 0;

            foreach (var gear in actualGearRatios)
            {
                gearTotal += gear;
            }

            return gearTotal.ToString();
        }

        private int GetNumberFromMatrixNumberNode(MatrixNode<MatrixNumberNodeValue> node)
        {
            var firstNode = node.Value.NumberParentNode ?? throw new Exception("Got a node with no first number");
            var currentNode = firstNode;
            int currentNum = 0;
            while (currentNode != null && currentNode.Value.NumberParentNode != null)
            {
                currentNum = currentNum * 10 + int.Parse(currentNode.Value.Value.ToString());
                currentNode = currentNode.GetFromDirection(Direction.Right);
            }
            return currentNum;
        }

        private int CheckIfTooManyGearsAdjacent(MatrixNode<MatrixNumberNodeValue> node)
        {
            var numGearNodes = 0;
            var firstNode = node.Value.NumberParentNode ?? throw new Exception("Got a node with no first number");
            var currentNode = firstNode;
            var behindDirs = new List<Direction> { Direction.Left, Direction.UpLeft, Direction.DownLeft };
            var aheadDirs = new List<Direction> { Direction.UpRight, Direction.Right, Direction.DownRight };
            var aboveOrBelowDirs = new List<Direction> { Direction.Up, Direction.Down };
            foreach (var dir in behindDirs)
            {
                if (currentNode.GetFromDirection(dir).Value.Value == '*')
                {
                    numGearNodes++;
                }
            }
            while (true)
            {
                var nextNode = currentNode.GetFromDirection(Direction.Right);
                foreach (var dir in aboveOrBelowDirs)
                {
                    if (currentNode.GetFromDirection(dir).Value.Value == '*')
                    {
                        numGearNodes++;
                    }
                }
                if (nextNode == null || !Char.IsDigit(nextNode.Value.Value)) { break; }

                currentNode = nextNode;
            }
            foreach (var dir in aheadDirs)
            {
                if (currentNode.GetFromDirection(dir).Value.Value == '*')
                {
                    numGearNodes++;
                }
            }

            return numGearNodes;
        }


    }


    class MatrixNumberNodeValue
    {
        public char Value { get; set; }

        public MatrixNode<MatrixNumberNodeValue>? NumberParentNode { get; set; } = null;
        public List<MatrixNode<MatrixNumberNodeValue>> GearAdjacencies { get; set; } = new();
    }
}
