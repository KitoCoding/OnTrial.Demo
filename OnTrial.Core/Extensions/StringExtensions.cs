using System.Text;

namespace OnTrial
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns true if the string is null or an empty string
        /// </summary>
        /// <param name="pContent">The string</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string pContent) => string.IsNullOrEmpty(pContent);

        /// <summary>
        /// Returns true if the string is null or an empty string or just whitespace
        /// </summary>
        /// <param name="pContent">The string</param>
        /// <returns></returns>
        public static bool IsNullOWhiteSpace(this string pContent) => string.IsNullOrWhiteSpace(pContent);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pContent"></param>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static string ToLeet(this string pContent, int degree = 100)
        {
            // Adjust degree between 0 - 100
            degree = degree >= 100 ? 100 : degree <= 0 ? 0 : degree;

            // No Leet Translator
            if (degree == 0)
                return pContent;
            
            // StringBuilder to store result.
            StringBuilder sb = new StringBuilder(pContent.Length);

            foreach (char c in pContent)
            {
                #region Degree > 0 and < 17
                if (degree < 17 && degree > 0)
                {
                    switch (c)
                    {
                        case 'e': sb.Append("3"); break;
                        case 'E': sb.Append("3"); break;
                        default: sb.Append(c); break;
                    }
                }
                #endregion
                #region Degree > 16 and < 33
                else if (degree < 33 && degree > 16)
                {
                    switch (c)
                    {
                        case 'a': sb.Append("4"); break;
                        case 'e': sb.Append("3"); break;
                        case 'i': sb.Append("1"); break;
                        case 'o': sb.Append("0"); break;
                        case 'A': sb.Append("4"); break;
                        case 'E': sb.Append("3"); break;
                        case 'I': sb.Append("1"); break;
                        case 'O': sb.Append("0"); break;
                        default: sb.Append(c); break;
                    }
                }
                #endregion
                #region Degree > 32 and < 49
                else if (degree < 49 && degree > 32)
                {
                    switch (c)
                    {
                        case 'a': sb.Append("4"); break;
                        case 'e': sb.Append("3"); break;
                        case 'i': sb.Append("1"); break;
                        case 'o': sb.Append("0"); break;
                        case 'A': sb.Append("4"); break;
                        case 'E': sb.Append("3"); break;
                        case 'I': sb.Append("1"); break;
                        case 'O': sb.Append("0"); break;
                        case 's': sb.Append("$"); break;
                        case 'S': sb.Append("$"); break;
                        case 'l': sb.Append("£"); break;
                        case 'L': sb.Append("£"); break;
                        case 'c': sb.Append("("); break;
                        case 'C': sb.Append("("); break;
                        case 'y': sb.Append("¥"); break;
                        case 'Y': sb.Append("¥"); break;
                        case 'u': sb.Append("µ"); break;
                        case 'U': sb.Append("µ"); break;
                        case 'd': sb.Append("Ð"); break;
                        case 'D': sb.Append("Ð"); break;
                        default: sb.Append(c); break;
                    }
                }
                #endregion
                #region Degree > 48 and < 65
                else if (degree < 65 && degree > 48)
                {
                    switch (c)
                    {
                        case 'a': sb.Append("4"); break;
                        case 'e': sb.Append("3"); break;
                        case 'i': sb.Append("1"); break;
                        case 'o': sb.Append("0"); break;
                        case 'A': sb.Append("4"); break;
                        case 'E': sb.Append("3"); break;
                        case 'I': sb.Append("1"); break;
                        case 'O': sb.Append("0"); break;
                        case 'k': sb.Append("|{"); break;
                        case 'K': sb.Append("|{"); break;
                        case 's': sb.Append("$"); break;
                        case 'S': sb.Append("$"); break;
                        case 'g': sb.Append("9"); break;
                        case 'G': sb.Append("9"); break;
                        case 'l': sb.Append("£"); break;
                        case 'L': sb.Append("£"); break;
                        case 'c': sb.Append("("); break;
                        case 'C': sb.Append("("); break;
                        case 't': sb.Append("7"); break;
                        case 'T': sb.Append("7"); break;
                        case 'z': sb.Append("2"); break;
                        case 'Z': sb.Append("2"); break;
                        case 'y': sb.Append("¥"); break;
                        case 'Y': sb.Append("¥"); break;
                        case 'u': sb.Append("µ"); break;
                        case 'U': sb.Append("µ"); break;
                        case 'f': sb.Append("ƒ"); break;
                        case 'F': sb.Append("ƒ"); break;
                        case 'd': sb.Append("Ð"); break;
                        case 'D': sb.Append("Ð"); break;
                        default: sb.Append(c); break;
                    }
                }
                #endregion
                #region Degree > 64 and < 81
                else if (degree < 81 && degree > 64)
                {
                    switch (c)
                    {
                        case 'a': sb.Append("4"); break;
                        case 'e': sb.Append("3"); break;
                        case 'i': sb.Append("1"); break;
                        case 'o': sb.Append("0"); break;
                        case 'A': sb.Append("4"); break;
                        case 'E': sb.Append("3"); break;
                        case 'I': sb.Append("1"); break;
                        case 'O': sb.Append("0"); break;
                        case 'k': sb.Append("|{"); break;
                        case 'K': sb.Append("|{"); break;
                        case 's': sb.Append("$"); break;
                        case 'S': sb.Append("$"); break;
                        case 'g': sb.Append("9"); break;
                        case 'G': sb.Append("9"); break;
                        case 'l': sb.Append("£"); break;
                        case 'L': sb.Append("£"); break;
                        case 'c': sb.Append("("); break;
                        case 'C': sb.Append("("); break;
                        case 't': sb.Append("7"); break;
                        case 'T': sb.Append("7"); break;
                        case 'z': sb.Append("2"); break;
                        case 'Z': sb.Append("2"); break;
                        case 'y': sb.Append("¥"); break;
                        case 'Y': sb.Append("¥"); break;
                        case 'u': sb.Append("µ"); break;
                        case 'U': sb.Append("µ"); break;
                        case 'f': sb.Append("ƒ"); break;
                        case 'F': sb.Append("ƒ"); break;
                        case 'd': sb.Append("Ð"); break;
                        case 'D': sb.Append("Ð"); break;
                        case 'n': sb.Append("|\\|"); break;
                        case 'N': sb.Append("|\\|"); break;
                        case 'w': sb.Append("\\/\\/"); break;
                        case 'W': sb.Append("\\/\\/"); break;
                        case 'h': sb.Append("|-|"); break;
                        case 'H': sb.Append("|-|"); break;
                        case 'v': sb.Append("\\/"); break;
                        case 'V': sb.Append("\\/"); break;
                        case 'm': sb.Append("|\\/|"); break;
                        case 'M': sb.Append("|\\/|"); break;
                        default: sb.Append(c); break;
                    }
                }
                #endregion
                #region Degree < 100 and > 80
                else if (degree > 80 && degree < 100)
                {
                    switch (c)
                    {
                        case 'a': sb.Append("4"); break;
                        case 'e': sb.Append("3"); break;
                        case 'i': sb.Append("1"); break;
                        case 'o': sb.Append("0"); break;
                        case 'A': sb.Append("4"); break;
                        case 'E': sb.Append("3"); break;
                        case 'I': sb.Append("1"); break;
                        case 'O': sb.Append("0"); break;
                        case 's': sb.Append("$"); break;
                        case 'S': sb.Append("$"); break;
                        case 'g': sb.Append("9"); break;
                        case 'G': sb.Append("9"); break;
                        case 'l': sb.Append("£"); break;
                        case 'L': sb.Append("£"); break;
                        case 'c': sb.Append("("); break;
                        case 'C': sb.Append("("); break;
                        case 't': sb.Append("7"); break;
                        case 'T': sb.Append("7"); break;
                        case 'z': sb.Append("2"); break;
                        case 'Z': sb.Append("2"); break;
                        case 'y': sb.Append("¥"); break;
                        case 'Y': sb.Append("¥"); break;
                        case 'u': sb.Append("µ"); break;
                        case 'U': sb.Append("µ"); break;
                        case 'f': sb.Append("ƒ"); break;
                        case 'F': sb.Append("ƒ"); break;
                        case 'd': sb.Append("Ð"); break;
                        case 'D': sb.Append("Ð"); break;
                        case 'n': sb.Append("|\\|"); break;
                        case 'N': sb.Append("|\\|"); break;
                        case 'w': sb.Append("\\/\\/"); break;
                        case 'W': sb.Append("\\/\\/"); break;
                        case 'h': sb.Append("|-|"); break;
                        case 'H': sb.Append("|-|"); break;
                        case 'v': sb.Append("\\/"); break;
                        case 'V': sb.Append("\\/"); break;
                        case 'k': sb.Append("|{"); break;
                        case 'K': sb.Append("|{"); break;
                        case 'r': sb.Append("®"); break;
                        case 'R': sb.Append("®"); break;
                        case 'm': sb.Append("|\\/|"); break;
                        case 'M': sb.Append("|\\/|"); break;
                        case 'b': sb.Append("ß"); break;
                        case 'B': sb.Append("ß"); break;
                        case 'q': sb.Append("Q"); break;
                        case 'Q': sb.Append("Q¸"); break;
                        case 'x': sb.Append(")("); break;
                        case 'X': sb.Append(")("); break;
                        default: sb.Append(c); break;
                    }
                }
                #endregion
                #region Degree 100
                else if (degree > 99)
                {
                    switch (c)
                    {
                        case 'a': sb.Append("4"); break;
                        case 'e': sb.Append("3"); break;
                        case 'i': sb.Append("1"); break;
                        case 'o': sb.Append("0"); break;
                        case 'A': sb.Append("4"); break;
                        case 'E': sb.Append("3"); break;
                        case 'I': sb.Append("1"); break;
                        case 'O': sb.Append("0"); break;
                        case 's': sb.Append("$"); break;
                        case 'S': sb.Append("$"); break;
                        case 'g': sb.Append("9"); break;
                        case 'G': sb.Append("9"); break;
                        case 'l': sb.Append("£"); break;
                        case 'L': sb.Append("£"); break;
                        case 'c': sb.Append("("); break;
                        case 'C': sb.Append("("); break;
                        case 't': sb.Append("7"); break;
                        case 'T': sb.Append("7"); break;
                        case 'z': sb.Append("2"); break;
                        case 'Z': sb.Append("2"); break;
                        case 'y': sb.Append("¥"); break;
                        case 'Y': sb.Append("¥"); break;
                        case 'u': sb.Append("µ"); break;
                        case 'U': sb.Append("µ"); break;
                        case 'f': sb.Append("ƒ"); break;
                        case 'F': sb.Append("ƒ"); break;
                        case 'd': sb.Append("Ð"); break;
                        case 'D': sb.Append("Ð"); break;
                        case 'n': sb.Append("|\\|"); break;
                        case 'N': sb.Append("|\\|"); break;
                        case 'w': sb.Append("\\/\\/"); break;
                        case 'W': sb.Append("\\/\\/"); break;
                        case 'h': sb.Append("|-|"); break;
                        case 'H': sb.Append("|-|"); break;
                        case 'v': sb.Append("\\/"); break;
                        case 'V': sb.Append("\\/"); break;
                        case 'k': sb.Append("|{"); break;
                        case 'K': sb.Append("|{"); break;
                        case 'r': sb.Append("®"); break;
                        case 'R': sb.Append("®"); break;
                        case 'm': sb.Append("|\\/|"); break;
                        case 'M': sb.Append("|\\/|"); break;
                        case 'b': sb.Append("ß"); break;
                        case 'B': sb.Append("ß"); break;
                        case 'j': sb.Append("_|"); break;
                        case 'J': sb.Append("_|"); break;
                        case 'P': sb.Append("|°"); break;
                        case 'q': sb.Append("¶"); break;
                        case 'Q': sb.Append("¶¸"); break;
                        case 'x': sb.Append(")("); break;
                        case 'X': sb.Append(")("); break;
                        default: sb.Append(c); break;
                    }
                }
                #endregion
            }
            return sb.ToString(); // Return result.
        }
    }
}