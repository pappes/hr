using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class Solution {
//https://www.hackerrank.com/challenges/a-very-big-sum/problem
    public static long TestHarness(int a, long[] b)
    {
        // call actual logic 
        return aVeryBigSum(a, b);
    }

    static long aVeryBigSum(int n, long[] ar) {
        // Complete this function
        long result = 0;
        for (int element = 0; element < n; element++) {
            result += ar[element];
        }
        return result;
    }

    static void Main(String[] args) {
        int n = Convert.ToInt32(Console.ReadLine());
        string[] ar_temp = Console.ReadLine().Split(' ');
        long[] ar = Array.ConvertAll(ar_temp,Int64.Parse);
        long result = aVeryBigSum(n, ar);
        Console.WriteLine(result);
    }
}
