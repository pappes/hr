using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity;

//allow unit testing project to have visibility into private memebers
[assembly: InternalsVisibleToAttribute("lib.Xunit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Solution.Services {
    using LectureObserver = IObserver<LectureTheatre>;
    using LectureObservable = IObservable<LectureTheatre>;
    
    public interface IProfessor : LectureObserver 
    {    
        void Subscribe(IScheduledClass plannedClass);  
        Mind.MentalState GetMentalState();
    }

    public interface IScheduledClass : LectureObservable {          
        void RecordArrival (int arrivalTime) ;
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

    public class Mind 
    {    
        public enum MentalState { Calm, Pensive, Angry}
        public MentalState StateOfMind;
    }    

    public class Professor : IProfessor 
    {    
        private Mind _emotions = new Mind();
        internal IDisposable _Subscription;
        private readonly IProfessorUtils _ProfessorUtils;

        public Professor (IProfessorUtils professorUtils/* ,
                          Mind.MentalState mentalState = Mind.MentalState.Pensive */) 
        {
            _ProfessorUtils = professorUtils;
           // _emotions.StateOfMind = mentalState;
            _emotions.StateOfMind = Mind.MentalState.Pensive;
        }

        public void Subscribe(IScheduledClass plannedClass) =>
            _ProfessorUtils.Subscribe(ref _Subscription, plannedClass, this);

        public Mind.MentalState GetMentalState()
        {
            return _emotions.StateOfMind;
        }

        #region IObserver Members
            public virtual void OnNext(LectureTheatre plannedClass) 
            {
                if (_ProfessorUtils.ConfirmAttendance(ref _emotions.StateOfMind, plannedClass)) Unsubscribe();
            }
            public virtual void OnCompleted() {} // No implementation.
            public virtual void OnError(Exception e){} // No implementation.
        #endregion IObserver Members    

        internal void Unsubscribe() =>
            _ProfessorUtils.Unsubscribe(_Subscription);
    }

    public class ScheduledClass : IScheduledClass {   
        private LectureTheatre _Lesson;
        private IClassUtils _ClassUtils;
        private List<LectureObserver> _Staff; 
        
        public ScheduledClass ( 
                               LectureTheatre lectureTheatre,
                               IClassUtils classUtils,
                               List<LectureObserver> observers  = null )
        {
            _Lesson = lectureTheatre;
            _ClassUtils = classUtils;
            _Staff = observers ??  new List<LectureObserver>();
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
}