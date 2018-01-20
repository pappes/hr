using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class Solution {
//https://www.hackerrank.com/challenges/angry-professor/problem
    public static void TestHarness(StreamReader input, StreamWriter output)
    {
        // call actual logic 
        timeLine(input, output);
    }
/*    static string[] angryProfessor(int tests, int[][] rules, int[][] arrivals) {
        // Complete this function
        return new string[] {};
    }*/

    static void timeLine(StreamReader source, StreamWriter destination) {
        int tests = Convert.ToInt32(source.ReadLine());/* 
        int[][] arrivals = new int[tests][];
        int[][] rules = new int[tests][];
        for(int a0 = 0; a0 < tests; a0++){
            string[] tokens_n = source.ReadLine().Split(' ');
            rules[a0] = Array.ConvertAll(tokens_n,Int32.Parse);
            tokens_n = source.ReadLine().Split(' ');
            arrivals[a0] = Array.ConvertAll(tokens_n,Int32.Parse);
        }
        string[] result = angryProfessor(tests, rules, arrivals);
        for(int a0 = 0; a0 < tests; a0++){
            Console.destination(result[a0]);        
        }*/
    }
    static void Main(String[] args) {
        timeLine(new StreamReader(Console.OpenStandardInput(), Console.InputEncoding), 
                 new StreamWriter(Console.OpenStandardOutput()));
    }
}

/*class Sitting:ObservableImpl {    
    public string cancelled { get;  } = '?';
    int size;
    int threshold;
    int onTime = 0;
    int late = 0;

    public Sitting (int classSize, int classThreshold) {
        size = classSize;
        threshold = classThreshold;
    }

    public void RecordArrival (int time) {
        time<=0 ? onTime++ : late++;
        if (onTime > size-threshold) {
            cancelled = 'NO'
        } else {
        if (late > size-threshold) {
            cancelled = 'YES'            
        }
    }
}

class Professor:IObserver {    
    public string angry { get;  } = '?';
    Sitting class;

    public Professor (int classSize, int classThreshold) {
        size = classSize;
        threshold = classThreshold;
    }

    public void Arrive (Sitting s) {
        //subscribe for updates when students arrive
        s.Register(this);
    }
    public void Notify (Sitting s) {
        class = s;
        if (onTime > threshold) {
            //cancelled = 'NO'
        } else {
        if (late > size-threshold) {
            //cancelled = 'YES'            
        }
    }
}
*/