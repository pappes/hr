using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity;

//allow unit testing project to have visibility into private memebers
/* [assembly: InternalsVisibleToAttribute("lib.Xunit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")] */

namespace Solution.Services {
    using LectureObserver = IObserver<LectureTheatre>;
    using LectureObservable = IObservable<LectureTheatre>;

    internal class ProfessorUtils : IProfessorUtils
    {    
        public bool ConfirmAttendance(ref Mind.MentalState stateOfMind, LectureTheatre plannedClass) 
        {
            if (plannedClass.OnTimeStudents >= plannedClass.CancellationThreshold) {
                stateOfMind = Mind.MentalState.Calm;              
                // Stop watching and get on with the job (even if it causess problems for list enumeration).
                return true; 
            } else {
                if (plannedClass.LateStudents >0 && 
                    plannedClass.LateStudents >= plannedClass.ClassSize-plannedClass.CancellationThreshold) {
                    stateOfMind = Mind.MentalState.Angry ;                  
                    // Ragequit in protest( even if it causess problems for list enumeration).
                    return true; 
                } 
            } 
            // Attendence is incomplete so need more data.
            return false; 
        }
        public void Unsubscribe(IDisposable subscription) =>
            subscription.Dispose();
        
        public void Subscribe(ref IDisposable subscription, IScheduledClass plannedClass, Professor subscriber) =>
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

    public class UnsubscriberLambda : IUnsubscriber
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