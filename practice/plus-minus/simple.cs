using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution {
//https://www.hackerrank.com/challenges/plus-minus/problem
    
    static double[] ClassifyContents (int n, int[] arr) {
        double negative = 0d;
        double posative = 0d;
        double zero = 0d;
        double arraySize = (double) n; 
        for (int element=0; element < n; element ++) {
            if (arr[element] > 0) {
                posative++;
            }
            else if (arr[element] < 0) {
                negative ++;
            }
            else {
                zero++;
            }
        }
        return new double[] {posative/arraySize, negative/arraySize, zero/arraySize};
        
    }

    static void Main(String[] args) {
        int n = Convert.ToInt32(Console.ReadLine());
        string[] arr_temp = Console.ReadLine().Split(' ');
        int[] arr = Array.ConvertAll(arr_temp,Int32.Parse);
        double[] ratios = ClassifyContents(n, arr);
        Console.WriteLine("{0:F6}", ratios[0]);
        Console.WriteLine("{0:F6}", ratios[1]);
        Console.WriteLine("{0:F6}", ratios[2]);
    }
}
