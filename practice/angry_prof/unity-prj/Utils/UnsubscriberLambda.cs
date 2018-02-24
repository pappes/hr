using System;
using System.Collections.Generic;

namespace Solution.Services {
    using LectureObserver = IObserver<LectureTheatre>;
    using LectureObservable = IObservable<LectureTheatre>;

    public class UnsubscriberLambda : IUnsubscriber
    {
        private Action _DisposeCallback;

        public UnsubscriberLambda(Action callback) => 
            _DisposeCallback = callback;
        
        #region IDisposable Members
            public void Dispose() =>
                _DisposeCallback();

        #endregion
    }
}