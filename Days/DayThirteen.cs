namespace AOC2023Backend.Days
{
    public class DayThirteen : Day
    {
        public override string PartOne(string input)
        {
            var inputs = input.Split("\n\n");

            var allInputs = inputs.Select(x => ParseInputRowsAndColumns(x)).ToList();

            var total = 0;
            foreach (var mirror in allInputs)
            {
                var verticalSolution = FindReflections(mirror.Columns);
                if (verticalSolution > 0)
                {
                    total += verticalSolution;
                }
                var horizontalSolution = FindReflections(mirror.Rows);
                total += 100 * horizontalSolution;
            }
            return total.ToString();
        }

        public int FindReflections(List<List<char>> mirror)
        {
            for (int i = 1; i < mirror.Count; i++)
            {
                //Check for reflection with line to the left of self
                var reflection = true;
                var leftIndex = i - 1;
                var rightIndex = i;
                while (leftIndex >= 0 && rightIndex < mirror.Count && reflection)
                {
                    for (int rowIndex = 0; rowIndex < mirror[0].Count; rowIndex++)
                    {
                        if (mirror[leftIndex][rowIndex] != mirror[rightIndex][rowIndex])
                        {
                            reflection = false;
                            break;
                        }
                    }
                    leftIndex--;
                    rightIndex++;
                }

                //if (!reflection)
                //{
                //    reflection = true;
                //    //check for reflection excluding self
                //    leftIndex = i - 1;
                //    rightIndex = i + 1;
                //    if (rightIndex == mirror.Count || leftIndex == -1)
                //    {
                //        reflection = false;
                //    }
                while (leftIndex >= 0 && rightIndex < mirror.Count && reflection)
                {
                    for (int rowIndex = 0; rowIndex < mirror[0].Count; rowIndex++)
                    {
                        if (mirror[leftIndex][rowIndex] != mirror[rightIndex][rowIndex])
                        {
                            reflection = false;
                            break;
                        }
                    }
                    leftIndex--;
                    rightIndex++;
                }
                //}

                if (reflection)
                {
                    return i;
                }
            }
            return 0;
        }

        public int FindReflectionsPart2(List<List<char>> mirror)
        {
            for (int i = 1; i < mirror.Count; i++)
            {
                //Check for reflection with line to the left of self
                var reflection = true;
                var leftIndex = i - 1;
                var rightIndex = i;
                var smudgeUsed = false;
                while (leftIndex >= 0 && rightIndex < mirror.Count && reflection)
                {
                    var numSame = mirror[0].Count;
                    for (int rowIndex = 0; rowIndex < mirror[0].Count; rowIndex++)
                    {
                        if (mirror[leftIndex][rowIndex] != mirror[rightIndex][rowIndex])
                        {
                            numSame--;
                        }
                    }
                    if (numSame == mirror[0].Count - 1 && !smudgeUsed)
                    {
                        smudgeUsed = true;
                    }
                    else if (numSame < mirror[0].Count)
                    {
                        reflection = false;
                        break;
                    }
                    leftIndex--;
                    rightIndex++;
                }

                if (reflection && smudgeUsed)
                {
                    return i;
                }
            }
            return 0;
        }

        public override string PartTwo(string input)
        {
            var inputs = input.Split("\n\n");

            var allInputs = inputs.Select(x => ParseInputRowsAndColumns(x)).ToList();

            var total = 0;
            foreach (var mirror in allInputs)
            {
                var verticalSolutionP1 = FindReflections(mirror.Columns);
                var horizontalSolutionP1 = FindReflections(mirror.Rows);
                var verticalSolution = FindReflectionsPart2(mirror.Columns);
                var horizontalSolution = FindReflectionsPart2(mirror.Rows);

                var verticalSolutionCorrect = horizontalSolutionP1 == horizontalSolution && verticalSolution > 0;
                var horizontalSolutionCorrect = verticalSolutionP1 == verticalSolution && horizontalSolution > 0;
                if (verticalSolution>0)
                {
                    total += verticalSolution;
                }
                else
                {

                    total += 100 * horizontalSolution;
                }
            }
            return total.ToString();
        }
    }
}
