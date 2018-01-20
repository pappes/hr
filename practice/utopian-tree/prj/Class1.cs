using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class Solution {
    //https://www.hackerrank.com/challenges/utopian-tree/problem
    public static int[] TestHarness(int cases, int[] cycles)
    {
        // call actual logic 
        return utopianTree(cases, cycles);
    }

    static int[] utopianTree(int cases, int[] cycles) {
        // Complete this function
        int[] growth = new int[cases];
        for(int c = 0; c < cases; c++){
            growth[c] = grow(cycles[c]);
        }
        return growth;
    }
    static int grow(int cycles) {
        // start at 1, double for even cycle, add one for odd cycle
        int result=1;
        for(int c = 0; c < cycles; c++){
            if (c%2 == 0) {
                result*=2;
            } else {
                result+=1;
            }
        }
        return result;
    }

    static void Main(String[] args) {
        int t = Convert.ToInt32(Console.ReadLine());
        int[] n = new int[t];
        for(int a0 = 0; a0 < t; a0++){
            n[a0] = Convert.ToInt32(Console.ReadLine());
        }
        int[] result = utopianTree(t, n);
        for(int a0 = 0; a0 < t; a0++){
            Console.WriteLine(result[a0]);
        }
    }
}
