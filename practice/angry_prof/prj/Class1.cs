using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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
            destination.WriteLine(result[a0]);        
        }*/
      ScheduledClass lessonProvider = new ScheduledClass(10,7);
      Professor lessonObserver = new Professor();
      lessonObserver.Subscribe(lessonProvider);

      lessonProvider.RecordArrival(-1);
            destination.WriteLine(lessonObserver.angry);  
      lessonProvider.RecordArrival(1);
      lessonProvider.RecordArrival(1);
      lessonProvider.RecordArrival(1);
      lessonProvider.RecordArrival(1);
            destination.WriteLine(lessonObserver.angry);  
            destination.WriteLine(tests); 
            destination.WriteLine("no class for you");  
            destination.WriteLine("no class today");  
    }
    static void Main(String[] args) {
        timeLine(new StreamReader(Console.OpenStandardInput(), Console.InputEncoding), 
                 new StreamWriter(Console.OpenStandardOutput()));
    }
}


class LectureTheatreDTO {    
    public string cancelled { get; set; } = "?";
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
        foreach (var lecturer in staff)
            lecturer.OnNext(lesson);
    }
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
}

class Professor : IObserver<LectureTheatreDTO> {    
    public string angry { get; set; } = "?";
    private IDisposable subscription;

   public virtual void OnNext(LectureTheatreDTO plannedClass) 
   {
        if (plannedClass.onTime > plannedClass.threshold) {
            this.angry = "NO";
        } else {
            if (plannedClass.late > plannedClass.size-plannedClass.threshold) {
                this.angry = "YES" ;   
                //TODO: investigate why unsubscribing causes problems    Would it cause unsubscribe to be called twice?
                //Unsubscribe();  //causes thread isues    
            } 
        } 
   }

   public virtual void Subscribe(ScheduledClass plannedClass)
   {
      subscription = plannedClass.Subscribe(this);
   }

   public virtual void Unsubscribe()
   {
      subscription.Dispose();
   }

   public virtual void OnCompleted() 
   {
      // No implementation.
   }

   // No implementation needed: Method is not called by the ScheduledClass class.
   public virtual void OnError(Exception e)
   {
      // No implementation.
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

