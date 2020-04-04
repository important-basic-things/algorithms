namespace Leet.MyAtoi
{
    static class CharacterExtension
    {
        public static bool IsSign(this char c)
        {
            return c == '+' || c == '-';
        }
        
        public static int CreateSignNumber(this char c)
        {
            return c == '+' ? 1 : -1;
        }
        
        public static bool IsDigit(this char c)
        {
            return c >= '0' && c <= '9';
        }

        public static int ToDigitNumber(this char c)
        {
            return c - '0';
        }
    }
}