using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{
    //https://www.hackerrank.com/challenges/ctci-array-left-rotation/problem
    static void Main(String[] args)
    {
        string[] tokens_n = Console.ReadLine().Split(' ');
        int n = Convert.ToInt32(tokens_n[0]);
        int k = Convert.ToInt32(tokens_n[1]);
        string[] a_temp = Console.ReadLine().Split(' ');
        int[] a = Array.ConvertAll(a_temp, Int32.Parse);

        //TODO:pull out custom code into a seperate proc
        //TODO:sort in the array (copy out one elelement to make a space and shuffle remainingf before putting original element back in)
        //TODO:output the array in linear fashion
        //TODO: coovert console tests to nunint tests
        if (k > n)
        {//k is not supposed to be greater than n but just to be safe
            k = k % n;
        }

        for (int element = k; element < n; element++)
        {
            Console.Write("{0} ", a[element]);
        }
        for (int element = 0; element < k; element++)
        {
            Console.Write("{0} ", a[element]);
        }
    }
}
