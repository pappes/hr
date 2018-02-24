using System;

namespace Solution.Services {    
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
}