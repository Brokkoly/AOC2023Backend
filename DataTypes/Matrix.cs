namespace AOC2023Backend.DataTypes
{
    public enum Direction
    {
        UpLeft, UpRight, Up, Down, Left, Right, DownLeft, DownRight
    }

    public class Matrix<T>
    {
        public List<List<MatrixNode<T>>> Values = new();
        public bool ReverseYAxis { get; set; }
        public int XCount => Values[0].Count;
        public int YCount => Values.Count;


        public Matrix(List<List<T>> input, bool reverseYAxis = false)
        {
            for (var yIndex = 0; yIndex < input.Count; yIndex++)
            {
                List<MatrixNode<T>> currentRow = new();
                for (var xIndex = 0; xIndex < input[yIndex].Count; xIndex++)
                {
                    currentRow.Add(new MatrixNode<T>(input[yIndex][xIndex], this, xIndex, yIndex));
                }
                Values.Add(currentRow);
            }
            foreach (var row in Values)
            {
                foreach (var node in row)
                {
                    var dirs = new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
                    var adjacents = dirs.Select(x => node.GetFromDirection(x)).Where(x => x != null).ToList();
                    node.Adjacencies.AddRange(adjacents);
                }
            }
        }
        public MatrixNode<T>? GetNode(int xIndex, int yIndex)
        {
            if (xIndex < 0 || yIndex < 0 || yIndex > Values.Count - 1 || xIndex > Values[0].Count - 1)
            {
                return null;
            }
            return Values[yIndex][xIndex];
        }

        public List<MatrixNode<T>> FlatNodes()
        {
            var retList = new List<MatrixNode<T>>();

            foreach (var row in Values)
            {
                retList.AddRange(row);
            }

            return retList;
        }
        public void PrintMatrix()
        {
            for (var yIndex = 0; yIndex < Values.Count; yIndex++)
            {
                Console.Write('\n');
                for (var xIndex = 0; xIndex < Values[yIndex].Count; xIndex++)
                {
                    Console.Write(GetNode(xIndex, yIndex).Value.ToString());
                }
            }
            Console.Write('\n');
        }
    }
    public class MatrixNode<T>
    {
        public override string ToString()
        {
            return Value.ToString();
        }
        public List<MatrixNode<T>> Adjacencies { get; set; } = new();
        public int XIndex { get; set; }
        public int YIndex { get; set; }
        public T Value { get; set; }
        public Matrix<T> Parent { get; set; }

        public bool ReverseYAxis { get; set; }
        public MatrixNode(T value, Matrix<T> parent, int xIndex, int yIndex, bool reverseYAxis = false)
        {
            Value = value;
            XIndex = xIndex;
            YIndex = yIndex;
            Parent = parent;
            ReverseYAxis = reverseYAxis;
        }
        /// <summary>
        /// Checks if two nodes are the same node based on having the same parent and same indices
        /// </summary>
        /// <param name="node"></param>
        /// <returns>True if they are the same node</returns>
        public bool SameNode(MatrixNode<T> node)
        {
            return Parent == node.Parent && node.XIndex == XIndex && node.YIndex == YIndex;
        }

        public MatrixNode<T>? GetFromDirection(Direction direction)
        {
            int xOffset, yOffset;
            //Get X offset
            switch (direction)
            {
                case Direction.UpLeft:
                case Direction.UpRight:
                case Direction.Up:
                    yOffset = -1; break;
                case Direction.DownLeft:
                case Direction.DownRight:
                case Direction.Down:
                    yOffset = 1; break;
                default:
                    yOffset = 0; break;
            }
            switch (direction)
            {
                case Direction.Left:
                case Direction.UpLeft:
                case Direction.DownLeft:
                    xOffset = -1; break;
                case Direction.Right:
                case Direction.UpRight:
                case Direction.DownRight:
                    xOffset = 1; break;
                default:
                    xOffset = 0; break;
            }

            int newX = XIndex + xOffset;
            int newY = YIndex + yOffset;

            return Parent.GetNode(newX, newY);
        }

        public void RemoveAdjacentNode(MatrixNode<T> node) { Adjacencies.Remove(node); }

        public void AddAdjacentNode(MatrixNode<T> node) { Adjacencies.Add(node); }
    }
}
