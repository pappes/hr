using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

//allow unit testing project to have visibility into private memebers
[assembly: InternalsVisibleToAttribute("lib.Xunit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

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
        public MentalState StateOfMind;
        internal IDisposable _Subscription;
        private readonly IProfessorUtils _ProfessorUtils;

        public Professor (MentalState mentalState = MentalState.Pensive,
                          IProfessorUtils professorUtils = null) 
        {
            _ProfessorUtils = professorUtils ?? new ProfessorUtils();
            StateOfMind = mentalState;
        }

        public void Subscribe(ScheduledClass plannedClass) =>
            _ProfessorUtils.Subscribe(ref _Subscription, plannedClass, this);

        #region IObserver Members
            public virtual void OnNext(LectureTheatre plannedClass) 
            {
                if (_ProfessorUtils.ConfirmAttendance(ref StateOfMind, plannedClass)) Unsubscribe();
            }
            public virtual void OnCompleted() {} // No implementation.
            public virtual void OnError(Exception e){} // No implementation.
        #endregion IObserver Members    

        internal void Unsubscribe() =>
            _ProfessorUtils.Unsubscribe(_Subscription);
    }

    public class LectureTheatre {    
        public virtual int OnTimeStudents { get; protected internal  set; } = 0;
        public virtual int LateStudents { get; protected internal  set; } = 0;   
        public virtual int ClassSize { get; protected internal set; }
        public virtual int CancellationThreshold { get; protected internal set; }

        public virtual void InitialiseStatistics (int expectedClassSize, int classCancellationThreshold) 
        {
            ClassSize = expectedClassSize;
            CancellationThreshold = classCancellationThreshold;
        }
        public virtual void UpdateStatistics (int arrivalTime) 
        {
            if (arrivalTime<=0)
                OnTimeStudents++;
            else
                LateStudents++;   
        }
    }    

    public class ScheduledClass : LectureObservable {   
        private LectureTheatre _Lesson;
        private IClassUtils _ClassUtils;
        private List<LectureObserver> _Staff; 
        
        public ScheduledClass (int expectedClassSize, 
                               int classCancellationThreshold, 
                               IClassUtils classUtils = null,
                               LectureTheatre lectureTheatre = null,
                               List<LectureObserver> observers = null)
        {
            _ClassUtils = classUtils ??  new ClassUtils();
            _Staff = observers ??  new List<LectureObserver>();
            _Lesson = lectureTheatre ??  new LectureTheatre();
            _Lesson.InitialiseStatistics(expectedClassSize, classCancellationThreshold);
        }
        
        public virtual void RecordArrival (int arrivalTime) 
        {
            _Lesson.UpdateStatistics(arrivalTime);        
            _ClassUtils.NotifyStaff(_Staff,_Lesson);
        }
        #region IObservable Members
            public virtual IDisposable Subscribe(LectureObserver lecturer)
            {
                _ClassUtils.RecordSubscription(_Staff, lecturer);            
                // Provide observer with existing data.
                lecturer.OnNext(_Lesson);
                return _ClassUtils.CreateUnsubscriber(_Staff, lecturer);
            }
        #endregion IObservable Members
    }    

    public interface IProfessorUtils 
    {    
        bool ConfirmAttendance(ref Professor.MentalState stateOfMind, LectureTheatre plannedClass);
        void Unsubscribe(IDisposable subscription);
        void Subscribe(ref IDisposable subscription, ScheduledClass plannedClass, Professor subscriber);
    }

    public interface IClassUtils 
    {   
        void NotifyStaff (List<LectureObserver> staff, LectureTheatre lesson) ;
        void RecordSubscription (List<LectureObserver> staff, LectureObserver lecturer) ;
        void Unsubscribe (List<LectureObserver> staff, LectureObserver lecturer) ;
        IDisposable CreateUnsubscriber (List<LectureObserver> staff, LectureObserver lecturer);
    }

    internal class ProfessorUtils : IProfessorUtils
    {    
        public bool ConfirmAttendance(ref Professor.MentalState stateOfMind, LectureTheatre plannedClass) 
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
        public virtual void Unsubscribe(IDisposable subscription) =>
            subscription.Dispose();
        
        public virtual void Subscribe(ref IDisposable subscription, ScheduledClass plannedClass, Professor subscriber) =>
            subscription = plannedClass.Subscribe(subscriber);        
    }

    internal class ClassUtils : IClassUtils 
    {   
        public void NotifyStaff (List<LectureObserver> staff, LectureTheatre lesson) 
        {
            // Copy the list before enumerating in case one observer ragequits when they see what thet don't like.
            List<LectureObserver> staffClone = new List<LectureObserver>(staff);
            foreach (var lecturer in staffClone)
                lecturer.OnNext(lesson);
        }
        public void RecordSubscription (List<LectureObserver> staff, LectureObserver lecturer) 
        {
            // Check whether lecturer is already registered. If not, add it.
            if (! staff.Contains(lecturer)) staff.Add(lecturer);
        }
        public void Unsubscribe (List<LectureObserver> staff, LectureObserver lecturer) 
        {    
            if (staff.Contains(lecturer)) staff.Remove(lecturer);
        }
        public IDisposable CreateUnsubscriber (List<LectureObserver> staff, LectureObserver lecturer) =>
            new UnsubscriberLambda(() => Unsubscribe(staff, lecturer) );
    }

    public class UnsubscriberLambda : IDisposable
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