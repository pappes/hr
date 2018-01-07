using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class Solution {

    //https://www.hackerrank.com/challenges/mini-max-sum/problem

    public static long[] TestHarness(int[] arr)
    {
        // call actual logic 
        return miniMaxSum(arr);
    }
    static long[] miniMaxSum(int[] arr) {
        // Complete this function
        long maxVal = Int32.MinValue;
        long minVal = Int32.MaxValue;
        long totalValue = 0;
        long currValue;
        for (int i=0; i<arr.Count(); i++)
        {
            currValue = Convert.ToInt64(arr[i]);
            totalValue += currValue;
            maxVal = currValue>maxVal ? currValue : maxVal;
            minVal = currValue<minVal ? currValue : minVal;
        }
        return new long [] {totalValue-maxVal, totalValue-minVal};
    }

    static void Main(String[] args) {
        string[] arr_temp = Console.ReadLine().Split(' ');
        int[] arr = Array.ConvertAll(arr_temp,Int32.Parse);
        long[] results = miniMaxSum(arr);
        Console.WriteLine("{0} {1}",results[0],results[1]);
    }
}
