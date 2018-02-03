﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Solution.Services {
    using LectureObserver = System.IObserver<LectureTheatre>;
    using LectureObservable = System.IObservable<LectureTheatre>;

    public class Solution {
    //https://www.hackerrank.com/challenges/angry-professor/problem
        public static void TestHarness(StreamReader input, StreamWriter output) 
        {
            // Call actual logic.
            TimeLine(input, output);
        }

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
                destination.WriteLine(lessonObserver.Angry);            
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

    class LectureTheatre {    
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

    class ScheduledClass : LectureObservable {   
        private List<LectureObserver> _Staff = new List<LectureObserver>(); 
        private LectureTheatre _Lesson = new LectureTheatre();

        public ScheduledClass (int expectedClassSize, int classCancellationThreshold) 
        {
            _Lesson.InitialiseStatistics(expectedClassSize, classCancellationThreshold);
        }
        public void RecordArrival (int arrivalTime) 
        {
            _Lesson.UpdateStatistics(arrivalTime);        
            NotifyStaff(_Staff,_Lesson);
        }
        #region IObservable Members
            public IDisposable Subscribe(LectureObserver lecturer)
            {
                RecordSubscription(_Staff, lecturer);            
                // Provide observer with existing data.
                lecturer.OnNext(_Lesson);
                return CreateUnsubscriber(_Staff, lecturer);
            }
        #endregion IObservable Members

        private static void NotifyStaff (List<LectureObserver> staff, LectureTheatre lesson) 
        {
            // Copy the list before enumerating in case one observer ragequits when they see what thet don't like.
            List<LectureObserver> staffClone = new List<LectureObserver>(staff);
            foreach (var lecturer in staffClone)
                lecturer.OnNext(lesson);
        }
        private static void RecordSubscription (List<LectureObserver> staff, LectureObserver lecturer) 
        {
            // Check whether lecturer is already registered. If not, add it.
            if (! staff.Contains(lecturer)) {
                staff.Add(lecturer);
            }
        }
        private static void Unsubscribe (List<LectureObserver> staff, LectureObserver lecturer) {
            if (staff.Contains(lecturer)) 
                staff.Remove(lecturer);
        }
        private static IDisposable CreateUnsubscriber (List<LectureObserver> staff, LectureObserver lecturer) {
            return new UnsubscriberLambda(() => Unsubscribe(staff, lecturer) );
        }
    }

    class Professor : LectureObserver 
    {    
        public string Angry { get; set; } = "?";
        private IDisposable _Subscription;

        public void Subscribe(ScheduledClass plannedClass) 
        {
            SubscribeT(ref _Subscription, plannedClass, this);
        }
        #region IObserver Members
            public virtual void OnNext(LectureTheatre plannedClass) 
            {
                if (ConfirmAttendance(plannedClass)) Unsubscribe();
            }
            public virtual void OnCompleted() {} // No implementation.
            public virtual void OnError(Exception e){} // No implementation.
        #endregion IObserver Members    

        private bool ConfirmAttendance(LectureTheatre plannedClass) 
        {
            if (plannedClass.OnTimeStudents >= plannedClass.CancellationThreshold) {
                this.Angry = "NO";             
                // Stop watching and get on with the job (even if it causess problems for list enumeration).
                return true; 
            } else {
                if (plannedClass.LateStudents >0 && 
                    plannedClass.LateStudents >= plannedClass.ClassSize-plannedClass.CancellationThreshold) {
                    this.Angry = "YES" ;                  
                    // Ragequit in protest( even if it causess problems for list enumeration).
                    return true; 
                } 
            } 
            // Attendence is incomplete so need more data.
            return false; 
        }
        private void UnsubscribeT(IDisposable subscription) 
        {
            subscription.Dispose();
        }
        private void SubscribeT(ref IDisposable subscription, ScheduledClass plannedClass, Professor subscriber) 
        {
            subscription = plannedClass.Subscribe(subscriber);
        }
        private void Unsubscribe() 
        {
            UnsubscribeT(_Subscription);
        }
    }

    public class UnsubscriberLambda :IDisposable
    {
        private Action _DsposeCallback;

        public UnsubscriberLambda(Action callback)
        {
            _DsposeCallback = callback;
        }
        #region IDisposable Members
            public void Dispose()
            {
                _DsposeCallback();
            }
        #endregion
    }
}