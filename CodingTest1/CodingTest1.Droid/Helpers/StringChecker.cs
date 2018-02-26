using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CodingTest1.Droid.Helpers
{
    public static class StringChecker
    {
        // Strings to compare characters against.
        private static string Alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string AlphaLower;
        private static string Numbers = "0123456789";

        // Constructor
        static StringChecker()
        {
            AlphaLower = Alpha.ToLower();
        }

        // Check for at least one alpha character.
        public static bool ContainsAlphaCharacter(string str)
        {
            foreach (char c in str)
            {
                if (Alpha.Contains(c) || AlphaLower.Contains(c)) return true;
            }

            return false;
        }

        // Check for at least one numeric character.
        public static bool ContainsNumericCharacter(string str)
        {
            foreach (char c in str)
            {
                if (Numbers.Contains(c)) return true;
            }

            return false;
        }

        // Check for any non alpha-numeric characters.
        public static bool ContainsInvalidCharacter(string str)
        {
            foreach (char c in str)
            {
                if (!Alpha.Contains(c) && !AlphaLower.Contains(c) && !Numbers.Contains(c)) return true;
            }
            return false;
        }

        // Check whether a password string is between 5 and 12 characters (inclusive).
        public static bool IsPasswordLengthValid(string pass)
        {
            return (pass.Length >= 5 && pass.Length <= 12);
        }

        // Check whether the string contains a repeating sequence.
        public static bool ContainsRepeatingSequence(string str)
        {
            if (string.IsNullOrEmpty(str)) return false;

            for (int i=0; i<str.Length; i++)
            {
                // Every iteration, cut down the string from i to the end.
                string toCheck = str.Substring(i);

                // Set N equal to half the length of the substring. At most, we have to compare half the string to half the string. If the string length is odd, the last character will not be checked against, but it will be checked in the next iteration.
                int N = toCheck.Length / 2;

                // Check strings of all lengths from 1 to N against the subsequent string of length 1 to N.
                for (int j=1; j<=N; j++)
                {
                    // Check from beginning to j-1, compare against j to j+j.
                    if (toCheck.Substring(0, j) == toCheck.Substring(j, j)) return true;
                }
            }

            return false;
        }
    }
}
