using AOC2023Backend.DataTypes;

namespace AOC2023Backend.Days
{
    public class DayEighteen : Day
    {
        public override string PartOne(string input)
        {
            var actions = ParseInput(input).Select(x => DiggerAction.DiggerActionFactory(x)).ToList();


            long currentX = 0;
            long currentY = 0;
            long currentSum = 0;
            var firstVertex = new Vertex() { XIndex = currentX, YIndex = currentY };
            var vertices = new List<Vertex> { firstVertex };
            var previousVertex = firstVertex;
            for (var actionIndex = 0; actionIndex < actions.Count; actionIndex++)
            {
                var action = actions[actionIndex];
                //var nextAction = actions[actionIndex + 1];
                switch (action.Direction)
                {
                    case Direction.Up:
                        currentY += action.Distance;
                        break;
                    case Direction.Down:
                        currentY -= action.Distance;
                        break;
                    case Direction.Left:
                        currentX -= action.Distance;
                        break;
                    case Direction.Right:
                        currentX += action.Distance;
                        break;
                }
                var newVertex = new Vertex()
                {
                    XIndex = currentX,
                    YIndex = currentY
                };
                currentSum += (previousVertex.XIndex * newVertex.YIndex - previousVertex.YIndex * newVertex.XIndex);
                previousVertex = newVertex;
                vertices.Add(newVertex);
            }
            currentSum += (previousVertex.XIndex * firstVertex.YIndex - previousVertex.YIndex * firstVertex.XIndex);

            long area = Math.Abs(currentSum) / 2;

            long perimeter = 1;
            foreach (var action in actions)
            {
                perimeter += action.Distance;
            }
            area += perimeter / 2 + 1;

            return area.ToString();
        }

        public override string PartTwo(string input)
        {

            var actions = ParseInput(input).Select(x => DiggerAction.DiggerActionFactoryPart2(x)).ToList();
            long currentX = 0;
            long currentY = 0;
            long currentSum = 0;
            var firstVertex = new Vertex() { XIndex = currentX, YIndex = currentY };
            var vertices = new List<Vertex> { firstVertex };
            var previousVertex = firstVertex;
            for (var actionIndex = 0; actionIndex < actions.Count; actionIndex++)
            {
                var action = actions[actionIndex];
                //var nextAction = actions[actionIndex + 1];
                switch (action.Direction)
                {
                    case Direction.Up:
                        currentY += action.Distance;
                        break;
                    case Direction.Down:
                        currentY -= action.Distance;
                        break;
                    case Direction.Left:
                        currentX -= action.Distance;
                        break;
                    case Direction.Right:
                        currentX += action.Distance;
                        break;
                }
                var newVertex = new Vertex()
                {
                    XIndex = currentX,
                    YIndex = currentY
                };
                currentSum += (previousVertex.XIndex * newVertex.YIndex - previousVertex.YIndex * newVertex.XIndex);
                previousVertex = newVertex;
                vertices.Add(newVertex);
            }
            currentSum += (previousVertex.XIndex * firstVertex.YIndex - previousVertex.YIndex * firstVertex.XIndex);

            long area = Math.Abs(currentSum) / 2;

            long perimeter = 1;
            foreach (var action in actions)
            {
                perimeter += action.Distance;
            }
            area += perimeter / 2 + 1;

            return area.ToString();
        }
    }

    public class DiggerAction
    {
        public Direction Direction { get; set; }
        public long Distance { get; set; }
        public char DirectionChar { get; set; }
        public static DiggerAction DiggerActionFactory(string input)
        {
            var splits = input.Split(' ');
            var distance = long.Parse(splits[1]);
            Direction dir;
            switch (splits[0])
            {
                case "R": dir = Direction.Right; break;
                case "U": dir = Direction.Up; break;
                case "L": dir = Direction.Left; break;
                case "D": dir = Direction.Down; break;
                default:
                    throw new Exception();
            }
            var color = long.Parse(splits[2][2..(splits[2].Length - 1)], System.Globalization.NumberStyles.HexNumber);
            return new DiggerAction(dir, distance, splits[0][0]);
        }

        public static DiggerAction DiggerActionFactoryPart2(string input)
        {
            var splits = input.Split(' ');
            var color = long.Parse(splits[1]);
            Direction dir;

            var hex = splits[2][2..(splits[2].Length - 1)];

            var dirChar = '0';

            switch (hex.Last())
            {
                case '0':
                    dir = Direction.Right;
                    dirChar = 'R';
                    break;
                case '3':
                    dir = Direction.Up;
                    dirChar = 'U';
                    break;
                case '2':
                    dir = Direction.Left;
                    dirChar = 'L';
                    break;
                case '1':
                    dir = Direction.Down;
                    dirChar = 'D';
                    break;
                default:
                    throw new Exception();
            }

            var distance = long.Parse(hex[..(hex.Length - 1)], System.Globalization.NumberStyles.HexNumber);
            return new DiggerAction(dir, distance, dirChar);
        }

        public DiggerAction(Direction direction, long distance, char directionChar)
        {
            Direction = direction;
            Distance = distance;
            DirectionChar = directionChar;
        }



    }
    public class Vertex
    {
        public long XIndex;
        public long YIndex;
    }
    public static class Extension
    {
        public static void DigOut(this Matrix<char> myMatrix, List<DiggerAction> actions, int startX, int startY)
        {
            var firstNode = myMatrix.GetNode(startX, startY);
            if (firstNode == null)
            {
                throw new Exception();
            }
            var nextNode = firstNode;
            foreach (var action in actions)
            {
                var distanceLeft = action.Distance;
                //For later inside checking
                if (action.Direction == Direction.Up || action.Direction == Direction.Down)
                {
                    nextNode.Value = action.DirectionChar;
                }

                while (distanceLeft > 0)
                {
                    nextNode = nextNode.GetFromDirection(action.Direction);
                    distanceLeft--;
                    if (nextNode == null)
                    {
                        throw new Exception();
                    }
                    nextNode.Value = action.DirectionChar;
                }
            }
        }

        public static long CalculateTrenchSize(this Matrix<char> myMatrix)
        {
            var wholeSize = 0;
            bool inside = false;
            char lastUpDown = '0';
            foreach (var row in myMatrix.Values)
            {
                inside = false;
                lastUpDown = '0';
                foreach (var node in row)
                {
                    if (node.Value == 'U' || node.Value == 'D')
                    {
                        if (node.Value != lastUpDown)
                        {
                            inside = !inside;
                            lastUpDown = node.Value;
                        }
                    }
                    else if (inside && node.Value != 'L' && node.Value != 'R')
                    {
                        node.Value = '#';
                    }
                }
            }
            foreach (var row in myMatrix.Values)
            {
                foreach (var node in row)
                {
                    switch (node.Value)
                    {
                        case 'U':
                        case 'D':
                        case 'L':
                        case 'R':
                        case '#':
                            wholeSize++;
                            break;
                    }
                }
            }
            return wholeSize;
        }
    }
}
