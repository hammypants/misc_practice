using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace StringOnlyDigits
{
    /*
     * Was asked to validate that a string contains only digits.
     * Wrote my own validate function that parses a string and checks
     * each character to see if it is within the range 0 <= c <= 9.
     * We then discussed Regex / Integer.ParseInt solutions...
     * where it was claimed they were faster.
     * 
     * Well, not always... ;)
     * 
     * ~ Program input not sanitized.
     * 
     * ~ Missing an ASCII-checking solution that was discussed..
     * ~ Maybe the IsDigit for characters? Based on the implementation...
     */
    class Program
    {
        private static Regex regex = new Regex("^[0-9]+$", RegexOptions.Compiled);

        static void Main(string[] args)
        {
            SpeedTest();
            ValidationTest("1295325");
            ValidationTest("385d39");
            Console.ReadKey();
        }

        // Fastest.
        static bool OnlyDigits(string s)
        {
            foreach (char c in s)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        // Slower than OnlyDigits.
        static bool OnlyDigits2(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        // Order of speed with this test:
        // OnlyDigits > Int.TryParse > OnlyDigits2 > Regex
        //
        // So.. faster to write a simple function? Yes.
        //
        // Addtionally, using Regex options for Compiled
        // offers performances increases over other options.
        static void SpeedTest()
        {
            Stopwatch stopwatch = new Stopwatch();
            string test = int.MaxValue.ToString();
            int value;

            stopwatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                int.TryParse(test, out value);
            }
            stopwatch.Stop();
            Console.WriteLine("TryParse validation took: " + stopwatch.ElapsedTicks + " ticks.");

            stopwatch.Reset();

            stopwatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                regex.IsMatch(test);
            }
            stopwatch.Stop();
            Console.WriteLine("Regex validation took: " + stopwatch.ElapsedTicks + " ticks.");

            stopwatch.Reset();

            stopwatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                OnlyDigits(test);
            }
            stopwatch.Stop();
            Console.WriteLine("OnlyDigits validation took: " + stopwatch.ElapsedTicks + " ticks.");

            stopwatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                OnlyDigits2(test);
            }
            stopwatch.Stop();
            Console.WriteLine("OnlyDigits2 validation took: " + stopwatch.ElapsedTicks + " ticks.");
        }

        /// <summary>
        /// Sort of redundant, since all of them should be correct.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        static void ValidationTest(string s)
        {
            bool onlyDigits1Test = OnlyDigits(s);
            Console.WriteLine("OnlyDigits returned " + onlyDigits1Test.ToString());

            bool onlyDigits2Test = OnlyDigits2(s);
            Console.WriteLine("OnlyDigits2 returned " + onlyDigits1Test.ToString());

            int value;
            bool intTryParseTest = int.TryParse(s, out value);
            Console.WriteLine("TryParse returned " + onlyDigits1Test.ToString());

            bool regexTest = regex.IsMatch(s);
            Console.WriteLine("Regex returned " + onlyDigits1Test.ToString());
        }
    }
}
