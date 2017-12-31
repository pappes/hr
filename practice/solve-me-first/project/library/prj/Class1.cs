using System;
using System.Collections.Generic;
using System.IO;
/*namespace prj
{*/
    public class Solution
    {
        //https://www.hackerrank.com/challenges/solve-me-first/problem

        public static int TestHarness(int a, int b)
        {
            // call actual logic 
            return solveMeFirst(a, b);
        }
        static int solveMeFirst(int a, int b)
        {
            // Hint: Type return a+b; below  
            return a + b;
        }
        static void Main(String[] args)
        {
            int val1 = Convert.ToInt32(Console.ReadLine());
            int val2 = Convert.ToInt32(Console.ReadLine());
            int sum = solveMeFirst(val1, val2);
            Console.WriteLine(sum);
        }
    }
/*}*/