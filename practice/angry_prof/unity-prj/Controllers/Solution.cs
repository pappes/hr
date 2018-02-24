using System;
using System.IO;

namespace Solution.Services {
    public class Solution : ISolution {
    //https://www.hackerrank.com/challenges/angry-professor/problem
        private readonly IProfessor _lessonObserver;        
        //private readonly IScheduledClass _lessonProvider;
        private readonly IScheduledClassFactory _scheduledClassFactory;

        public Solution (IProfessor professor,
                         IScheduledClassFactory scheduledClassFactory) 
        {
            _lessonObserver = professor;
            _scheduledClassFactory = scheduledClassFactory;

        }
        public void TestHarness(StreamReader input, StreamWriter output) =>
            // Call actual logic.
            TimeLine(input, output);

        public void TimeLine(StreamReader source, StreamWriter destination) 
        {
            //var lessonObserver = new Professor();
        
            int t = Convert.ToInt32(source.ReadLine());
            for(int a0 = 0; a0 < t; a0++){
                string[] tokens_n = source.ReadLine().Split(' ');
                int n = Convert.ToInt32(tokens_n[0]);
                int k = Convert.ToInt32(tokens_n[1]);
                string[] a_temp = source.ReadLine().Split(' ');
                int[] a = Array.ConvertAll(a_temp,Int32.Parse);
                
                //TODO pull mental state out of proffessor
                 var lessonProvider = _scheduledClassFactory.Create(n, k);
                _lessonObserver.Subscribe(lessonProvider);
                foreach (var time in a){
                    lessonProvider.RecordArrival(time);      
                }
                destination.WriteLine(_lessonObserver.GetMentalState() == Mind.MentalState.Angry ? "YES" : "NO");  
            }
        }
    }
}