using System;

namespace Solution.Services {
    public interface IScheduledClassFactory
    {
        IScheduledClass Create( int expectedClassSize, 
                                int classCancellationThreshold);
    }
}