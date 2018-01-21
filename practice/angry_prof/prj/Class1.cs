using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


/* 
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive; 
using System.Reactive.Linq; */



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

    // public LectureTheatre () {
    // }
/* 
    public void RecordArrival (int time) {
        time<=0 ? onTime++ : late++;
        if (onTime > size-threshold) {
            cancelled = 'NO'
        } else {
        if (late > size-threshold) {
            cancelled = 'YES'            
        }
    } */
}

class ScheduledClass : IObservable<LectureTheatreDTO> {   
    private List<IObserver<LectureTheatreDTO>> staff; 
    private LectureTheatreDTO lesson;

    /*public string cancelled { get; set; } = "?";
    int size { get; set; }
    int threshold { get; set; }*/

    public ScheduledClass (int classSize, int classThreshold) {
        staff = new List<IObserver<LectureTheatreDTO>>();
        lesson = new LectureTheatreDTO();
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
/* 
    public void RecordArrival (int time) {
        time<=0 ? onTime++ : late++;
        if (onTime > size-threshold) {
            cancelled = 'NO'
        } else {
        if (late > size-threshold) {
            cancelled = 'YES'            
        }
    } */
}

class Professor : IObserver<LectureTheatreDTO> {    
    public string angry { get; set; } = "?";
    private IDisposable subscription;
    //Sitting classRoomSitting;

   /*  public Professor (int classSize, int classThreshold) {
        size = classSize;
        threshold = classThreshold;
    } */
 
   /* public void StudentArrival (Sitting s) {
        classRoomSitting = s;
        if (s.onTime > s.threshold) {
            s.cancelled = "NO";
        } else {
            if (s.late > s.size-s.threshold) {
                s.cancelled = "YES" ;           
            } 
        } 
    }*/
   public virtual void OnNext(LectureTheatreDTO plannedClass) 
   {
        if (plannedClass.onTime > plannedClass.threshold) {
            this.angry = "NO";
        } else {
            if (plannedClass.late > plannedClass.size-plannedClass.threshold) {
                this.angry = "YES" ;     
                Unsubscribe();      
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
      //flightInfos.Clear();
   }

   public virtual void OnCompleted() 
   {
      //flightInfos.Clear();
   }

   // No implementation needed: Method is not called by the BaggageHandler class.
   public virtual void OnError(Exception e)
   {
      // No implementation.
   }
}


/* class IObservableSitting<T> : IObservable<T> where T:class {    
    // public string cancelled { get; set; } = "?";
    // int size { get; set; }
    // int threshold { get; set; }
    // int onTime { get; set; } = 0;
    // int late { get; set; } = 0; 

    
    private List<IObserver<T>> observers = new List<IObserver<T>>();
    #region IObservable<T> Members
        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber<T>(observers, observer);
        }
    #endregion

    // public Sitting (int classSize, int classThreshold) {
    //     size = classSize;
    //     threshold = classThreshold;
    // } 
 
    // public void RecordArrival (int time) {
    //     time<=0 ? onTime++ : late++;
    //     if (onTime > size-threshold) {
    //         cancelled = 'NO'
    //     } else {
    //     if (late > size-threshold) {
    //         cancelled = 'YES'            
    //     }
    // } 
}

class IObserverProfessor<T> : IObserver<T> where T:class, new() {    
    public string angry { get;  } = "?";

    private IDisposable unsubscriber;
    #region IObserver<T> Members\
        public void OnCompleted()
        {
            //Console.WriteLine("{0} : OnComplete is called.", this.Name);
            this.Unsubscribe();
        }

        public void OnError(Exception error)
        {
            //Console.WriteLine("{0} : OnError is called", this.Name);
            this.LogException(error);
        }

        public void OnNext(T value)
        {
            //Console.WriteLine("{0} : OnNext is called.", this.Name);
            //this.StudentArrival(value);
        }
    #endregion    

    public virtual void Subscribe(IObservable<T> observable)
    {
        if(observable != null)
            this.unsubscriber = observable.Subscribe(this);
    }

    public virtual void Unsubscribe()
    {
        //Console.WriteLine("{0} : Calling Unsubscriber for Observer", this.Name);
        if(this.unsubscriber != null)
            this.unsubscriber.Dispose();
    }

    private void LogException(Exception error)
    {
        //Console.WriteLine("Exception occurred while traversing thorough objects of type {0}", error.GetType().Name);
        Console.WriteLine("Exception Message : {0}", error.Message);
    }
} */



/* 
//sample code from http://www.abhisheksur.com/2010/08/implementation-of-observer.html
//want a portable observable implementation becasue hackerrank does not allow asemblies
public class SampleObserver<T> : IObserver<T> where T:class, new()
{
    private IDisposable unsubscriber;

    public string Name { get; set; }

    public SampleObserver(string name)
    {
        this.Name = name;
    }

    #region IObserver<T> Members\
        public void OnCompleted()
        {
            Console.WriteLine("{0} : OnComplete is called.", this.Name);
            this.Unsubscribe();
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("{0} : OnError is called", this.Name);
            this.LogException(error);
        }

        public void OnNext(T value)
        {
            Console.WriteLine("{0} : OnNext is called.", this.Name);
            this.LogProperties(value);
        }
    #endregion

    public virtual void Subscribe(IObservable<T> observable)
    {
        if(observable != null)
            this.unsubscriber = observable.Subscribe(this);
    }

    public virtual void Unsubscribe()
    {
        Console.WriteLine("{0} : Calling Unsubscriber for Observer", this.Name);
        if(this.unsubscriber != null)
            this.unsubscriber.Dispose();
    }

    private void LogException(Exception error)
    {
        Console.WriteLine("Exception occurred while traversing thorough objects of type {0}", error.GetType().Name);
        Console.WriteLine("Exception Message : {0}", error.Message);
    }
    private void LogProperties(T value)
    {
        T tobj = value;
        PropertyInfo[] pinfos = tobj.GetType().GetProperties();
        Console.WriteLine("==========================={0}====================================================", this.Name);
        Console.WriteLine("Lets trace all the Properties ");

        foreach (PropertyInfo pinfo in pinfos)
            Console.WriteLine("Value of {0} is {1}", pinfo.Name, pinfo.GetValue(tobj, null));

        Console.WriteLine("============================={0}===================================================", this.Name);

            
    }
} */

//Sample code from 
//https://docs.microsoft.com/en-us/dotnet/standard/events/observer-design-pattern
// AND http://www.abhisheksur.com/2010/08/implementation-of-observer.html
public class Unsubscriber<T> :IDisposable
{
    private List<IObserver<T>> _observers;
    private IObserver<T> _observer;

    //TODO: use callback to unubscribe
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


/* public class SampleObservable<T> : IObservable<T> where T:class
{

    private List<IObserver<T>> observers = new List<IObserver<T>>();
      
    #region IObservable<T> Members
        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber<T>(observers, observer);
        }
    #endregion

    public void TrackObserver(T obj)
    {
        if (obj != null)
        {
            foreach (IObserver<T> observer in observers)
                if (observers.Contains(observer))
                    observer.OnNext(obj);
                else
                    observer.OnError(new ApplicationException("Not available"));
        }
    }

    public void TrackComplete()
    {
        foreach (IObserver<T> observer in observers)
            observer.OnCompleted();

        observers.Clear();
    } 
}*/
        
