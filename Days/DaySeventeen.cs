using AOC2023Backend.DataTypes;

namespace AOC2023Backend.Days
{
    public class DaySeventeen : Day
    {
        public override string PartOne(string input)
        {
            var values = ParseInput2D(input).Select(x => x.Select(y => int.Parse(y.ToString())).ToList()).ToList();

            var djikstraMatrix = DjikstraMatrix.DjikstraMatrixFactory(values);

            var shortestPath = djikstraMatrix.FindShortest();
            return shortestPath.ToString();
        }

        public override string PartTwo(string input)
        {
            throw new NotImplementedException();
        }
    }

    public class DjikstraValue
    {
        public Dictionary<Direction, List<(MatrixNode<DjikstraValue>, int)>> AdjacenciesFromDirection { get; set; } = new();
        public int Weight { get; set; }

        //The key here is the direction of travel into the node
        public Dictionary<Direction, bool> VisitedFromDir { get; set; } = new();
        //The key here is the direction of travel into the node
        public Dictionary<Direction, int> DistanceFromStartFromDirection { get; set; } = new();
        public DjikstraValue(int weight)
        {
            Weight = weight;
            DistanceFromStartFromDirection[Direction.Up] = int.MaxValue;
            DistanceFromStartFromDirection[Direction.Right] = int.MaxValue;
            VisitedFromDir[Direction.Down] = false;
            VisitedFromDir[Direction.Up] = false;
            VisitedFromDir[Direction.Left] = false;
            VisitedFromDir[Direction.Right] = false;
        }
        public int GetDistanceFromStartFromDirection(Direction direction)
        {
            var actualDirection = direction;
            if (actualDirection == Direction.Down)
            {
                actualDirection = Direction.Up;
            }
            else if (actualDirection == Direction.Left)
            {
                actualDirection = Direction.Right;
            }
            return DistanceFromStartFromDirection[actualDirection];
        }

        public void SetDistanceFromStartFromDirection(Direction direction, int distance)
        {
            var actualDirection = direction;
            if (actualDirection == Direction.Down)
            {
                actualDirection = Direction.Up;
            }
            else if (actualDirection == Direction.Left)
            {
                actualDirection = Direction.Right;
            }
            DistanceFromStartFromDirection[actualDirection] = distance;
        }
    }

    public class DjikstraMatrix : Matrix<DjikstraValue>
    {
        public int MaxCost { get; set; }
        public static DjikstraMatrix DjikstraMatrixFactory(List<List<int>> input)
        {
            var values = input.Select(x => x.Select(y => new DjikstraValue(y)).ToList()).ToList();
            var matrix = new DjikstraMatrix(values);
            matrix.Initialize();
            return matrix;
        }

        public List<(MatrixNode<DjikstraValue>, Direction)> GetNextNodes(MatrixNode<DjikstraValue> node, Direction inputDir)
        {
            var retNodes = new List<(MatrixNode<DjikstraValue>, Direction)>();
            node.Value.VisitedFromDir[inputDir] = true;
            var directionDict = new Dictionary<Direction, List<MatrixNode<DjikstraValue>>>();
            foreach (var dir in new List<Direction> { Direction.Up, Direction.Right, Direction.Down, Direction.Left })
            {
                if (dir == inputDir) continue;
                else if (dir == Direction.Up && inputDir == Direction.Down) continue;
                else if (dir == Direction.Down && inputDir == Direction.Up) continue;
                else if (dir == Direction.Left && inputDir == Direction.Right) continue;
                else if (dir == Direction.Right && inputDir == Direction.Left) continue;
                directionDict[dir] = new();
                var node1 = node.GetFromDirection(dir);
                if (node1 == null)
                {
                    continue;
                }
                var cost = node.Value.GetDistanceFromStartFromDirection(inputDir);
                if (cost == int.MaxValue)
                {
                    throw new Exception();
                }
                cost += node1.Value.Weight;
                node1.Value.SetDistanceFromStartFromDirection(dir, Math.Min(cost, node1.Value.GetDistanceFromStartFromDirection(dir)));
                if (!node1.Value.VisitedFromDir[dir])
                {
                    directionDict[dir].Add(node1);
                }
                var node2 = node1.GetFromDirection(dir);
                if (node2 == null)
                {
                    continue;
                }
                else
                {
                    cost += node2.Value.Weight;
                    node2.Value.SetDistanceFromStartFromDirection(dir, Math.Min(cost, node2.Value.GetDistanceFromStartFromDirection(dir)));
                    if (!node2.Value.VisitedFromDir[dir])
                    {
                        directionDict[dir].Add(node2);
                    }
                }
                var node3 = node2.GetFromDirection(dir);
                if (node3 == null)
                {
                    continue;
                }
                else
                {
                    cost += node3.Value.Weight;
                    node3.Value.SetDistanceFromStartFromDirection(dir, Math.Min(cost, node3.Value.GetDistanceFromStartFromDirection(dir)));
                    if (!node3.Value.VisitedFromDir[dir])
                    {
                        directionDict[dir].Add(node3);
                    }
                }
            }
            foreach (var keyValuePair in directionDict)
            {
                keyValuePair.Value.ForEach(x =>
                {
                    retNodes.Add((x, keyValuePair.Key));
                });
            }
            return retNodes;
        }

