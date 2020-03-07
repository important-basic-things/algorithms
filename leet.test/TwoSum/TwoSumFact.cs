using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Leet.TwoSum;
using Xunit;

namespace Leet.Test.TwoSum
{
    public class TwoSumFact
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static IEnumerable<object[]> TwoSumCases
        {
            get
            {
                yield return new object[] {new[] {2, 7, 11, 15}, 9, new[] {0, 1}};
                yield return new object[] {new[] {3, 2, 4}, 6, new[] {1, 2}};
                yield return new object[] {new[] {3, 2, 3}, 6, new[] {0, 2}};
            }
        }
        
        [Theory]
        [MemberData(nameof(TwoSumCases))]
        public void should_find_two_sum(int[] collection, int target, int[] expected)
        {
            Assert.Equal(expected, Solution.TwoSum(collection, target));
        }
    }
}