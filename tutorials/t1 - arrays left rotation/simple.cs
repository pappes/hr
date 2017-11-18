using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution {

    static void Main(String[] args) {
        string[] tokens_n = Console.ReadLine().Split(' ');
        int n = Convert.ToInt32(tokens_n[0]);
        int k = Convert.ToInt32(tokens_n[1]);
        string[] a_temp = Console.ReadLine().Split(' ');
        int[] a = Array.ConvertAll(a_temp,Int32.Parse);
        if (k > n) {//k is not supposed to be greater than n but just to be safe
            k = k % n;
        }
        
         for (int element = k; element < n; element++) {
            Console.Write("{0} ", a[element]);
         }
         for (int element = 0; element < k; element++) {
            Console.Write("{0} ", a[element]);
         }
    }
}
