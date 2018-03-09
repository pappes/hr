using System;
using System.IO;
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
            container.RegisterType<IProfessor, Professor>();
            container.RegisterType<IScheduledClass, ScheduledClass>();
            container.RegisterType<IUnsubscriber, UnsubscriberLambda>();
            container.RegisterType<IScheduledClassFactory, ScheduledClassFactory>();

             container.RegisterType<ISolution, Solution>();
            /* container.RegisterType<ISolution, Solution>(
                         container.Resolve<IProfessor>(),
                         container.Resolve<IScheduledClassFactory>(),
                         new StreamReader(Console.OpenStandardInput(), Console.InputEncoding),
                         new StreamWriter(Console.OpenStandardOutput())); */
            // Instance registration 
            /*container.RegisterInstance<IProgramInput>(new StreamReader(Console.OpenStandardInput(), Console.InputEncoding));
            container.RegisterInstance<IProgramOutput>(new StreamWriter(Console.OpenStandardOutput())); */

        }
    }
}    