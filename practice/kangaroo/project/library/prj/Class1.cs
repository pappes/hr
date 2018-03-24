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
        var k1 = new Kangaroo(x1,v1);
        var k2 = new Kangaroo(x2,v2);
        var first = k1>k2 ? k1 : k2;
        var last =  k2>k1 ? k1 : k2;
        if (last == first) return "YES";
        if (last.SlowerThan(first)) return "NO";
        while (first>last)
        {
            first.Move();
            last.Move();
        }
        if (k1 == k2) return "YES";
        return "NO";
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

public class Kangaroo 
{
    int  _postionIncrement;
    long _currentPosition;
    public Kangaroo(int start, int increment)
    {
        _currentPosition = start;
        _postionIncrement = increment;
    }
    public long Move() =>
        _currentPosition += _postionIncrement;
    public bool SlowerThan(Kangaroo obj) =>
        _postionIncrement < obj._postionIncrement;
    public long Position()=>
        _currentPosition;
    public int CompareTo(Kangaroo obj) =>
        _currentPosition.CompareTo(obj.Position());
    public static bool operator >(Kangaroo obj1, Kangaroo obj2) =>
        obj1.CompareTo(obj2)>0; 
    public static bool operator <(Kangaroo obj1, Kangaroo obj2) =>
        obj1.CompareTo(obj2)<0;
    public static bool operator !=(Kangaroo obj1, Kangaroo obj2) =>
        obj1.CompareTo(obj2)!=0;
    public static bool operator ==(Kangaroo obj1, Kangaroo obj2) =>
        obj1.CompareTo(obj2)==0;
    public override bool Equals(Object obj)
    {
        if (obj?.GetType() == GetType() )
            return this == ((Kangaroo)obj);
        return false;
    }
    public override int GetHashCode()=>
        this._currentPosition.GetHashCode();
}