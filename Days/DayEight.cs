using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace AOC2023Backend.Days
{
    public class DayEight : Day
    {
        public override string PartOne(string input)
        {
            var lines = ParseInput(input);

            var nodes = Node.GetAllNodesMapped(lines.Skip(2).ToList());
            var moveCount = (nodes.Find(x => x.Key == "AAA") ?? throw new Exception()).DistanceToGoal(lines[0], false);
            return moveCount.ToString();
        }

        public override string PartTwo(string input)
        {
            var lines = ParseInput(input);

            var nodes = Node.GetAllNodesMapped(lines.Skip(2).ToList());
            var startNodes = nodes.FindAll(x => x.Key.EndsWith('A'));
            var moveCounts = startNodes.Select(x => x.DistancesToGoals(lines[0])).ToList();
            var loopNums = moveCounts.Select(x => x[1].Item1 - x[0].Item1).OrderByDescending(x => x).ToList();
            var primes = FindAllPrimes(loopNums[0]);
            var primeFactors = loopNums.Select(x => FindPrimeFactors(x, ref primes)).ToList();
            long multNum = primeFactors[0][1];
            foreach(var factor in primeFactors)
            {
                multNum *= factor[0];
            }
            return multNum.ToString();
        }

        public List<int> FindAllPrimes(int upTo)
        {
            var maxNum = upTo / 2;
            var allPrimes = new List<int> { 2 };

            for (int i = 3; i <= maxNum; i += 2)
            {
                var isPrime = true;
                for (int j = 0; j < allPrimes.Count; j++)
                {
                    if (i % allPrimes[j] == 0)
                    {
                        isPrime = false; break;
                    }
                }
                if (isPrime)
                {
                    allPrimes.Add(i);
                }
            }
            return allPrimes;
        }
        public List<int> FindPrimeFactors(int input, ref List<int> primes)
        {
            var retPrimes = new List<int>();
            var numToUse = input;
            if (input % 2 == 0)
            {
                do
                {
                    retPrimes.Add(2);
                    numToUse = input / 2;
                } while (numToUse % 2 == 0);
            }



            for (var i = 0; i <= primes.Count && primes[i] <= numToUse && numToUse > 1;)
            {
                if (numToUse % primes[i] == 0)
                {
                    retPrimes.Add(primes[i]);
                    numToUse = input / primes[i];
                    continue;
                }
                i++;
            }
            return retPrimes;
        }
    }



    public class Node
    {
        private Node? _leftNode;
        private Node? _rightNode;
        public string Key { get; set; }
        public string LeftNodeKey { get; set; }
        public string RightNodeKey { get; set; }
        public Node LeftNode { get => _leftNode ?? throw new Exception("Left Node Was Null"); set => _leftNode = value; }
        public Node RightNode { get => _rightNode ?? throw new Exception("Right Node Was Null"); set => _rightNode = value; }

        public Node(string line)
        {
            var leftRight = line.Split(" = ");
            Key = leftRight[0];
            var nodes = leftRight[1].Substring(1, leftRight[1].Length - 2).Split(", ").ToList();
            LeftNodeKey = nodes[0];
            RightNodeKey = nodes[1];
        }

        public static List<Node> GetAllNodesMapped(List<string> input)
        {
            Dictionary<string, Node> nodes = new();

            foreach (var line in input)
            {
                var node = new Node(line);
                nodes.Add(node.Key, node);
            }

            foreach (var keyValuePair in nodes)
            {
                nodes.TryGetValue(keyValuePair.Value.LeftNodeKey, out var leftNode);
                nodes.TryGetValue(keyValuePair.Value.RightNodeKey, out var rightNode);
                keyValuePair.Value.LeftNode = leftNode ?? throw new Exception("Left Node Should Not Be Null");
                keyValuePair.Value.RightNode = rightNode ?? throw new Exception("Right Node Should Not Be Null");
            }
            return nodes.Values.OrderBy(x => x.Key).ToList();
        }

        public int DistanceToGoal(string commands, bool partTwo)
        {
            var currentNode = this;
            var stepNum = 0;
            var moveNum = 0;
            while (currentNode.Key != "ZZZ")
            {
                currentNode = currentNode.GetNextNode(commands[stepNum++]);
                moveNum++;
                if (stepNum >= commands.Length)
                {
                    stepNum = 0;
                }
            }
            return moveNum;
        }

        public List<Tuple<int, Node>> DistancesToGoals(string commands)
        {
            var retList = new List<Tuple<int, Node>>();
            var currentNode = this;
            var stepNum = 0;
            var moveNum = 0;
            while (retList.Count < 2)
            {
                currentNode = currentNode.GetNextNode(commands[stepNum++]);
                moveNum++;
                if (stepNum >= commands.Length)
                {
                    stepNum = 0;
                }
                if (currentNode.Key.EndsWith('Z'))
                {
                    retList.Add(new(moveNum, currentNode));
                }
            }
            return retList;
        }


        public Node GetNextNode(char dir)
        {
            if (dir == 'L')
            {
                return LeftNode;
            }
            else
            {
                return RightNode;
            }
        }
    }
}