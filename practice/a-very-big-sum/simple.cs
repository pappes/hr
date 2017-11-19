using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution {
//https://www.hackerrank.com/challenges/a-very-big-sum/problem

    static long aVeryBigSum(int n, long[] ar) {
        // Complete this function
        int result = 0;
        for (int element = 0; element < n; element++) {
            result += ar[element];
        }
        return 0;
    }

    static void Main(String[] args) {
        int n = Convert.ToInt32(Console.ReadLine());
        string[] ar_temp = Console.ReadLine().Split(' ');
        long[] ar = Array.ConvertAll(ar_temp,Int64.Parse);
        long result = aVeryBigSum(n, ar);
        Console.WriteLine(result);
    }
}
