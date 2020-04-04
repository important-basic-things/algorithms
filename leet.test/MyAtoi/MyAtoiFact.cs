using Leet.MyAtoi;
using Xunit;

namespace Leet.Test.MyAtoi
{
    public class MyAtoiFact
    {
        [Fact]
        public void should_convert_whitespace()
        {
            Assert.Equal(0, Solution.MyAtoi(null));
            Assert.Equal(0, Solution.MyAtoi(""));
            Assert.Equal(0, Solution.MyAtoi(" "));
        }

        [Theory]
        [InlineData("42", 42)]
        [InlineData("-42", -42)]
        [InlineData("     42  ", 42)]
        [InlineData("     -42  ", -42)]
        [InlineData("4193 with words", 4193)]
        public void should_convert_digits(string text, int expected)
        {
            Assert.Equal(expected, Solution.MyAtoi(text));
        }
        
        [Theory]
        [InlineData("-912283746283746287364827", int.MinValue)]
        [InlineData("2341342309429834792374", int.MaxValue)]
        public void should_convert_maximum(string text, int expected)
        {
            Assert.Equal(expected, Solution.MyAtoi(text));
        }
        
        [Theory]
        [InlineData("word 1234")]
        [InlineData("----")]
        [InlineData("--001")]
        public void should_convert_invalid(string text)
        {
            Assert.Equal(0, Solution.MyAtoi(text));
        }

        [Fact]
        public void should_convert_digits_with_leading_zeros()
        {
            Assert.Equal(12345678, Solution.MyAtoi("0000000000012345678"));
        }
        
        [Fact]
        public void should_convert_really_long_sequences()
        {
            Assert.Equal(int.MaxValue, Solution.MyAtoi("10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000522545459"));
        }
    }
}