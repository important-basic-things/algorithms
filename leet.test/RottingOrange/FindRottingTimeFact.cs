using Leet.RottingOrange;
using Xunit;

namespace Leet.Test.RottingOrange
{
    public class FindRottingTimeFact
    {
        [Fact]
        public void should_return_negative_1_if_no_rot_orange_found()
        {
            var noRotGrid = new[]
            {
                new[] {0, 1, 1},
                new[] {1, 1, 1}
            };
            var rottingTime = Solution.FindRottingTime(noRotGrid);
            Assert.Equal(-1, rottingTime);
        }

        [Fact]
        public void should_return_minimum_time_used_to_rot_case1()
        {
            var grid = new[]
            {
                new[] {2, 1, 1},
                new[] {1, 1, 0},
                new[] {0, 1, 1}
            };
            
            var rottingTime = Solution.FindRottingTime(grid);
            Assert.Equal(4, rottingTime);
        }

        [Fact]
        public void should_return_0_if_all_oranges_are_rot_ones()
        {
            var grid = new[]
            {
                new[] {0}
            };
            
            var rottingTime = Solution.FindRottingTime(grid);
            Assert.Equal(0, rottingTime);
        }
        
        [Fact]
        public void should_handle_one_dimension_grid()
        {
            var grid = new[]
            {
                new[] {1, 2}
            };
            
            var rottingTime = Solution.FindRottingTime(grid);
            Assert.Equal(1, rottingTime);
        }
    }
}