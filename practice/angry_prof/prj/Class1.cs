using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


public class Solution {
//https://www.hackerrank.com/challenges/angry-professor/problem
    public static void TestHarness(StreamReader input, StreamWriter output) {
        // call actual logic 
        timeLine(input, output);
    }

    static void timeLine(StreamReader source, StreamWriter destination) {
        Professor lessonObserver = new Professor();
      
        int t = Convert.ToInt32(source.ReadLine());
        for(int a0 = 0; a0 < t; a0++){
            string[] tokens_n = source.ReadLine().Split(' ');
            int n = Convert.ToInt32(tokens_n[0]);
            int k = Convert.ToInt32(tokens_n[1]);
            string[] a_temp = source.ReadLine().Split(' ');
            int[] a = Array.ConvertAll(a_temp,Int32.Parse);
            
            ScheduledClass lessonProvider = new ScheduledClass(n, k);
            lessonObserver.Subscribe(lessonProvider);
            foreach (int time in a){
                lessonProvider.RecordArrival(time);      
            }
            destination.WriteLine(lessonObserver.angry);            
        }
    }
    static void Main(String[] args) {
        StreamReader stdin = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);
        StreamWriter stdout = new StreamWriter(Console.OpenStandardOutput());
        timeLine(stdin, stdout);
        stdout.Flush();
    }
}

class LectureTheatreDTO {    
    public int onTimeStudents { get; set; } = 0;
    public int lateStudents { get; set; } = 0;   
    public int classSize { get; set; }
    public int cancellationThreshold { get; set; }
}

class ScheduledClass : IObservable<LectureTheatreDTO> {   
    private List<IObserver<LectureTheatreDTO>> _staff; 
    private LectureTheatreDTO _lesson ;

    public ScheduledClass (int expectedClassSize, int classCancellationThreshold) {
        _staff = new List<IObserver<LectureTheatreDTO>>();
        _lesson = new LectureTheatreDTO();
        InitialiseStatistics(_lesson, expectedClassSize, classCancellationThreshold);
    }
    public void RecordArrival (int arrivalTime) {
        UpdateStatistics(_lesson, arrivalTime);        
        NotifyStaff(_staff,_lesson);
    }
    #region IObservable Members
        public IDisposable Subscribe(IObserver<LectureTheatreDTO> lecturer)
        {
            RecordSubscription(_staff, lecturer);            
            lecturer.OnNext(_lesson);// Provide observer with existing data.
            return CreateUnsubscriber(_staff, lecturer);
        }
    #endregion IObservable Members

    private static void InitialiseStatistics (LectureTheatreDTO lesson, int expectedClassSize, int classCancellationThreshold) {
        lesson.classSize = expectedClassSize;
        lesson.cancellationThreshold = classCancellationThreshold;
    }
    private static void UpdateStatistics (LectureTheatreDTO lesson, int arrivalTime) {
        if (arrivalTime<=0)
            lesson.onTimeStudents++;
        else
            lesson.lateStudents++;   
    }
    private static void NotifyStaff (List<IObserver<LectureTheatreDTO>> staff, LectureTheatreDTO lesson) {
        //copy the list before enumerating in case one observer ragequits when they see what thet don't like
        List<IObserver<LectureTheatreDTO>> staffClone = new List<IObserver<LectureTheatreDTO>>(staff);
        foreach (var lecturer in staffClone)
            lecturer.OnNext(lesson);
    }
    //private static void RecordSubscription (List<IObserver<object>> staff, IObserver<object> lecturer) {
    private static void RecordSubscription (List<IObserver<LectureTheatreDTO>> staff, IObserver<LectureTheatreDTO> lecturer) {
        // Check whether lecturer is already registered. If not, add it
        if (! staff.Contains(lecturer)) {
            staff.Add(lecturer);
        }
    }
    private static void Unsubscribe (List<IObserver<LectureTheatreDTO>> staff, IObserver<LectureTheatreDTO> lecturer) {
        if (staff.Contains(lecturer)) 
            staff.Remove(lecturer);
    }
    private static IDisposable CreateUnsubscriber (List<IObserver<LectureTheatreDTO>> staff, IObserver<LectureTheatreDTO> lecturer) {
        return new UnsubscriberLambda(() => Unsubscribe(staff, lecturer) );
    }
}

class Professor : IObserver<LectureTheatreDTO> {    
    public string angry { get; set; } = "?";
    private IDisposable _subscription;

    public void Subscribe(ScheduledClass plannedClass) {
        SubscribeT(ref _subscription, plannedClass, this);
    }
    #region IObserver Members
        public virtual void OnNext(LectureTheatreDTO plannedClass) {
            if (ConfirmAttendance(plannedClass)) Unsubscribe();
        }
        public virtual void OnCompleted() {} // No implementation.
        public virtual void OnError(Exception e){} // No implementation.
    #endregion IObserver Members    

    private bool ConfirmAttendance(LectureTheatreDTO plannedClass) {
        if (plannedClass.onTimeStudents >= plannedClass.cancellationThreshold) {
            this.angry = "NO";             
            return true; //stop watching and get on with the job (even if it causess problems for list enumeration)
        } else {
            if (plannedClass.lateStudents >0 && 
                plannedClass.lateStudents >= plannedClass.classSize-plannedClass.cancellationThreshold) {
                this.angry = "YES" ;                  
                return true; //ragequit in protest( even if it causess problems for list enumeration)
            } 
        } 
        return false; //attendence incomplete
    }
    private void UnsubscribeT(IDisposable subscription) {
        subscription.Dispose();
    }
    private void SubscribeT(ref IDisposable subscription, ScheduledClass plannedClass, Professor subscriber) {
        subscription = plannedClass.Subscribe(subscriber);
    }
    private void Unsubscribe() {
        UnsubscribeT(_subscription);
    }
}

public class UnsubscriberLambda :IDisposable
{
    private Action _disposeCallback;

    public UnsubscriberLambda(Action callback)
    {
        _disposeCallback = callback;
    }
    #region IDisposable Members
        public void Dispose()
        {
            _disposeCallback();
        }
    #endregion
}