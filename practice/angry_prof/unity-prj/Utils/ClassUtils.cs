using System;
using System.Collections.Generic;

namespace Solution.Services {
    using LectureObserver = IObserver<LectureTheatre>;
    using LectureObservable = IObservable<LectureTheatre>;

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
}