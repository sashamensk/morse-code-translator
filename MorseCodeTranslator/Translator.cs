using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

#pragma warning disable S2368

namespace MorseCodeTranslator
{
    public static class Translator
    {
        public static int GetAlphaIndexOfChar(char c)
        {
            return char.ToUpper(c, CultureInfo.CurrentCulture) - 64;
        }

        public static string GetMorseCode(int index)
        {
            string morseCode = string.Empty;
            for (int y = 1; y < MorseCodes.CodeTable[index].Length; y++)
            {
                morseCode = string.Concat(morseCode, MorseCodes.CodeTable[index][y]);
            }

            return morseCode;
        }

        public static string TranslateToMorse(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            StringBuilder result = new StringBuilder(message);
            result.Replace(",", string.Empty);
            result.Replace(".", string.Empty);

            for (int i = 0; i < result.Length; i++)
            {
                if (char.IsLetter(result[i]))
                {
                    string currentChar = result[i].ToString();
                    result.Replace(currentChar, $"{GetMorseCode(GetAlphaIndexOfChar(result[i]) - 1)} ");
                }
            }

            result.Replace("  ", " ");
            return result.ToString().Trim();
        }

        public static string TranslateToText(string morseMessage)
        {
            if (string.IsNullOrWhiteSpace(morseMessage))
            {
                throw new ArgumentNullException(nameof(morseMessage));
            }

            StringBuilder result = new StringBuilder();

            string[] words = morseMessage.Split(' ', StringSplitOptions.None);
            for (int i = 0; i < words.Length; i++)
            {
                for (int y = 0; y < MorseCodes.CodeTable.Length; y++)
                {
                    if (words[i] == GetMorseCode(y))
                    {
                        result.Append(char.ToUpper(MorseCodes.CodeTable[y][0], CultureInfo.CurrentCulture));
                    }
                }
            }

            result.ToString();
            return result.ToString();
        }

        public static void WriteMorse(this char[][] codeTable, string message, StringBuilder morseMessageBuilder, char dot = '.', char dash = '-', char separator = ' ')
        {
            if (codeTable is null)
            {
                throw new ArgumentNullException(nameof(codeTable));
            }

            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (morseMessageBuilder is null)
            {
                throw new ArgumentNullException(nameof(morseMessageBuilder));
            }

            string result = string.Empty;

            for (int i = 0; i < message.Length; i++)
            {
                if (char.IsLetter(message[i]))
                {
                    result = string.Concat(result, GetMorseCode(GetAlphaIndexOfChar(message[i]) - 1), " ");
                }
            }

            result = result.Trim();
            morseMessageBuilder.Append(result);

            if (dot != '.')
            {
                morseMessageBuilder.Replace('.', dot);
            }

            if (dash != '-')
            {
                morseMessageBuilder.Replace('-', dash);
            }

            if (separator != ' ')
            {
                morseMessageBuilder.Replace(' ', separator);
            }
        }

        public static void WriteText(char[][] codeTable, string morseMessage, StringBuilder messageBuilder, char dot = '.', char dash = '-', char separator = ' ')
        {
            if (codeTable is null)
            {
                throw new ArgumentNullException(nameof(codeTable));
            }

            if (morseMessage is null)
            {
                throw new ArgumentNullException(nameof(morseMessage));
            }

            if (messageBuilder is null)
            {
                throw new ArgumentNullException(nameof(messageBuilder));
            }

            if (separator != ' ')
            {
                morseMessage = morseMessage.Replace(separator, ' ');
                separator = ' ';
            }

            if (dot != '.')
            {
                morseMessage = morseMessage.Replace(dot, '.');
                dot = '.';
            }

            if (dash != '-')
            {
                morseMessage = morseMessage.Replace(dash, '-');
                dash = '-';
            }

            if (morseMessage.Contains(separator, StringComparison.CurrentCultureIgnoreCase))
            {
                string[] words = morseMessage.Split(separator);
                for (int i = 0; i < words.Length; i++)
                {
                    for (int y = 0; y < codeTable.Length; y++)
                    {
                        string temp = string.Empty;

                        for (int x = 1; x < codeTable[y].Length; x++)
                        {
                            temp = string.Concat(temp, codeTable[y][x]);
                        }

                        if (words[i] == temp)
                        {
                            messageBuilder.Append(codeTable[y][0]);
                        }
                    }
                }
            }
            else
            {
                string[] words = new string[1] { morseMessage };
                for (int i = 0; i < words.Length; i++)
                {
                    for (int y = 0; y < codeTable.Length; y++)
                    {
                        string temp = string.Empty;

                        for (int x = 1; x < codeTable[y].Length; x++)
                        {
                            temp = string.Concat(temp, codeTable[y][x]);
                        }

                        if (words[i] == temp)
                        {
                            messageBuilder.Append(codeTable[y][0]);
                        }
                    }
                }
            }
        }
    }
}
