using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

//allow unit testing project to have visibility into private members
[assembly: InternalsVisibleToAttribute("lib.Xunit")]        //Visibility for XUnit
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]  //visibility for Moq

namespace Solution.Services {
    using LectureObserver = IObserver<LectureTheatre>;
    using LectureObservable = IObservable<LectureTheatre>;

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