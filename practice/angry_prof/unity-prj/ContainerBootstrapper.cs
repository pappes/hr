using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity;


namespace Solution.Services {
    public class ContainerBootstrapper
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ISolution, Solution>();
            //container.RegisterType<IProfessorUtils>(new ProfessorUtils());
            container.RegisterType<IProfessor, Professor>();
            container.RegisterType<IScheduledClass, ScheduledClass>();
            container.RegisterType<IProfessorUtils, ProfessorUtils>();
            container.RegisterType<IClassUtils, ClassUtils>();
            container.RegisterType<IScheduledClassFactory, ScheduledClassFactory>();
            /* .RegisterType<IUnsubscriber<Action>, UnsubscriberLambda<Action>>(
                new InjectionConstructor(Action)); */
            container.RegisterType<IUnsubscriber, UnsubscriberLambda>();

            
//myContainer.RegisterType<CustomerData>(new ContainerControlledLifetimeManager());
        }
    }
}    