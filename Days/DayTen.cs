using AOC2023Backend.DataTypes;
using System.Security.Cryptography.X509Certificates;

namespace AOC2023Backend.Days
{
    public class DayTen : Day
    {
        public override string PartOne(string input)
        {
            var pipeMatrix = new Matrix<char>(ParseInput2D(input));

            MatrixNode<char>? startNode = null;
            foreach (var row in pipeMatrix.Values)
            {
                var node = row.Find(node => node.Value == 'S');
                if (node != null)
                {
                    startNode = node;
                    break;
                }
            }
            if (startNode == null)
            {
                throw new Exception("Didn't Find Start");
            }

            var adjacentPipes = new List<MatrixNode<char>>();

            var left = startNode.GetFromDirection(Direction.Left);
            var right = startNode.GetFromDirection(Direction.Right);
            var up = startNode.GetFromDirection(Direction.Up);
            var down = startNode.GetFromDirection(Direction.Down);

            if (left != null && PipeNode.ShapeToDir(left.Value).Contains(Direction.Right))
            {
                adjacentPipes.Add(left);
            }
            if (right != null && PipeNode.ShapeToDir(right.Value).Contains(Direction.Left))
            {
                adjacentPipes.Add(right);
            }
            if (up != null && PipeNode.ShapeToDir(up.Value).Contains(Direction.Down))
            {
                adjacentPipes.Add(up);
            }
            if (down != null && PipeNode.ShapeToDir(down.Value).Contains(Direction.Up))
            {
                adjacentPipes.Add(down);
            }
            var distanceFromStart = 1;
            var currentNode = adjacentPipes[0];
            var previousNode = startNode;
            while (currentNode.Value != 'S')
            {
                var nextNode = GetNextPipeNode(previousNode, currentNode);
                previousNode = currentNode;
                currentNode = nextNode;
                distanceFromStart++;
            }

            return (distanceFromStart / 2).ToString();
        }

        public override string PartTwo(string input)
        {
            var pipeInput = ParseInput2D(input).Select(x => x.Select(inputChar => new PipeNode(inputChar)).ToList()).ToList();
            var pipeMatrix = new Matrix<PipeNode>(pipeInput);

            MatrixNode<PipeNode>? startNode = null;
            foreach (var row in pipeMatrix.Values)
            {
                var node = row.Find(node => node.Value.IsStart);
                if (node != null)
                {
                    startNode = node;
                    break;
                }
            }
            if (startNode == null)
            {
                throw new Exception("Didn't Find Start");
            }

            var adjacentPipes = new List<MatrixNode<PipeNode>>();

            var left = startNode.GetFromDirection(Direction.Left);
            var right = startNode.GetFromDirection(Direction.Right);
            var up = startNode.GetFromDirection(Direction.Up);
            var down = startNode.GetFromDirection(Direction.Down);

            var possiblePipeShapes = new List<char> { 'J', 'L', 'F', '|', '-', '7' };

            if (left != null && PipeNode.ShapeToDir(left.Value.PipeShape).Contains(Direction.Right))
            {
                adjacentPipes.Add(left);
                possiblePipeShapes.Remove('L');
                possiblePipeShapes.Remove('F');
                possiblePipeShapes.Remove('|');
            }
            if (right != null && PipeNode.ShapeToDir(right.Value.PipeShape).Contains(Direction.Left))
            {
                adjacentPipes.Add(right);
                possiblePipeShapes.Remove('J');
                possiblePipeShapes.Remove('7');
                possiblePipeShapes.Remove('|');
            }
            if (up != null && PipeNode.ShapeToDir(up.Value.PipeShape).Contains(Direction.Down))
            {
                adjacentPipes.Add(up);
                possiblePipeShapes.Remove('-');
                possiblePipeShapes.Remove('7');
                possiblePipeShapes.Remove('F');
            }
            if (down != null && PipeNode.ShapeToDir(down.Value.PipeShape).Contains(Direction.Up))
            {
                adjacentPipes.Add(down);
                possiblePipeShapes.Remove('L');
                possiblePipeShapes.Remove('J');
                possiblePipeShapes.Remove('-');
            }
            if (possiblePipeShapes.Count == 1)
            {
                startNode.Value.PipeShape = possiblePipeShapes[0];
            }
            var distanceFromStart = 1;
            var currentNode = adjacentPipes[0];
            currentNode.Value.InLoop = true;
            var previousNode = startNode;
            while (!currentNode.Value.IsStart)
            {
                var nextNode = GetNextPipeNode(previousNode, currentNode);
                nextNode.Value.InLoop = true;
                previousNode = currentNode;
                currentNode = nextNode;
                distanceFromStart++;
            }
            var upChars = new List<char> { 'J', 'L', '|' };
            var numInside = 0;
            foreach (var row in pipeMatrix.Values)
            {
                var inside = false;
                for (int i = 0; i < row.Count; i++)
                {
                    if (row[i].Value.InLoop)
                    {
                        if (upChars.Contains(row[i].Value.PipeShape))
                        {
                            inside = !inside;
                        }
                    }
                    else
                    {
                        if (inside)
                        {
                            row[i].Value.PipeShape = 'I';
                            numInside++;
                        }
                        else
                        {
                            row[i].Value.PipeShape = 'O';
                        }
                    }

                }
            }
            var rowStrings = pipeMatrix.Values.Select(row =>
            {
                var newRow = String.Join("", row.Select(x => x.Value.PipeShape).ToList());
                return newRow;
            });
            Console.WriteLine(String.Join('\n', rowStrings));


            return numInside.ToString();


        }

