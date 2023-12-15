namespace AOC2023Backend.Days
{
    public class DayFifteen : Day
    {
        public override string PartOne(string input)
        {
            var steps = input.TrimEnd().Split(',');

            var results = steps.Select(x => HASHMAP.HashAlgorithm(x)).ToList();

            var sum = 0;
            foreach (var step in results)
            {
                sum += step;
            }
            return sum.ToString();
        }



        public override string PartTwo(string input)
        {
            var steps = input.TrimEnd().Split(',');

            var hashMap = new HASHMAP();

            foreach (var step in steps)
            {
                hashMap.PerformStep(step);
            }

            var sum = hashMap.CalculateFocusingPower();
            return sum.ToString();
        }

        public class HASHMAP
        {
            public List<List<(string, int)>> boxes;
            public HASHMAP()
            {
                boxes = new(256);
                for (int i = 0; i < 256; i++)
                {
                    boxes.Add(new());
                }
            }

            public void PerformStep(string input)
            {
                var operationIndexMinus = input.IndexOf('-');
                var operationIndexEquals = input.IndexOf("=");
                var operation = input[operationIndexMinus == -1 ? operationIndexEquals : operationIndexMinus];
                var label = input[..(operationIndexMinus == -1 ? operationIndexEquals : operationIndexMinus)];
                var boxHash = HashAlgorithm(label);

                switch (operation)
                {
                    case '-':
                        boxes[boxHash].RemoveAll(x => x.Item1 == label);
                        break;
                    case '=':
                        var lensNumber = 0;
                        try
                        {
                            lensNumber = int.Parse(input[(operationIndexMinus == -1 ? operationIndexEquals : operationIndexMinus) + 1].ToString());

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw e;
                        }

                        var existingBoxIndex = boxes[boxHash].FindIndex(x => x.Item1 == label);
                        if (existingBoxIndex == -1)
                        {
                            boxes[boxHash].Add((label, lensNumber));
                        }
                        else
                        {
                            boxes[boxHash][existingBoxIndex] = (label, lensNumber);
                        }
                        break;
                    default:
                        throw new Exception();
                }
            }

            public int CalculateFocusingPower()
            {
                var power = 0;
                for (int i = 0; i < boxes.Count; i++)
                {
                    for (int j = 0; j < boxes[i].Count; j++)
                    {
                        var boxPower = (i + 1) * (j + 1) * boxes[i][j].Item2;
                        power += boxPower;
                    }
                }
                return power;
            }

            public static int HashAlgorithm(string input)
            {
                var currentValue = 0;

                foreach (char ch in input)
                {
                    currentValue += ch;
                    currentValue *= 17;
                    currentValue %= 256;
                }
                return currentValue;
            }
        }
    }
}
