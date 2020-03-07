using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Leet.TwoSum
{
    public static class Solution
    {
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
        struct Item : IComparable<Item>
        {
            public readonly int Value;
            public readonly int OriginalIndex;

            public Item(int value, int originalIndex)
            {
                Value = value;
                OriginalIndex = originalIndex;
            }

            public int CompareTo(Item other)
            {
                return Value.CompareTo(other.Value);
            }
        }

        public static int[] TwoSum(int[] nums, int target)
        {
            Item[] sorted = CreateSortedArray(nums);
            
            for (int i = 0; i < sorted.Length - 1; i++)
            {
                Item current = sorted[i];
                int expected = target - current.Value;
                int indexFound = expected >= current.Value ? 
                    Array.BinarySearch(sorted, i + 1, sorted.Length - i - 1, new Item(expected, 0)) :
                    Array.BinarySearch(sorted, 0, i, new Item(expected, 0));

                if (indexFound < 0) { continue; }
                
                var currentOriginalIndex = current.OriginalIndex;
                var foundOriginalIndex = sorted[indexFound].OriginalIndex;
                return currentOriginalIndex > foundOriginalIndex ? new[] {foundOriginalIndex, currentOriginalIndex} : new[] {currentOriginalIndex, foundOriginalIndex};
            }

            return Array.Empty<int>();
        }

        private static Item[] CreateSortedArray(int[] nums)
        {
            return nums.Select((value, index) => new Item(value, index))
                .OrderBy(item => item.Value)
                .ToArray();
        }
    }
}