using System;
using System.Collections.Generic;

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
}