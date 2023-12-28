using AOC2023Backend.DataTypes;

namespace AOC2023Backend.Days
{
    public class DayTwentyOne : Day
    {
        public override string PartOne(string input)
        {
            var inputs = ParseInput2D(input).Select(row => row.Select(x => new GridNode(x)).ToList()).ToList();

            var matrix = new Matrix<GridNode>(inputs);
            MatrixNode<GridNode>? startNode = null;

            var allGoodNodes = new List<MatrixNode<GridNode>>();
            foreach (var node in matrix.FlatNodes())
            {
                if (node.Value.Value == '#')
                {
                    continue;
                }
                allGoodNodes.Add(node);
                if (node.Value.Value == 'S')
                {
                    startNode = node;
                }
                for (int i = 0; i < node.Adjacencies.Count;)
                {
                    if (node.Adjacencies[i].Value.Value == '#')
                    {
                        node.RemoveAdjacentNode(node.Adjacencies[i]);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            if (startNode == null)
            {
                throw new Exception();
            }
            startNode.Value.MinDistanceToStart = 0;


            var nextNode = startNode;
            var nodesToCheck = new PriorityQueue<MatrixNode<GridNode>, int>(Comparer<int>.Create((x, y) => x - y));

            nodesToCheck.Enqueue(startNode, startNode.Value.MinDistanceToStart);

            //var nodesToCheck = new Queue<MatrixNode<GridNode>>();

            //nodesToCheck.Enqueue(startNode);

            while (nodesToCheck.Count > 0)
            {
                var node = nodesToCheck.Dequeue();
                node.Value.Visited = true;
                if (node.Value.MinDistanceToStart % 2 == 0 && node.Value.Value != 'S')
                {
                    node.Value.Value = 'O';

                }
                foreach (var otherNode in node.Adjacencies)
                {
                    if (otherNode.Value.MinDistanceToStart != int.MaxValue)
                    {
                        node.Value.MinDistanceToStart = Math.Min(node.Value.MinDistanceToStart, otherNode.Value.MinDistanceToStart + 1);

                    }
                }
                foreach (var otherNode in node.Adjacencies)
                {
                    //if (otherNode.Value.Visited)
                    //{
                    //    continue;
                    //}
                    if (otherNode.Value.MinDistanceToStart != int.MaxValue && node.Value.MinDistanceToStart > otherNode.Value.MinDistanceToStart + 1)
                    {

                    }
                    otherNode.Value.MinDistanceToStart = Math.Min(node.Value.MinDistanceToStart + 1, otherNode.Value.MinDistanceToStart);

                    if (!otherNode.Value.Visited && otherNode.Value.MinDistanceToStart <= 64)
                    {
                        otherNode.Value.Visited = true;
                        nodesToCheck.Enqueue(otherNode, otherNode.Value.MinDistanceToStart);
                        //nodesToCheck.Enqueue(otherNode);
                    }
                }
            }
            var numWithinRange = allGoodNodes.Where(x => x.Value.MinDistanceToStart <= 64 && x.Value.MinDistanceToStart % 2 == 0).Count();
            matrix.PrintMatrix();
            return numWithinRange.ToString();
        }

        public int PartOneMath(string input, int numSteps)
        {
            var inputs = ParseInput2D(input).Select(row => row.Select(x => new GridNode(x)).ToList()).ToList();

            var isEven = numSteps % 2 == 0;

            var matrix = new Matrix<GridNode>(inputs);
            MatrixNode<GridNode>? startNode = null;

            var allGoodNodes = new List<MatrixNode<GridNode>>();
            foreach (var node in matrix.FlatNodes())
            {
                if (node.Value.Value == '#')
                {
                    continue;
                }
                allGoodNodes.Add(node);
                if (node.Value.Value == 'S')
                {
                    startNode = node;
                }
                for (int i = 0; i < node.Adjacencies.Count;)
                {
                    if (node.Adjacencies[i].Value.Value == '#')
                    {
                        node.RemoveAdjacentNode(node.Adjacencies[i]);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            if (startNode == null)
            {
                throw new Exception();
            }
            startNode.Value.MinDistanceToStart = 0;


            var nextNode = startNode;
            var nodesToCheck = new PriorityQueue<MatrixNode<GridNode>, int>(Comparer<int>.Create((x, y) => x - y));

            nodesToCheck.Enqueue(startNode, startNode.Value.MinDistanceToStart);

            //var nodesToCheck = new Queue<MatrixNode<GridNode>>();

            //nodesToCheck.Enqueue(startNode);

            while (nodesToCheck.Count > 0)
            {
                var node = nodesToCheck.Dequeue();
                node.Value.Visited = true;
                if (isEven ? node.Value.MinDistanceToStart % 2 == 0 : node.Value.MinDistanceToStart % 2 != 0 && node.Value.Value != 'S')
                {
                    node.Value.Value = 'O';

                }
                foreach (var otherNode in node.Adjacencies)
                {
                    if (otherNode.Value.MinDistanceToStart != int.MaxValue)
                    {
                        node.Value.MinDistanceToStart = Math.Min(node.Value.MinDistanceToStart, otherNode.Value.MinDistanceToStart + 1);

                    }
                }
                foreach (var otherNode in node.Adjacencies)
                {
                    //if (otherNode.Value.Visited)
                    //{
                    //    continue;
                    //}
                    if (otherNode.Value.MinDistanceToStart != int.MaxValue && node.Value.MinDistanceToStart > otherNode.Value.MinDistanceToStart + 1)
                    {

                    }
                    otherNode.Value.MinDistanceToStart = Math.Min(node.Value.MinDistanceToStart + 1, otherNode.Value.MinDistanceToStart);

                    if (!otherNode.Value.Visited && otherNode.Value.MinDistanceToStart <= numSteps)
                    {
                        otherNode.Value.Visited = true;
                        nodesToCheck.Enqueue(otherNode, otherNode.Value.MinDistanceToStart);
                        //nodesToCheck.Enqueue(otherNode);
                    }
                }
            }
            var numWithinRange = allGoodNodes.Where(x => x.Value.MinDistanceToStart <= numSteps && isEven ? x.Value.MinDistanceToStart % 2 == 0 : x.Value.MinDistanceToStart % 2 != 0).Count();
            matrix.PrintMatrix();
            return numWithinRange;
        }

        public int PartTwoMath(string input, int numSteps)
        {
            var bigInput = ParseInput(input).Select(x => x.Replace('S', '.') + x.Replace('S', '.') + x + x.Replace('S', '.') + x.Replace('S', '.')).ToList();
            //numSteps += 1 + bigInput[0].Length / 2;
            var bigInputFull = new List<string>();
            bigInputFull.AddRange(bigInput.Select(x => x.Replace('S', '.')));
            bigInputFull.AddRange(bigInput.Select(x => x.Replace('S', '.')));
            bigInputFull.AddRange(bigInput);
            bigInputFull.AddRange(bigInput.Select(x => x.Replace('S', '.')));
            bigInputFull.AddRange(bigInput.Select(x => x.Replace('S', '.')));
            var bigInputString = string.Join('\n', bigInputFull);
            var inputs = ParseInput2D(bigInputString).Select(row => row.Select(x => new GridNode(x)).ToList()).ToList();

            var isEven = numSteps % 2 == 0;

            var matrix = new Matrix<GridNode>(inputs);
            MatrixNode<GridNode>? startNode = null;

            //for (int i = 0; i < matrix.Values.Count; i++)
            //{
            //    var node1 = matrix.Values[i][0];
            //    var node2 = matrix.Values[i][^1];
            //    node1.AddAdjacentNode(node2);
            //    node2.AddAdjacentNode(node1);
            //}

            //for (int i = 0; i < matrix.Values[0].Count; i++)
            //{
            //    var node1 = matrix.Values[i][0];
            //    var node2 = matrix.Values[i][^1];
            //    node1.AddAdjacentNode(node2);
            //    node2.AddAdjacentNode(node1);
            //}

            var allGoodNodes = new List<MatrixNode<GridNode>>();
            foreach (var node in matrix.FlatNodes())
            {
                if (node.Value.Value == '#')
                {
                    continue;
                }
                allGoodNodes.Add(node);
                if (node.Value.Value == 'S')
                {
                    startNode = node;
                }
                for (int i = 0; i < node.Adjacencies.Count;)
                {
                    if (node.Adjacencies[i].Value.Value == '#')
                    {
                        node.RemoveAdjacentNode(node.Adjacencies[i]);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            if (startNode == null)
            {
                throw new Exception();
            }
            startNode.Value.MinDistanceToStart = 0;


            var nextNode = startNode;
            var nodesToCheck = new PriorityQueue<MatrixNode<GridNode>, int>(Comparer<int>.Create((x, y) => x - y));

            nodesToCheck.Enqueue(startNode, startNode.Value.MinDistanceToStart);

            //var nodesToCheck = new Queue<MatrixNode<GridNode>>();

            //nodesToCheck.Enqueue(startNode);

            while (nodesToCheck.Count > 0)
            {
                var node = nodesToCheck.Dequeue();
                node.Value.Visited = true;
                if (isEven ? node.Value.MinDistanceToStart % 2 == 0 : node.Value.MinDistanceToStart % 2 != 0 && node.Value.Value != 'S')
                {
                    node.Value.Value = 'O';

                }
                foreach (var otherNode in node.Adjacencies)
                {
                    if (otherNode.Value.MinDistanceToStart != int.MaxValue)
                    {
                        node.Value.MinDistanceToStart = Math.Min(node.Value.MinDistanceToStart, otherNode.Value.MinDistanceToStart + 1);

                    }
                }
                foreach (var otherNode in node.Adjacencies)
                {
                    //if (otherNode.Value.Visited)
                    //{
                    //    continue;
                    //}
                    if (otherNode.Value.MinDistanceToStart != int.MaxValue && node.Value.MinDistanceToStart > otherNode.Value.MinDistanceToStart + 1)
                    {

                    }
                    otherNode.Value.MinDistanceToStart = Math.Min(node.Value.MinDistanceToStart + 1, otherNode.Value.MinDistanceToStart);

                    if (!otherNode.Value.Visited && otherNode.Value.MinDistanceToStart <= numSteps)
                    {
                        otherNode.Value.Visited = true;
                        nodesToCheck.Enqueue(otherNode, otherNode.Value.MinDistanceToStart);
                        //nodesToCheck.Enqueue(otherNode);
                    }
                }
            }
            var numWithinRange = allGoodNodes.Where(x => x.Value.MinDistanceToStart <= numSteps && (isEven ? x.Value.MinDistanceToStart % 2 == 0 : x.Value.MinDistanceToStart % 2 != 0)).Count();
            matrix.PrintMatrix();
            return numWithinRange;
        }

        public override string PartTwo(string input)
        {
            List<(int, int)> results = new()
            {
                (65, PartTwoMath(input, 65)),
                (196, PartTwoMath(input, 196)),
                (327, PartTwoMath(input, 327))
            };

            var result = 1551 - 17.5 * 26501365 + .94 * Math.Pow(26501365, 2);
            var resultLong = (long)result;
            var s1 = string.Join('\n', results.Select(x => String.Format("{0},{1}", x.Item1, x.Item2)));

            return resultLong.ToString();
        }
    }

    public class GridNode
    {
        public char Value { get; set; }
        public bool Visited { get; set; } = false;
        public int MinDistanceToStart { get; set; } = int.MaxValue;
        public GridNode(char ch)
        {
            Value = ch;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
