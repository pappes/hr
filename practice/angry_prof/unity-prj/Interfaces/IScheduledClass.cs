using System;
using System.Collections.Generic;

namespace Solution.Services {
    using LectureObserver = IObserver<LectureTheatre>;
    using LectureObservable = IObservable<LectureTheatre>;
    
    public interface IScheduledClass : LectureObservable {          
        void RecordArrival (int arrivalTime) ;
    }    
}