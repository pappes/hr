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

    public interface IProfessorUtils 
    {    
        bool ConfirmAttendance(ref Mind.MentalState stateOfMind, LectureTheatre plannedClass);
        void Unsubscribe(IDisposable subscription);
        void Subscribe(ref IDisposable subscription, IScheduledClass plannedClass, Professor subscriber);
    }

    public interface IClassUtils 
    {   
        void NotifyStaff (List<LectureObserver> staff, LectureTheatre lesson) ;
        void RecordSubscription (List<LectureObserver> staff, LectureObserver lecturer) ;
        void Unsubscribe (List<LectureObserver> staff, LectureObserver lecturer) ;
        IDisposable CreateUnsubscriber (List<LectureObserver> staff, LectureObserver lecturer);
    }


    public interface IUnsubscriber : IDisposable
    {
        //void IUnsubscriber(Action callback); 
        //cant enforce constructor params in an interface :(
    }
}