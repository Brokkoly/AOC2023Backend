using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace AOC2023Backend.Days
{
    public class DayNineteen : Day
    {
        public override string PartOne(string input)
        {
            var rulesAndParts = input.Split("\n\n");
            var rules = rulesAndParts[0].Split('\n').Where(x => !string.IsNullOrEmpty(x)).Select(x => Rule.RuleFactory(x)).ToList();
            var parts = rulesAndParts[1].Split('\n').Where(x => !string.IsNullOrEmpty(x)).Select(x => Part.PartFactory(x)).ToList();

            var ruleMap = new Dictionary<string, Rule>();
            foreach (var rule in rules)
            {
                ruleMap.Add(rule.Name, rule);
            }

            var acceptedParts = parts.Where(part => IsAcceptedPart(part, ref ruleMap));

            var total = 0;

            foreach (var part in acceptedParts)
            {
                foreach (var value in part.PartValues)
                {
                    total += value.Value;
                }
            }
            return total.ToString();
        }

        public bool IsAcceptedPart(Part part, ref Dictionary<string, Rule> rules)
        {
            var outcome = "in";
            while (outcome != "A" && outcome != "R")
            {
                outcome = rules[outcome].GetRuleOutcome(part);
            }
            return outcome == "A";
        }

        public override string PartTwo(string input)
        {
            var rulesAndParts = input.Split("\n\n");
            var rules = rulesAndParts[0].Split('\n').Where(x => !string.IsNullOrEmpty(x)).Select(x => Rule.RuleFactory(x)).ToList();

            var ruleMap = new Dictionary<string, Rule>();
            foreach (var rule in rules)
            {
                ruleMap.Add(rule.Name, rule);
            }





            var total = 0;

            return total.ToString();
        }
    }

    public class SubRuleNode
    {
        private SubRuleNode? _leftNode;
        private SubRuleNode? _rightNode;
        private SubRuleNode? _parentNode;
        public string Key { get; set; }
        public string LeftNodeKey { get; set; }
        public string RightNodeKey { get; set; }
        public SubRuleNode LeftNode { get => _leftNode ?? throw new Exception("Left Node Was Null"); set => _leftNode = value; }
        public SubRuleNode RightNode { get => _rightNode ?? throw new Exception("Right Node Was Null"); set => _rightNode = value; }

        public SubRuleNode ParentNode { get => _parentNode ?? throw new Exception("Parent Was Null"); set => _parentNode = value; }

        public SubRule LeftNodeSubRule { get; set; }

        public SubRuleNode(SubRule subRule)
        {
            LeftNodeSubRule = subRule;
        }

        public static SubRuleNode GetNodes(string ruleString, int startSubRule, ref Dictionary<string, Rule> input)
        {
            var rule = input[ruleString];

            var subRule = rule.SubRules[startSubRule];

            if (startSubRule == rule.SubRules.Count - 1)
            {

            }






            return new SubRuleNode(subRule);
        }


    }

    public class AcceptableRange
    {
        public char ValueChar { get; set; }
        public int StartRange { get; set; }
        public int EndRange { get; set; }
    }

    public class Rule
    {
        public string Name { get; set; }
        public List<SubRule> SubRules { get; set; }
        public static Rule RuleFactory(string inputLine)
        {
            var firstBracket = inputLine.IndexOf('{');
            var name = inputLine[..firstBracket];
            inputLine = inputLine[(firstBracket + 1)..];
            inputLine = inputLine[..(inputLine.Length - 1)];
            var subRules = inputLine.Split(',').Select(x => SubRule.SubRuleFactory(x)).ToList();

            return new Rule(name, subRules);
        }
        public Rule(string name, List<SubRule> subRules)
        {
            Name = name;
            SubRules = subRules;
        }

        public string GetRuleOutcome(Part part)
        {
            foreach (var subRule in SubRules)
            {
                var outcome = subRule.GetOutcome(part);
                if (!string.IsNullOrEmpty(outcome))
                {
                    return outcome;
                }
            }
            throw new Exception();
        }
    }

    public class SubRule
    {
        public string Outcome { get; set; }

        public char Operator { get; set; }

        public int ToCompare { get; set; }
        public char ValueChar { get; set; }


        public static SubRule SubRuleFactory(string input)
        {
            if (!input.Contains(':'))
            {
                return new SubRule(input);
            }

            var leftRight = input.Split(':');
            var outcome = leftRight[1];
            var value = leftRight[0][0];
            var op = leftRight[0][1];
            var num = int.Parse(leftRight[0][2..]);

            return new SubRule(outcome, value, op, num);
        }

        public SubRule(string outcome, char value = '*', char @operator = '#', int toCompare = 0)
        {
            Outcome = outcome;
            Operator = @operator;
            ToCompare = toCompare;
            ValueChar = value;
        }

        public string GetOutcome(Part part)
        {
            switch (Operator)
            {
                case '>':
                    if (part.GetPartValue(ValueChar) > ToCompare)
                    {
                        return Outcome;
                    }
                    break;
                case '<':
                    if (part.GetPartValue(ValueChar) < ToCompare)
                    {
                        return Outcome;
                    }
                    break;
                case '#':
                    return Outcome;
                default:
                    throw new Exception();
            }
            return "";
        }


    }

    public class Part
    {
        public Dictionary<char, int> PartValues { get; set; } = new();

        public static Part PartFactory(string input)
        {
            input = input.Trim(new[] { '{', '}' });
            var parts = input.Split(',').Select(x => int.Parse(x.Split('=')[1])).ToList();

            return new Part(parts[0], parts[1], parts[2], parts[3]);
        }
        public Part(int x, int m, int a, int s)
        {
            PartValues.Add('x', x);
            PartValues.Add('m', m);
            PartValues.Add('a', a);
            PartValues.Add('s', s);
        }

        public int GetPartValue(char ch)
        {
            var exists = PartValues.TryGetValue(ch, out var value);
            if (!exists)
            {
                throw new Exception();
            }
            return value;
        }
    }
}
