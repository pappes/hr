using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class Solution {
//https://www.hackerrank.com/challenges/compare-the-triplets/problem
    public static int[] TestHarness(int a0, int a1, int a2, int b0, int b1, int b2)
    {
        // call actual logic 
        return solve(a0, a1, a2, b0, b1, b2);;
    }

    static int[] solve(int a0, int a1, int a2, int b0, int b1, int b2){
        int[] results = {0,0};
        // Complete this function
        results[0] += compare(a0, b0);
        results[0] += compare(a1, b1);
        results[0] += compare(a2, b2);
        results[1] += compare(b0, a0);
        results[1] += compare(b1, a1);
        results[1] += compare(b2, a2);
        return results;
    }
    static int compare(int a, int b){
        if (a>b) 
            return 1;
        else
            return 0;
    }

    static void Main(String[] args) {
        string[] tokens_a0 = Console.ReadLine().Split(' ');
        int a0 = Convert.ToInt32(tokens_a0[0]);
        int a1 = Convert.ToInt32(tokens_a0[1]);
        int a2 = Convert.ToInt32(tokens_a0[2]);
        string[] tokens_b0 = Console.ReadLine().Split(' ');
        int b0 = Convert.ToInt32(tokens_b0[0]);
        int b1 = Convert.ToInt32(tokens_b0[1]);
        int b2 = Convert.ToInt32(tokens_b0[2]);
        int[] result = solve(a0, a1, a2, b0, b1, b2);
        Console.WriteLine(String.Join(" ", result));
        

    }
}
