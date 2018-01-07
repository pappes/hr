using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class Solution {
    //https://www.hackerrank.com/challenges/time-conversion/problem
    public static string TestHarness(string s)
    {
        // call actual logic 
        return timeConversion(s);
    }

    static string timeConversion(string s) {
        // Complete this function
        DateTime parsedDate;
        string inPattern = "hh:mm:sstt";
        string outPattern = "HH:mm:ss";
        
        DateTime.TryParseExact(s, inPattern, null, 
                                   DateTimeStyles.None, out parsedDate);
        return parsedDate.ToString(outPattern,CultureInfo.InvariantCulture);
    }

    static void Main(String[] args) {
        string s = Console.ReadLine();
        string result = timeConversion(s);
        Console.WriteLine(result);
    }
}
