using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInt1
{
    public class ArrayStrings
    {
        public static bool StringCharsUnique(string s)
        {
            HashSet<char> charsSeen = new HashSet<char>();
            foreach (char c in s)
            {
                if (charsSeen.Contains(c))
                {
                    return false;
                }
                charsSeen.Add(c);
            }
            return true;
        }

        public static bool StringCharsUniqueNoAdd(string s)
        {
            // Note: This is an n^2 algorithm... maybe improved by sorting?
            for (int i = 0; i < s.Length; i++)
            {
                for (int j = i+1; j < s.Length; j++)
                {
                    if (s[j] == s[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static void reverseCStr(byte[] chars)
        {
            // In C, this would just be a call to strlen
            // It's a little wonky in this context.
            int strlen = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '\0')
                {
                    break;
                }
                strlen++;
            }

            for (int i = 0; i < strlen / 2; i++)
            {
                byte temp = chars[i];
                chars[i] = chars[strlen-i-1];
                chars[strlen - i - 1] = temp;
            }
        }

        public static String removeDuplicates(String s)
        {
            // LEFTOFF: Do it later
            return "";
        }

        public static bool CheckAnagram(String s1, String s2)
        {
            Dictionary<char, int> CharCounts1 = new Dictionary<char, int>();
            Dictionary<char, int> CharCounts2 = new Dictionary<char, int>();

            // Quick out-case
            if (s1.Length != s2.Length)
            {
                return false;
            }

            foreach (char c in s1)
            {
                if (CharCounts1.ContainsKey(c))
                {
                    CharCounts1[c] += 1;
                }
                else
                {
                    CharCounts1[c] = 1;
                }
            }

            foreach (char c in s2)
            {
                if (CharCounts2.ContainsKey(c))
                {
                    CharCounts2[c] += 1;
                }
                else
                {
                    CharCounts2[c] = 1;
                }
            }

            // I have to implement an equality checker for the dictionaries
            if (CharCounts1.Count != CharCounts2.Count)
            {
                return false;
            }

            foreach (char c in CharCounts1.Keys)
            {
                if (!CharCounts2.ContainsKey(c))
                {
                    return false;
                }
                if (CharCounts1[c] != CharCounts2[c])
                {
                    return false;
                }
            }

            return true;
        }

        public static String SpaceReplace(String s)
        {
            int newStrLen = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                {
                    newStrLen += 3;
                }
                else
                {
                    newStrLen += 1;
                }
            }

            char[] newStr = new char[newStrLen];

            for (int i=0, j=0; i < s.Length;)
            {
                if (s[i] == ' ')
                {
                    newStr[j] = '%';
                    newStr[j+1] = '2';
                    newStr[j+2] = '0';
                    i += 1;
                    j += 3;
                }
                else
                {
                    newStr[j] = s[i];
                    i += 1;
                    j += 1;
                }
            }

            return new string(newStr);
        }
    }
}
