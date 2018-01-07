using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class Solution {

    //https://www.hackerrank.com/challenges/birthday-cake-candles/problem
    public static int TestHarness(int n, int[] ar)
    {
        // call actual logic 
        return birthdayCakeCandles(n, ar);
    }
    static int birthdayCakeCandles(int n, int[] ar) {
        int maxHeight = Int32.MinValue;
        int matchingCandles = 0;
        for (int i=0; i<n; i++)
        {
            if (ar[i] == maxHeight) {
                matchingCandles++;
            }
            if (ar[i] > maxHeight) {
                matchingCandles=1;
                maxHeight = ar[i];
            }
            
        }
        return matchingCandles;
    }

    static void Main(String[] args) {
        int n = Convert.ToInt32(Console.ReadLine());
        string[] ar_temp = Console.ReadLine().Split(' ');
        int[] ar = Array.ConvertAll(ar_temp,Int32.Parse);
        int result = birthdayCakeCandles(n, ar);
        Console.WriteLine(result);
    }
}
