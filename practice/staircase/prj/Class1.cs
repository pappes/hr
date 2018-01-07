using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class Solution {
//https://www.hackerrank.com/challenges/staircase/problem
    public static string TestHarness(int n)
    {
        // call actual logic 
        return staircase(n);
    }
    static string staircase(int n) {
        // Complete this function
        string result = "";
        for (int i=0; i<n; i++) 
        {
            result = result + new string(' ', n-i-1) + new string('#', i+1) + "\r\n";
        }
        return result.TrimEnd();
    }

    static void Main(String[] args) {
        int n = Convert.ToInt32(Console.ReadLine());
        staircase(n);
    }
}