        public MatrixNode<char> GetNextPipeNode(MatrixNode<char> previousNode, MatrixNode<char> CurrentNode)
        {
            var dirs = PipeNode.ShapeToDir(CurrentNode.Value);
            var nextNode = dirs.Select(dir => CurrentNode.GetFromDirection(dir)).Where(node => node != null && !node.SameNode(previousNode)).ToList();
            return nextNode[0] ?? throw new Exception("Couldn't find next pipe");
        }

        public MatrixNode<PipeNode> GetNextPipeNode(MatrixNode<PipeNode> previousNode, MatrixNode<PipeNode> CurrentNode)
        {
            var dirs = PipeNode.ShapeToDir(CurrentNode.Value.PipeShape);
            var nextNode = dirs.Select(dir => CurrentNode.GetFromDirection(dir)).Where(node => node != null && !node.SameNode(previousNode)).ToList();
            return nextNode[0] ?? throw new Exception("Couldn't find next pipe");
        }

    }

    public interface IPipeNode
    {
        public bool IsStart { get; set; }
        //public int X { get; set; }
        //public int Y { get; set; }
        public List<IPipeNode> ConnectedNodes { get; set; }
        public List<IPipeNode> GetConnectedNodes();
    }

    public class PipeNode
    {
        public static List<Direction> ShapeToDir(char shape)
        {
            switch (shape)
            {
                case '|': return new List<Direction> { Direction.Up, Direction.Down };
                case '-': return new List<Direction> { Direction.Left, Direction.Right };
                case 'L': return new List<Direction> { Direction.Up, Direction.Right };
                case 'J': return new List<Direction> { Direction.Left, Direction.Up };
                case '7': return new List<Direction> { Direction.Left, Direction.Down };
                case 'F': return new List<Direction> { Direction.Down, Direction.Right };
                default:
                    throw new Exception("Ceci n'est pas une pipe");
            }
        }

        public PipeNode(char inputChar)
        {
            PipeShape = inputChar;
            if (PipeShape == 'S')
            {
                IsStart = true;
                InLoop = true;
            }
        }

        public char PipeShape { get; set; }
        public bool IsStart { get; set; } = false;

        public bool InLoop { get; set; } = false;

    }
}
