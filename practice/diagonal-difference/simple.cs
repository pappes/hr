using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using Math;
class Solution {
//https://www.hackerrank.com/challenges/diagonal-difference/problem
    static int CompareAxis (int size, int[][] matrix) {
        long forwardslash=0;
        long backslash=0;
        long result = 0;
        for (int element=0; element<size; element ++) {
            backslash += matrix[element][element];
            forwardslash += matrix[element][size - element - 1];
        }
        result = backslash - forwardslash;
        return (int) Math.Abs(result);       
    }
    
    static void Main(String[] args) {
        int n = Convert.ToInt32(Console.ReadLine());
        int[][] a = new int[n][];
        for(int a_i = 0; a_i < n; a_i++){
           string[] a_temp = Console.ReadLine().Split(' ');
           a[a_i] = Array.ConvertAll(a_temp,Int32.Parse);
        }
        Console.WriteLine(CompareAxis(n,a));
    }
}
