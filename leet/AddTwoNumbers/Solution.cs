namespace Leet.AddTwoNumbers
{
    public static class Solution
    {
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode head = null;
            ListNode current = null;
            ListNode opLeft = l1;
            ListNode opRight = l2;
            int forward = 0;

            while (opLeft != null || opRight != null || forward != 0)
            {
                int leftValue = opLeft?.val ?? 0;
                int rightValue = opRight?.val ?? 0;
                int sum = leftValue + rightValue + forward;
                int result = sum % 10;
                forward = leftValue + rightValue + forward >= 10 ? 1 : 0;
                var newNode = new ListNode(result);
                if (head == null)
                {
                    head = newNode;
                    current = head;
                }
                else
                {
                    current.next = newNode;
                    current = newNode;
                }

                opLeft = opLeft?.next;
                opRight = opRight?.next;
            }

            return head;
        }
    }
}