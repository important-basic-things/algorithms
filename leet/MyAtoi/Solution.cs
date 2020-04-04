namespace Leet.MyAtoi
{
    public static class Solution
    {
        public static int MyAtoi(string str)
        {
            return new MyAtoiStateMachine(str).Convert();
        }
    }
}