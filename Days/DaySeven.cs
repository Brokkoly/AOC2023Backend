using System.Security.Cryptography.X509Certificates;

namespace AOC2023Backend.Days
{
    public class DaySeven : Day
    {
        public override string PartOne(string input)
        {
            var allHands = ParseInput(input).Select(line => new PokerHand(line)).ToList();
            PokerHand.SortHands(ref allHands);
            var totalWinnings = 0;
            for (int i = allHands.Count; i > 0; i--)
            {
                var hand = allHands[i - 1];
                totalWinnings += i * hand.Bet;
            }
            return totalWinnings.ToString();
        }

        public override string PartTwo(string input)
        {
            var allHands = ParseInput(input).Select(line => new PokerHandPart2(line)).ToList();
            PokerHandPart2.SortHands(ref allHands);
            var totalWinnings = 0;
            for (int i = allHands.Count; i > 0; i--)
            {
                var hand = allHands[i - 1];
                totalWinnings += i * hand.Bet;
            }
            return totalWinnings.ToString();
        }
    }

    public class PokerHand
    {
        public enum Hands
        {
            HighCard = 0,
            OnePair = 1,
            TwoPair = 2,
            ThreeOfAKind = 3,
            FullHouse = 4,
            FourOfAKind = 5,
            FiveOfAKind = 6,
        }
        public string Hand { get; set; }
        public int Bet { get; set; }
        public Hands HandScore { get; set; }
        public PokerHand(string line)
        {
            var handAndBet = line.Split(' ');
            Hand = handAndBet[0];
            Bet = int.Parse(handAndBet[1]);
            HandScore = PokerHand.GetHandScore(Hand);
        }

        public static int CardToRank(char card)
        {
            switch (card)
            {
                case 'A': return 14;
                case 'K': return 13;
                case 'Q': return 12;
                case 'J': return 11;
                case 'T': return 10;
                default: return card - '0';
            }
        }

        public static void SortHands(ref List<PokerHand> hands)
        {
            hands.Sort((hand1, hand2) =>
            {
                if (hand1.HandScore == hand2.HandScore)
                {
                    return CompareHands(hand1, hand2);
                }
                else
                {
                    return hand1.HandScore.CompareTo(hand2.HandScore);
                }
            });
        }

        public static int CompareHands(PokerHand hand1, PokerHand hand2)
        {
            for (var i = 0; i < hand1.Hand.Length; i++)
            {
                var rank1 = CardToRank(hand1.Hand[i]);
                var rank2 = CardToRank(hand2.Hand[i]);
                if (rank1 != rank2)
                {
                    return rank1.CompareTo(rank2);
                }
            }
            return 0;
        }

        public static Hands GetHandScore(string hand)
        {
            Dictionary<char, int> count = new();
            foreach (char ch in hand)
            {
                if (count.ContainsKey(ch))
                {
                    count[ch]++;
                }
                else
                {
                    count[ch] = 1;
                }
            }
            var result = count.ToList().OrderBy(entry => entry.Value).Reverse().ToList();

            if (result[0].Value == 5)
            {
                return Hands.FiveOfAKind;
            }
            else if (result[0].Value == 4)
            {
                return Hands.FourOfAKind;
            }
            else if (result[0].Value == 3)
            {
                if (result[1].Value == 2)
                {
                    return Hands.FullHouse;
                }
                else
                {
                    return Hands.ThreeOfAKind;
                }
            }
            else if (result[0].Value == 2)
            {
                if (result[1].Value == 2)
                {
                    return Hands.TwoPair;
                }
                else
                {
                    return Hands.OnePair;
                }
            }
            else
            {
                return Hands.HighCard;
            }
        }
    }

    public class PokerHandPart2
    {
        public enum Hands
        {
            HighCard = 0,
            OnePair = 1,
            TwoPair = 2,
            ThreeOfAKind = 3,
            FullHouse = 4,
            FourOfAKind = 5,
            FiveOfAKind = 6,
        }
        public string Hand { get; set; }
        public int Bet { get; set; }
        public Hands HandScore { get; set; }
        public PokerHandPart2(string line)
        {
            var handAndBet = line.Split(' ');
            Hand = handAndBet[0];
            Bet = int.Parse(handAndBet[1]);
            HandScore = GetHandScore(Hand);
        }

        public static int CardToRank(char card)
        {
            switch (card)
            {
                case 'A': return 14;
                case 'K': return 13;
                case 'Q': return 12;
                case 'J': return -1;
                case 'T': return 10;
                default: return card - '0';
            }
        }

        public static void SortHands(ref List<PokerHandPart2> hands)
        {
            hands.Sort((hand1, hand2) =>
            {
                if (hand1.HandScore == hand2.HandScore)
                {
                    return CompareHands(hand1, hand2);
                }
                else
                {
                    return hand1.HandScore.CompareTo(hand2.HandScore);
                }
            });
        }

        public static int CompareHands(PokerHandPart2 hand1, PokerHandPart2 hand2)
        {
            for (var i = 0; i < hand1.Hand.Length; i++)
            {
                var rank1 = CardToRank(hand1.Hand[i]);
                var rank2 = CardToRank(hand2.Hand[i]);
                if (rank1 != rank2)
                {
                    return rank1.CompareTo(rank2);
                }
            }
            return 0;
        }

        public static Hands GetHandScore(string hand)
        {
            Dictionary<char, int> count = new();
            foreach (char ch in hand)
            {
                if (count.ContainsKey(ch))
                {
                    count[ch]++;
                }
                else
                {
                    count[ch] = 1;
                }
            }
            var result = count.ToList().Where(entry => entry.Key != 'J').OrderBy(entry => entry.Value).Reverse().ToList();
            var hasJoker = count.TryGetValue('J', out int jokerNum);
            if (!hasJoker)
            {
                jokerNum = 0;
            }
            if (jokerNum == 5)
            {
                return Hands.FiveOfAKind;
            }
            if (result[0].Value + jokerNum == 5)
            {
                return Hands.FiveOfAKind;
            }
            else if (result[0].Value + jokerNum == 4)
            {
                return Hands.FourOfAKind;
            }
            else if (result[0].Value + jokerNum == 3)
            {
                if (result[1].Value == 2)
                {
                    return Hands.FullHouse;
                }
                else
                {
                    return Hands.ThreeOfAKind;
                }
            }
            else if (result[0].Value + jokerNum == 2)
            {
                if (result[1].Value == 2)
                {
                    return Hands.TwoPair;
                }
                else
                {
                    return Hands.OnePair;
                }
            }
            else
            {
                return Hands.HighCard;
            }
        }
    }
}
