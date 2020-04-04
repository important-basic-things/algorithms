namespace Leet.MyAtoi
{
    enum ConvertingState
    {
        Start,
        LeadingEmpty,
        SignReceived,
        DigitsReceived,
    }
}