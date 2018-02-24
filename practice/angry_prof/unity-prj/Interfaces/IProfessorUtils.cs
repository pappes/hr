using System;
using System.Collections.Generic;

namespace Solution.Services {
    using LectureObserver = IObserver<LectureTheatre>;
    using LectureObservable = IObservable<LectureTheatre>;

    public interface IProfessorUtils 
    {    
        bool ConfirmAttendance(ref Mind.MentalState stateOfMind, LectureTheatre plannedClass);
        void Unsubscribe(IDisposable subscription);
        void Subscribe(ref IDisposable subscription, IScheduledClass plannedClass, Professor subscriber);
    }
}