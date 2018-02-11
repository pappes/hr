using System;
using System.Collections.Generic;
using System.IO;

//allow unit testing project to have visibility into private memebers
[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("lib.Xunit")]

namespace Solution.Services {
    using LectureObserver = IObserver<LectureTheatre>;
    using LectureObservable = IObservable<LectureTheatre>;

    public class Solution {
    //https://www.hackerrank.com/challenges/angry-professor/problem
        public static void TestHarness(StreamReader input, StreamWriter output) =>
            // Call actual logic.
            TimeLine(input, output);

        static void TimeLine(StreamReader source, StreamWriter destination) 
        {
            var lessonObserver = new Professor();
        
            int t = Convert.ToInt32(source.ReadLine());
            for(int a0 = 0; a0 < t; a0++){
                string[] tokens_n = source.ReadLine().Split(' ');
                int n = Convert.ToInt32(tokens_n[0]);
                int k = Convert.ToInt32(tokens_n[1]);
                string[] a_temp = source.ReadLine().Split(' ');
                int[] a = Array.ConvertAll(a_temp,Int32.Parse);
                
                var lessonProvider = new ScheduledClass(n, k);
                lessonObserver.Subscribe(lessonProvider);
                foreach (var time in a){
                    lessonProvider.RecordArrival(time);      
                }
                destination.WriteLine(lessonObserver.StateOfMind == Professor.MentalState.Angry ? "YES" : "NO");            
            }
        }
        static void Main(String[] args) 
        {
            StreamReader stdin = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);
            StreamWriter stdout = new StreamWriter(Console.OpenStandardOutput());
            TimeLine(stdin, stdout);
            stdout.Flush();
        }
    }
    
    public class Professor : LectureObserver 
    {    
        public enum MentalState { Calm, Pensive, Angry}
        //state of mind as public property (not getter/etter) becasue we want to be able to pass by ref
        public MentalState StateOfMind = MentalState.Pensive;
        internal IDisposable _Subscription;

        public void Subscribe(ScheduledClass plannedClass) =>
            ProfessorUtils.Subscribe(ref _Subscription, plannedClass, this);

        #region IObserver Members
            public virtual void OnNext(LectureTheatre plannedClass) 
            {
                if (ProfessorUtils.ConfirmAttendance(ref StateOfMind, plannedClass)) Unsubscribe();
            }
            public virtual void OnCompleted() {} // No implementation.
            public virtual void OnError(Exception e){} // No implementation.
        #endregion IObserver Members    

        internal void Unsubscribe() =>
            ProfessorUtils.Unsubscribe(_Subscription);
    }

    public class LectureTheatre {    
        public int OnTimeStudents { get; set; } = 0;
        public int LateStudents { get; set; } = 0;   
        public int ClassSize { get; set; }
        public int CancellationThreshold { get; set; }

        public void InitialiseStatistics (int expectedClassSize, int classCancellationThreshold) 
        {
            ClassSize = expectedClassSize;
            CancellationThreshold = classCancellationThreshold;
        }
        public void UpdateStatistics (int arrivalTime) 
        {
            if (arrivalTime<=0)
                OnTimeStudents++;
            else
                LateStudents++;   
        }
    }    

    public class ScheduledClass : LectureObservable {   
        internal List<LectureObserver> _Staff = new List<LectureObserver>(); 
        internal LectureTheatre _Lesson = new LectureTheatre();

        public ScheduledClass (int expectedClassSize, int classCancellationThreshold) =>        
            _Lesson.InitialiseStatistics(expectedClassSize, classCancellationThreshold);
        
        public virtual void RecordArrival (int arrivalTime) 
        {
            _Lesson.UpdateStatistics(arrivalTime);        
            ClassUtils.NotifyStaff(_Staff,_Lesson);
        }
        #region IObservable Members
            public virtual IDisposable Subscribe(LectureObserver lecturer)
            {
                ClassUtils.RecordSubscription(_Staff, lecturer);            
                // Provide observer with existing data.
                lecturer.OnNext(_Lesson);
                return ClassUtils.CreateUnsubscriber(_Staff, lecturer);
            }
        #endregion IObservable Members
    }    

    public class ProfessorUtils 
    {    
        internal static bool ConfirmAttendance(ref Professor.MentalState stateOfMind, LectureTheatre plannedClass) 
        {
            if (plannedClass.OnTimeStudents >= plannedClass.CancellationThreshold) {
                stateOfMind = Professor.MentalState.Calm;              
                // Stop watching and get on with the job (even if it causess problems for list enumeration).
                return true; 
            } else {
                if (plannedClass.LateStudents >0 && 
                    plannedClass.LateStudents >= plannedClass.ClassSize-plannedClass.CancellationThreshold) {
                    stateOfMind = Professor.MentalState.Angry ;                  
                    // Ragequit in protest( even if it causess problems for list enumeration).
                    return true; 
                } 
            } 
            // Attendence is incomplete so need more data.
            return false; 
        }
        internal static void Unsubscribe(IDisposable subscription) =>
            subscription.Dispose();
        
        internal static void Subscribe(ref IDisposable subscription, ScheduledClass plannedClass, Professor subscriber) =>
            subscription = plannedClass.Subscribe(subscriber);        
    }

    internal static class ClassUtils {   
        internal static void NotifyStaff (List<LectureObserver> staff, LectureTheatre lesson) 
        {
            // Copy the list before enumerating in case one observer ragequits when they see what thet don't like.
            List<LectureObserver> staffClone = new List<LectureObserver>(staff);
            foreach (var lecturer in staffClone)
                lecturer.OnNext(lesson);
        }
        internal static void RecordSubscription (List<LectureObserver> staff, LectureObserver lecturer) 
        {
            // Check whether lecturer is already registered. If not, add it.
            if (! staff.Contains(lecturer)) staff.Add(lecturer);
        }
        internal static void Unsubscribe (List<LectureObserver> staff, LectureObserver lecturer) 
        {    
            if (staff.Contains(lecturer)) staff.Remove(lecturer);
        }
        internal static IDisposable CreateUnsubscriber (List<LectureObserver> staff, LectureObserver lecturer) =>
            new UnsubscriberLambda(() => Unsubscribe(staff, lecturer) );
    }

    public class UnsubscriberLambda :IDisposable
    {
        private Action _DisposeCallback;

        public UnsubscriberLambda(Action callback) => 
            _DisposeCallback = callback;
        
        #region IDisposable Members
            public void Dispose() =>
                _DisposeCallback();

        #endregion
    }
}