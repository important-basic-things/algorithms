using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Leet.MyAtoi
{
    class MyAtoiStateMachine
    {
        ConvertingState state = ConvertingState.Start;
        int signNumber = 1;
        readonly string text;
        readonly List<short> digits = new List<short>(11);
        
        public MyAtoiStateMachine(string str)
        {
            text = str;
        }

        public int Convert()
        {
            if (string.IsNullOrEmpty(text)) return 0;
            int textLength = text.Length;
            for (int index = 0; index < textLength; ++index)
            {
                char currentCharacter = text[index];
                if (!UpdateStatus(currentCharacter))
                {
                    break;
                }
            }

            return CompactNumber();
        }

        int CompactNumber()
        {
            int beginPosition = digits.Count;
            for (int i = 0; i < digits.Count; ++i)
            {
                if (digits[i] != 0)
                {
                    beginPosition = i;
                    break;
                }
            }

            int totalDigits = digits.Count - beginPosition;

            if (totalDigits == 0) return 0;
            if (totalDigits > 10) return CreateMaxValueFromSignNumber();
            
            long currentBase = 1;
            long result = 0;
            for (int i = digits.Count - 1; i >= 0; --i)
            {
                short currentDigit = digits[i];
                result += currentBase * currentDigit;
                currentBase *= 10;
                if ((signNumber > 0 && result > int.MaxValue) || (signNumber < 0 && -result < int.MinValue))
                {
                    return CreateMaxValueFromSignNumber();
                }
            }
            
            return (int) result * signNumber;
        }

        int CreateMaxValueFromSignNumber() => signNumber > 0 ? int.MaxValue : int.MinValue;

        bool UpdateStatus(char currentCharacter)
        {
            switch (state)
            {
                case ConvertingState.Start:
                    return UpdateStartStatus(currentCharacter);
                case ConvertingState.LeadingEmpty:
                    return UpdateLeadingEmptyStatus(currentCharacter);
                case ConvertingState.SignReceived:
                case ConvertingState.DigitsReceived:
                    return UpdateDigitsReceivedStatus(currentCharacter);
                default:
                    return false;
            }
        }

        bool UpdateDigitsReceivedStatus(char currentCharacter)
        {
            if (currentCharacter.IsDigit())
            {
                return UpdateToDigitReceived(currentCharacter);
            }

            return false;
        }

        bool UpdateLeadingEmptyStatus(char currentCharacter)
        {
            if (char.IsWhiteSpace(currentCharacter))
            {
                return UpdateToLeadingEmpty();
            }
            
            if (currentCharacter.IsSign())
            {
                return UpdateToSignReceived(currentCharacter);
            }
            
            if (currentCharacter.IsDigit())
            {
                return UpdateToDigitReceived(currentCharacter);
            }

            return false;
        }

        [SuppressMessage("ReSharper", "ArgumentsStyleOther")]
        [SuppressMessage("ReSharper", "ArgumentsStyleNamedExpression")]
        bool UpdateStartStatus(char currentCharacter)
        {
            if (currentCharacter.IsSign())
            {
                return UpdateToSignReceived(currentCharacter);
            }

            if (char.IsWhiteSpace(currentCharacter))
            {
                return UpdateToLeadingEmpty();
            }

            if (currentCharacter.IsDigit())
            {
                return UpdateToDigitReceived(currentCharacter);
            }

            return false;
        }

        [SuppressMessage("ReSharper", "ArrangeRedundantParentheses")]
        bool UpdateToDigitReceived(char currentCharacter)
        {   
            UpdateInternalState(state: ConvertingState.DigitsReceived, digit: currentCharacter.ToDigitNumber());
            return true;
        }

        bool UpdateToSignReceived(char currentCharacter)
        {
            UpdateInternalState(
                signNumber: currentCharacter.CreateSignNumber(),
                state: ConvertingState.SignReceived);
            return true;
        }

        bool UpdateToLeadingEmpty()
        {
            UpdateInternalState(state: ConvertingState.LeadingEmpty);
            return true;
        }

        [SuppressMessage("ReSharper", "ParameterHidesMember")]
        void UpdateInternalState(
            int? signNumber = null,
            ConvertingState? state = null,
            int? digit = null)
        {
            if (signNumber.HasValue) this.signNumber = signNumber.Value;
            if (state.HasValue) this.state = state.Value;
            if (digit.HasValue)
            {
                digits.Add((short)digit);
            }
        }
    }
}