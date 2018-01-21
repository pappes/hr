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

class Sitting {    
    public string cancelled { get; set; } = "?";
    int size { get; set; }
    int threshold { get; set; }
    int onTime { get; set; } = 0;
    int late { get; set; } = 0;   

    public Sitting (int classSize, int classThreshold) {
        size = classSize;
        threshold = classThreshold;
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

class Professor {    
    public string angry { get;  } = "?";
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
}



class IObservableSitting<T> : IObservable<T> where T:class {    
    /* public string cancelled { get; set; } = "?";
    int size { get; set; }
    int threshold { get; set; }
    int onTime { get; set; } = 0;
    int late { get; set; } = 0; */

    
    private List<IObserver<T>> observers = new List<IObserver<T>>();
    #region IObservable<T> Members
        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber<T>(observers, observer);
        }
    #endregion

   /*  public Sitting (int classSize, int classThreshold) {
        size = classSize;
        threshold = classThreshold;
    } */
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
}




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
}

// A simple Disposable object
public class Unsubscriber<T> :IDisposable
{
    private List<IObserver<T>> observers;
    private IObserver<T> observer;

    public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
    {
        this.observers = observers;
        this.observer = observer;
    }
    #region IDisposable Members
        public void Dispose()
        {
            //We will just remove the Observer from the list whenever the unsubscription calls.
            if (observer != null && observers.Contains(observer))
                observers.Remove(observer);
        }
    #endregion
}


public class SampleObservable<T> : IObservable<T> where T:class
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
        
}