using System;

namespace Solution.Services {

    public interface IUnsubscriber : IDisposable
    {
        //void IUnsubscriber(Action callback); 
        //cant enforce constructor params in an interface :(
    }
}