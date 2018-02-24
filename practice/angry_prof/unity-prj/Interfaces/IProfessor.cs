using System;
using System.Collections.Generic;

namespace Solution.Services {
    using LectureObserver = IObserver<LectureTheatre>;
    using LectureObservable = IObservable<LectureTheatre>;
    
    public interface IProfessor : LectureObserver 
    {    
        void Subscribe(IScheduledClass plannedClass);  
        Mind.MentalState GetMentalState();
    }
}