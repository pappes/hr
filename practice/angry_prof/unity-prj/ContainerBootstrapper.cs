using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace Solution.Services {
    public class ContainerBootstrapper
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            
            /* container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default); */

            // Instance registration
            container.RegisterInstance<IProfessorUtils>(new ProfessorUtils());
            container.RegisterInstance<IClassUtils>(new ClassUtils());

            // Register store types
            container.RegisterType<ISolution, Solution>();
            container.RegisterType<IProfessor, Professor>();
            container.RegisterType<IScheduledClass, ScheduledClass>();
            container.RegisterType<IUnsubscriber, UnsubscriberLambda>();
            container.RegisterType<IScheduledClassFactory, ScheduledClassFactory>();

        }
    }
}    