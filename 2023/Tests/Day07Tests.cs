using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using static Day07;

namespace Tests
{
    [UseReporter(typeof(VisualStudioReporter))]
    public class Day07Tests
    {
        [Theory]
        [InlineData("T4236", "72934")]
        [InlineData("Q8886", "6669Q")]
        [InlineData("JJJJJ", "TQQQQ")]
        [InlineData("2222T", "AAAJJ")]
        [InlineData("22233", "AAAKQ")]
        [InlineData("99922", "2K22K")]
        public void CanCheckStrength(string Bigger, string Smaller)
        {
            var hand  = new Day07.Hand(Bigger);
            var hand2  = new Day07.Hand(Smaller);
            hand.Should().BeGreaterThan(hand2);
        }

        [Theory]
        [InlineData("2423J", 4)]
        [InlineData("3J3J3", 7)]
        [InlineData("8JJJJ", 7)]
        public void CanComputeJokerStrength(string input, int expected)
        {
            var hand = new Day07.Hand(input, true);
            hand.Strength.Should().Be(expected);
        }
        [Theory]
        [InlineData("2A5AJ", 2)]
        public void CanComputeStrength(string input, int expected)
        {
            var hand = new Day07.Hand(input, false);
            hand.Strength.Should().Be(expected);
        }

        [Fact]
        public void Regression()
        {
            var hands = GetHands().Select(hand => new Hand(hand));
            Approvals.VerifyAll(hands, "");

        }

        [Fact]
        public void JokerRegression()
        {
            var hands = GetHands().Select(hand => new Hand(hand, true));
            Approvals.VerifyAll(hands, "");

        }

        string suit = "23456789TJQKA";
        IEnumerable<string> GetHands()
        {
            var suit = "23456789TJQKA";
            var allStrings = new List<string>();
            for (int i = 0; i < suit.Length; i++)
            {
                for (int j = 0; j < suit.Length; j++)
                {
                    for (int k = 0; k < suit.Length; k++)
                    {
                        for (int l = 0; l < suit.Length; l++)
                        {
                            for (int m = 0; m < suit.Length; m++)
                            {
                                var str = $"{suit[i]}{suit[j]}{suit[k]}{suit[l]}{suit[m]}";
                                allStrings.Add(str);
                            }
                        }
                    }
                }
            }
            return allStrings;
        }
    }
}