        public List<(MatrixNode<DjikstraValue>, Direction)> GetNextNodes2(MatrixNode<DjikstraValue> node, Direction inputDir, int stepsRemaining)
        {
            var retNodes = new List<(MatrixNode<DjikstraValue>, Direction)>();
            node.Value.VisitedFromDir[inputDir] = true;
            var directionDict = new Dictionary<Direction, List<MatrixNode<DjikstraValue>>>();
            foreach (var dir in new List<Direction> { Direction.Up, Direction.Right, Direction.Down, Direction.Left })
            {
                if (dir == inputDir) continue;
                else if (dir == Direction.Up && inputDir == Direction.Down) continue;
                else if (dir == Direction.Down && inputDir == Direction.Up) continue;
                else if (dir == Direction.Left && inputDir == Direction.Right) continue;
                else if (dir == Direction.Right && inputDir == Direction.Left) continue;
                directionDict[dir] = new();
                var node1 = node.GetFromDirection(dir);
                if (node1 == null)
                {
                    continue;
                }
                var cost = node.Value.GetDistanceFromStartFromDirection(inputDir);
                if (cost == int.MaxValue)
                {
                    throw new Exception();
                }
                cost += node1.Value.Weight;
                node1.Value.SetDistanceFromStartFromDirection(dir, Math.Min(cost, node1.Value.GetDistanceFromStartFromDirection(dir)));
                if (!node1.Value.VisitedFromDir[dir])
                {
                    directionDict[dir].Add(node1);
                }
                var node2 = node1.GetFromDirection(dir);
                if (node2 == null)
                {
                    continue;
                }
                else
                {
                    cost += node2.Value.Weight;
                    node2.Value.SetDistanceFromStartFromDirection(dir, Math.Min(cost, node2.Value.GetDistanceFromStartFromDirection(dir)));
                    if (!node2.Value.VisitedFromDir[dir])
                    {
                        directionDict[dir].Add(node2);
                    }
                }
                var node3 = node2.GetFromDirection(dir);
                if (node3 == null)
                {
                    continue;
                }
                else
                {
                    cost += node3.Value.Weight;
                    node3.Value.SetDistanceFromStartFromDirection(dir, Math.Min(cost, node3.Value.GetDistanceFromStartFromDirection(dir)));
                    if (!node3.Value.VisitedFromDir[dir])
                    {
                        directionDict[dir].Add(node3);
                    }
                }
            }
            foreach (var keyValuePair in directionDict)
            {
                keyValuePair.Value.ForEach(x =>
                {
                    retNodes.Add((x, keyValuePair.Key));
                });
            }
            return retNodes;
        }

        public int FindShortest()
        {
            var firstNode = Values.First().First();
            var endNode = Values.Last().Last();

            firstNode.Value.SetDistanceFromStartFromDirection(Direction.Up, firstNode.Value.Weight);
            firstNode.Value.SetDistanceFromStartFromDirection(Direction.Right, firstNode.Value.Weight);

            var possibleNodes = new PriorityQueue<(MatrixNode<DjikstraValue>, Direction), int>();
            possibleNodes.Enqueue((firstNode, Direction.Right), firstNode.Value.GetDistanceFromStartFromDirection(Direction.Right));
            possibleNodes.Enqueue((firstNode, Direction.Down), firstNode.Value.GetDistanceFromStartFromDirection(Direction.Down));

            var nextNode = possibleNodes.Peek();
            while (nextNode.Item1 != endNode && possibleNodes.Count > 0)
            {
                nextNode = possibleNodes.Dequeue();
                //possibleNodes.RemoveAt(possibleNodes.Count - 1);

                var newPossibles = GetNextNodes(nextNode.Item1, nextNode.Item2);
                foreach (var possibility in newPossibles)
                {
                    //if (possibility.Item1.Value.GetDistanceFromStartFromDirection(possibility.Item2) < MaxCost)
                    //{
                    possibleNodes.Enqueue(possibility, possibility.Item1.Value.GetDistanceFromStartFromDirection(possibility.Item2) + endNode.YIndex - possibility.Item1.YIndex + endNode.XIndex - possibility.Item1.YIndex);
                    //}
                    //else
                    //{

                    //}
                }
            }

            return nextNode.Item1.Value.GetDistanceFromStartFromDirection(nextNode.Item2) - firstNode.Value.Weight;
        }
        public void Initialize()
        {
            //Values.ForEach(row =>
            //{
            //    foreach (var item in row)
            //    {
            //        var directionList = new Dictionary<Direction, List<MatrixNode<DjikstraValue>>>();
            //        foreach (var dir in new List<Direction> { Direction.Up, Direction.Right, Direction.Down, Direction.Left })
            //        {
            //            directionList[dir] = new();
            //            var node1 = item.GetFromDirection(dir);
            //            if (node1 == null)
            //            {
            //                continue;
            //            }
            //            else
            //            {
            //                directionList[dir].Add(node1);
            //            }
            //            var node2 = node1.GetFromDirection(dir);
            //            if (node2 == null)
            //            {
            //                continue;
            //            }
            //            else
            //            {
            //                directionList[dir].Add(node2);
            //            }
            //            var node3 = node2.GetFromDirection(dir);
            //            if (node3 == null)
            //            {
            //                continue;
            //            }
            //            else
            //            {
            //                directionList[dir].Add(node3);
            //            }
            //        }

            //        var nodesFromDirection = new 
            //    }
            //});
        }

        public DjikstraMatrix(List<List<DjikstraValue>> input, bool reverseYAxis = false) : base(input, reverseYAxis)
        {
            MaxCost = 0;
            for (int i = 0; i < input.Count; i++)
            {
                MaxCost += input[i][0].Weight;
            }
            for (int i = 0; i < input[0].Count; i++)
            {
                MaxCost += input[input.Count - 1][i].Weight;
            }
        }


    }
}
