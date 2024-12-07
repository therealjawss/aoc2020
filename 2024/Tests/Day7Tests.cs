using FluentAssertions;
using static Day07;

namespace Tests
{
    public class Day7Tests
    {
        [Fact]
        public void Test1()
        {
            var equation = new Equation(190, new Stack<ulong>([10, 19]));

            equation.IsValid().Should().BeTrue();
        }
        
        [Fact]
        public void Test2()
        {  
            var list = new List<ulong> { 81, 40, 27 };
            list.Reverse();
            var equation = new Equation(3267, new Stack<ulong>(list));

            equation.IsValid().Should().BeTrue();
        }

        [Fact]
        public void Test3()
        {
            var list = new List<ulong> { 6,8,6,15};
            list.Reverse();
            var equation = new Equation(7290, new Stack<ulong>(list));

            equation.IsValid(withConcat:true).Should().BeTrue();
        }
    }
}