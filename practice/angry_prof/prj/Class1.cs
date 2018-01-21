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
        timeLine(new StreamReader(Console.OpenStandardInput(), Console.InputEncoding), 
                 new StreamWriter(Console.OpenStandardOutput()));
    }
}

class LectureTheatreDTO {    
    public int onTime { get; set; } = 0;
    public int late { get; set; } = 0;   
    public int size { get; set; }
    public int threshold { get; set; }
}

class ScheduledClass : IObservable<LectureTheatreDTO> {   
    private List<IObserver<LectureTheatreDTO>> staff = new List<IObserver<LectureTheatreDTO>>(); 
    private LectureTheatreDTO lesson = new LectureTheatreDTO();

    public ScheduledClass (int classSize, int classThreshold) {
        lesson.size = classSize;
        lesson.threshold = classThreshold;
    }
    public void RecordArrival (int arrivalTime) {
        if (arrivalTime<=0)
            lesson.onTime++;
        else
            lesson.late++;        
        //copy the list before enumerating in case one observer ragequits when they see what thet don't like
        List<IObserver<LectureTheatreDTO>> observers = new List<IObserver<LectureTheatreDTO>>(staff);
        foreach (var lecturer in observers)
            lecturer.OnNext(lesson);
    }
    #region IObservable Members
        public IDisposable Subscribe(IObserver<LectureTheatreDTO> lecturer)
        {
            // Check whether lecturer is already registered. If not, add it
            if (! staff.Contains(lecturer)) {
                staff.Add(lecturer);
                // Provide observer with existing data.
                lecturer.OnNext(lesson);
            }
            return new Unsubscriber<LectureTheatreDTO>(staff, lecturer);
        }
    #endregion IObservable Members
}

class Professor : IObserver<LectureTheatreDTO> {    
    public string angry { get; set; } = "?";
    private IDisposable subscription;

    #region IObserver Members
        public virtual void OnNext(LectureTheatreDTO plannedClass) {
            if (plannedClass.onTime >= plannedClass.threshold) {
                this.angry = "NO"; 
                //stop watching and get on with the job, even if it causess problems for list enumeration
                Unsubscribe();  
            } else {
                if (plannedClass.late >0 && plannedClass.late >= plannedClass.size-plannedClass.threshold) {
                    this.angry = "YES" ;   
                    //ragequit in protest, even if it causess problems for list enumeration
                    Unsubscribe();  
                } 
            } 
        }
        public virtual void OnCompleted() {} // No implementation.

        // No implementation needed: Method is not called by the ScheduledClass class.
        public virtual void OnError(Exception e){} // No implementation.
    #endregion IObserver Members

    public virtual void Subscribe(ScheduledClass plannedClass) {
        subscription = plannedClass.Subscribe(this);
    }

    public virtual void Unsubscribe() {
        subscription.Dispose();
    }
}

//Sample code from 
//https://docs.microsoft.com/en-us/dotnet/standard/events/observer-design-pattern
// AND http://www.abhisheksur.com/2010/08/implementation-of-observer.html
public class Unsubscriber<T> :IDisposable
{
    private List<IObserver<T>> _observers;
    private IObserver<T> _observer;

    //TODO: use callback to unsubscribe
    public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
    {
        this._observers = observers;
        this._observer = observer;
    }
    #region IDisposable Members
        public void Dispose()
        {
            //Remove the Observer from the list when the subscriber disposes the diposable.
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    #endregion
}

