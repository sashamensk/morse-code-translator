using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

#pragma warning disable S2368

namespace MorseCodeTranslator
{
    public static class Translator
    {
        public static string RemoveLastWhiteSpace(string str)
        {
            if (str[str.Length - 1] == ' ')
            {
                str = str.Remove(str.Length - 1);
            }

            return str;
        }

        public static int GetAlphabetNum(char letter)
        {
            char upper = char.ToUpper(letter);
            if (upper < 'A' || upper > 'Z')
            {
                throw new ArgumentOutOfRangeException("value", "This method only accepts standard Latin characters.");
            }

            return (int)upper - (int)'A';
        }

        public static string TranslateToMorse(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            StringBuilder text = new StringBuilder(message);
            text.Replace(',', ' ');
            text.Replace('.', ' ');
            for (int i = 0; i < text.Length; i++)
            {
                string currentChar = text.ToString(i, 1);
                if (char.IsLetter(char.ToUpper(text[i], CultureInfo.CurrentCulture)))
                {
                    int alphaIndex = GetAlphabetNum(text[i]);
                    string temp = string.Empty;
                    for (int y = 1; y < MorseCodes.CodeTable[alphaIndex].Length; y++)
                    {
                        temp = string.Concat(temp, MorseCodes.CodeTable[alphaIndex][y]);
                    }

                    if (string.Compare(temp, currentChar, StringComparison.CurrentCulture) != 0)
                    {
                        text.Replace(currentChar, string.Concat(temp, " "));
                        i = i + temp.Length;
                    }
                }
            }

            string result = text.ToString(0, text.Length);
            result = result.Trim(' ');
            result = result.Replace("   ", " ", StringComparison.CurrentCulture);
            result = result.Replace("  ", " ", StringComparison.CurrentCulture);
            return result;
        }

        public static string MorseWordToText(string morse)
        {
            char[][] codes = (char[][])MorseCodes.CodeTable.Clone();

            for (int i = 0; i < codes.Length; i++)
            {
                string currentCode = string.Empty;
                for (int y = 1; y < codes[i].Length; y++)
                {
                    currentCode += codes[i][y];
                }

                if (string.Compare(morse, currentCode, true, CultureInfo.CurrentCulture) == 0)
                {
                    string result = codes[i][0].ToString();
                    return result;
                }
            }

            return "0";
        }

        public static string TranslateToText(string morseMessage)
        {
            if (string.IsNullOrWhiteSpace(morseMessage))
            {
                throw new ArgumentNullException(nameof(morseMessage));
            }

            List<string> list = MakeList(morseMessage);
            for (int i = 0; i < list.Count; i++)
            {
                string alpha = MorseWordToText(list[i]);
                list[i] = alpha;
            }

            string[] result = new string[list.Count];
            list.CopyTo(result);
            string str = string.Empty;
            foreach (var item in result)
            {
                str += item;
            }

            return str;
        }

        public static void WriteMorse(this char[][] codeTable, string message, StringBuilder morseMessageBuilder, char dot = '.', char dash = '-', char separator = ' ')
        {
            codeTable = MorseCodes.CodeTable;
            for (int i = 0; i < length; i++)
            {

            }

        }

        public static void WriteText(char[][] codeTable, string morseMessage, StringBuilder messageBuilder, char dot = '.', char dash = '-', char separator = ' ')
        {
            // TODO #4. Implement the method.
            throw new NotImplementedException();
        }

        public static List<string> MakeList(string str)
        {
            return new List<string>(str.Split(' '));
        }
    }
}
