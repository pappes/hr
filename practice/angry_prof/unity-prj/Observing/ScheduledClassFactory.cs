using System;

namespace Solution.Services {

    //inspired by https://stackoverflow.com/questions/1943576/is-there-a-pattern-for-initializing-objects-created-via-a-di-container
    public class ScheduledClassFactory : IScheduledClassFactory
    {
        private IClassUtils _ClassUtils;
        public ScheduledClassFactory (IClassUtils classUtils) =>
            _ClassUtils = classUtils;

        public IScheduledClass Create( int expectedClassSize, 
                                       int classCancellationThreshold)
        {
            var lesson = new LectureTheatre();
            lesson.InitialiseStatistics(expectedClassSize:expectedClassSize, 
                                        classCancellationThreshold:classCancellationThreshold);
            return new ScheduledClass(lectureTheatre: lesson, 
                                      classUtils:_ClassUtils);
        }
    }  
}