using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

//allow unit testing project to have visibility into private members
[assembly: InternalsVisibleToAttribute("lib.Xunit")]        //Visibility for XUnit
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]  //visibility for Moq

namespace Solution.Services {
    using LectureObserver = IObserver<LectureTheatre>;
    using LectureObservable = IObservable<LectureTheatre>;
    
    public class Professor : IProfessor 
    {    
        private Mind _emotions = new Mind();
        internal IDisposable _Subscription;
        private readonly IProfessorUtils _ProfessorUtils;

        public Professor (IProfessorUtils professorUtils/* ,
                          Mind.MentalState mentalState = Mind.MentalState.Pensive */) 
        {
            _ProfessorUtils = professorUtils;
           // _emotions.StateOfMind = mentalState;
            _emotions.StateOfMind = Mind.MentalState.Pensive;
        }

        public void Subscribe(IScheduledClass plannedClass) =>
            _ProfessorUtils.Subscribe(ref _Subscription, plannedClass, this);

        public Mind.MentalState GetMentalState()
        {
            return _emotions.StateOfMind;
        }

        #region IObserver Members
            public virtual void OnNext(LectureTheatre plannedClass) 
            {
                if (_ProfessorUtils.ConfirmAttendance(ref _emotions.StateOfMind, plannedClass)) Unsubscribe();
            }
            public virtual void OnCompleted() {} // No implementation.
            public virtual void OnError(Exception e){} // No implementation.
        #endregion IObserver Members    

        internal void Unsubscribe() =>
            _ProfessorUtils.Unsubscribe(_Subscription);
    }
}