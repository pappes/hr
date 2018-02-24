using System;
using System.Collections.Generic;

namespace Solution.Services {
    using LectureObserver = IObserver<LectureTheatre>;
    using LectureObservable = IObservable<LectureTheatre>;

    public interface IClassUtils 
    {   
        void NotifyStaff (List<LectureObserver> staff, LectureTheatre lesson) ;
        void RecordSubscription (List<LectureObserver> staff, LectureObserver lecturer) ;
        void Unsubscribe (List<LectureObserver> staff, LectureObserver lecturer) ;
        IDisposable CreateUnsubscriber (List<LectureObserver> staff, LectureObserver lecturer);
    }
}