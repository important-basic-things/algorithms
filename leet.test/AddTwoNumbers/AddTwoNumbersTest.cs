using Leet.AddTwoNumbers;
using Xunit;

namespace Leet.Test.AddTwoNumbers
{
    public class AddTwoNumbersTest
    {
        [Fact]
        public void should_be_able_to_add_two_single_digit_number()
        {
            var left = new ListNode(2);
            var right = new ListNode(3);
            ListNode result = Solution.AddTwoNumbers(left, right);

            Assert.Equal(5, result.val);
            Assert.Null(result.next);
        }

        [Fact]
        public void should_be_able_to_add_two_multiple_digits_number()
        {
            var left = new ListNode(2) {next = new ListNode(4) {next = new ListNode(3)}};
            var right = new ListNode(5) {next = new ListNode(6) {next = new ListNode(4)}};
            ListNode result = Solution.AddTwoNumbers(left, right);

            Assert.Equal(7, result.val);
            Assert.Equal(0, result.next.val);
            Assert.Equal(8, result.next.next.val);
            Assert.Null(result.next.next.next);
        }
        
        [Fact]
        public void should_be_able_to_add_two_number_with_different_digits_count()
        {
            var left = new ListNode(2) {next = new ListNode(4)};
            var right = new ListNode(5) {next = new ListNode(6) {next = new ListNode(4)}};
            ListNode result = Solution.AddTwoNumbers(left, right);

            Assert.Equal(7, result.val);
            Assert.Equal(0, result.next.val);
            Assert.Equal(5, result.next.next.val);
            Assert.Null(result.next.next.next);
        }

        [Fact]
        public void should_be_able_to_handle_forward()
        {
            var left = new ListNode(2) {next = new ListNode(4)};
            var right = new ListNode(5) {next = new ListNode(6)};
            ListNode result = Solution.AddTwoNumbers(left, right);
            
            Assert.Equal(7, result.val);
            Assert.Equal(0, result.next.val);
            Assert.Equal(1, result.next.next.val);
            Assert.Null(result.next.next.next);
        }
    }
}