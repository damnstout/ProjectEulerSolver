using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEulerSolvers
{
    enum PorkerRank
    {
        HighCard = 0,
        OnePair = 1,
        TwoPair = 2,
        ThreeOfAKind = 3,
        Straight = 4,
        Flush = 5,
        FullHouse = 6,
        FourOfAKind = 7,
        StraightFlush = 8,
        RoyalFlush = 9
    }

    class Prob054PorkerSuite : IComparable<Prob054PorkerSuite>
    {
        public PorkerRankResolver Resolver { get { return _resolver; } }

        private string[] _porkers;
        private PorkerRankResolver _resolver;

        public Prob054PorkerSuite(string[] porkers)
        {
            _porkers = porkers;
            _resolver = new PorkerRankResolver(_porkers);
        }

        #region IComparable<Prob054PorkerSuite>
        public int CompareTo(Prob054PorkerSuite other)
        {
            if (null == other)
            {
                return 1;
            }
            PorkerRankResolver r1 = this.Resolver, r2 = other.Resolver;
            if (r1.Rank > r2.Rank)
            {
                return 1;
            }
            if (r1.Rank < r2.Rank)
            {
                return -1;
            }
            return 0 == r1.FirstVal - r2.FirstVal ? (0 == r1.SecondVal - r2.SecondVal ? (r1.ThirdVal - r2.ThirdVal) : r1.SecondVal - r2.SecondVal) : r1.FirstVal - r2.FirstVal;
        }
        #endregion

        public override string ToString()
        {
            return string.Join(",", _porkers);
        }
    }

    class PorkerRankResolver 
    {
        private List<int> cardList = new List<int>();
        private bool isFlush = true;
        private int pairCount = 0;
        private int tripleCount = 0;
        private int tetradteCount = 0;
        private bool isStraight = false;

        private int firstVal = 0;
        private int secondVal = 0;
        private int thirdVal = 0;
        private PorkerRank _rank;

        public PorkerRank Rank { get { return _rank; } }
        public int FirstVal { get { return firstVal; } }
        public int SecondVal { get { return secondVal; } }
        public int ThirdVal { get { return thirdVal; } }

        public PorkerRankResolver(string[] porkers)
        {
            if (5 != porkers.Length)
            {
                return;
            }
            char currSuit = '0';
            foreach (string porker in porkers)
            {
                if (currSuit != Suit(porker) && currSuit != '0')
                {
                    isFlush = false;
                }
                currSuit = Suit(porker);
                cardList.Add(Val(porker));
            }
            cardList.Sort();
            cardList.Add(20);
            ProcessPorkers();
            ResolveRank();
        }

        private void ResolveRank()
        {
            if (IsRoyalFlush())
            {
                _rank = PorkerRank.RoyalFlush;
            }
            else if (IsStraightFlush())
            {
                _rank = PorkerRank.StraightFlush;
            }
            else if (IsFourOfAKind())
            {
                _rank = PorkerRank.FourOfAKind;
            }
            else if (IsFullHouse())
            {
                _rank = PorkerRank.FullHouse;
            }
            else if (IsFlush())
            {
                _rank = PorkerRank.Flush;
            }
            else if (IsStraight())
            {
                _rank = PorkerRank.Straight;
            }
            else if (IsThreeOfAKind())
            {
                _rank = PorkerRank.ThreeOfAKind;
            }
            else if (IsTwoPairs())
            {
                _rank = PorkerRank.TwoPair;
            }
            else if (IsOnePair())
            {
                _rank = PorkerRank.OnePair;
            }
            else
            {
                _rank = PorkerRank.HighCard;
            }
        }

        private bool IsRoyalFlush()
        {
            return isFlush && isStraight && firstVal == 14;
        }

        private bool IsStraightFlush()
        {
            return isFlush && isStraight;
        }

        private bool IsFourOfAKind()
        {
            return 1 == tetradteCount;
        }

        private bool IsFullHouse()
        {
            return 1 == tripleCount && 1 == pairCount;
        }

        private bool IsFlush()
        {
            return isFlush;
        }

        private bool IsStraight()
        {
            return isStraight;
        }

        private bool IsThreeOfAKind()
        {
            return 1 == tripleCount;
        }

        private bool IsTwoPairs()
        {
            return 2 == pairCount;
        }

        private bool IsOnePair()
        {
            return 1 == pairCount;
        }

        private void ProcessPorkers()
        {
            int pos = 0;
            for (int i = 1; i < 6; i++ )
            {
                if (cardList[pos] == cardList[i]) continue;
                if (4 == (i - pos))
                {
                    tetradteCount = 1;
                    firstVal = cardList[i - 1];
                    secondVal = cardList[i == 5 ? 0 : 4];
                }
                if (3 == (i - pos))
                {
                    tripleCount = 1;
                    firstVal = cardList[i - 1];
                    secondVal = cardList[i == 5 ? 1 : 4];
                }
                if (2 == (i - pos))
                {
                    if (1 == tripleCount)
                    {
                        secondVal = cardList[5];
                    }
                    else if (1 == pairCount)
                    {
                        secondVal = firstVal;
                        firstVal = cardList[i - 1];
                        if (4 == i)
                        {
                            thirdVal = cardList[4];
                        }
                        else
                        {
                            thirdVal = (cardList[0] == secondVal ? cardList[2] : cardList[0]);
                        }
                    }
                    else if (0 == pairCount)
                    {
                        firstVal = cardList[i - 1];
                        secondVal = cardList[i == 5 ? 2 : 4];
                    }
                    pairCount++;
                }
                pos = i;
            }
            if (0 == (tetradteCount + tripleCount + pairCount))
            {
                isStraight = (4 == (cardList[4] - cardList[0]));
                firstVal = cardList[4];
            }
        }

        private char Suit(string porker)
        {
            return porker[1];
        }

        private int Val(string porker)
        {
            char vchar = porker[0];
            switch (vchar)
            {
                case 'A':
                    return 14;
                case 'K':
                    return 13;
                case 'Q':
                    return 12;
                case 'J':
                    return 11;
                case 'T':
                    return 10;
                default:
                    return vchar - '0';
            }
        }

        public override string ToString()
        {
            return string.Format("R:{0},1st:{1},2nd:{2},3rd:{3}", Rank, FirstVal, SecondVal, ThirdVal);
        }
    }
}
