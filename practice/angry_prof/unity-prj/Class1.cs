using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity;

//allow unit testing project to have visibility into private memebers
[assembly: InternalsVisibleToAttribute("lib.Xunit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Solution.Services {
    using LectureObserver = IObserver<LectureTheatre>;
    using LectureObservable = IObservable<LectureTheatre>;
    public class Top 
    {
        static void Main(String[] args) 
        {
        // UnityContainer container;
            //unity dependency injection from https://msdn.microsoft.com/en-us/library/dn178463(v=pandp.30).aspx
            using (var container = new UnityContainer())
            {
                ContainerBootstrapper.RegisterTypes(container);

                /* container.Resolve<IProfessorUtils>().Initialize();
                container.Resolve<IClassUtils>().Initialize(); */
                //container.Resolve<IUnsubscriber>().Initialize();

                var solution = container.Resolve<ISolution>();
                StreamReader stdin = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);
                StreamWriter stdout = new StreamWriter(Console.OpenStandardOutput());
                solution.TimeLine(stdin, stdout);
                stdout.Flush();
            }
        }
    }
    public interface ISolution {
        void TestHarness(StreamReader input, StreamWriter output);
        void TimeLine(StreamReader source, StreamWriter destination) ;
    }

    public class Solution : ISolution {
    //https://www.hackerrank.com/challenges/angry-professor/problem
        private readonly IProfessor _lessonObserver;        
        //private readonly IScheduledClass _lessonProvider;
        private readonly IScheduledClassFactory _scheduledClassFactory;

        public Solution (IProfessor professor,
                         IScheduledClassFactory scheduledClassFactory) 
        {
            _lessonObserver = professor;
            _scheduledClassFactory = scheduledClassFactory;

        }
        public void TestHarness(StreamReader input, StreamWriter output) =>
            // Call actual logic.
            TimeLine(input, output);

        public void TimeLine(StreamReader source, StreamWriter destination) 
        {
            //var lessonObserver = new Professor();
        
            int t = Convert.ToInt32(source.ReadLine());
            for(int a0 = 0; a0 < t; a0++){
                string[] tokens_n = source.ReadLine().Split(' ');
                int n = Convert.ToInt32(tokens_n[0]);
                int k = Convert.ToInt32(tokens_n[1]);
                string[] a_temp = source.ReadLine().Split(' ');
                int[] a = Array.ConvertAll(a_temp,Int32.Parse);
                
                //TODO pull mental state out of proffessor
                 var lessonProvider = _scheduledClassFactory.Create(n, k);
                _lessonObserver.Subscribe(lessonProvider);
                foreach (var time in a){
                    lessonProvider.RecordArrival(time);      
                }
                destination.WriteLine(_lessonObserver.GetMentalState() == Mind.MentalState.Angry ? "YES" : "NO");  
            }
        }
    }
    
    public interface IScheduledClassFactory
    {
        IScheduledClass Create( int expectedClassSize, 
                                int classCancellationThreshold);
    }
    public class ScheduledClassFactory : IScheduledClassFactory
    {
        private IClassUtils _ClassUtils;
        public ScheduledClassFactory (IClassUtils classUtils)
        {
            _ClassUtils = classUtils;
        }
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