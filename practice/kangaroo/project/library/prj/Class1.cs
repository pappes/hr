using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class Solution {
//https://www.hackerrank.com/challenges/kangaroo/problem
    public static void TestHarness(StreamReader input, StreamWriter output) {
        // call actual logic 
        SolveProblem(input, output);
    }
    static string kangaroo(int x1, int v1, int x2, int v2) {
        // Complete this function
        return "";
    }
    static void SolveProblem(StreamReader source, StreamWriter destination) {
        string[] tokens_x1 = source.ReadLine().Split(' ');
        int x1 = Convert.ToInt32(tokens_x1[0]);
        int v1 = Convert.ToInt32(tokens_x1[1]);
        int x2 = Convert.ToInt32(tokens_x1[2]);
        int v2 = Convert.ToInt32(tokens_x1[3]);
        string result = kangaroo(x1, v1, x2, v2);
        destination.WriteLine(result);
    }

    static void Main(String[] args) {
        StreamReader stdin = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);
        StreamWriter stdout = new StreamWriter(Console.OpenStandardOutput());
        SolveProblem(stdin, stdout);
        stdout.Flush();
    }
}
