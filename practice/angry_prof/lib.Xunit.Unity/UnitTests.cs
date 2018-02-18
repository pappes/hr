using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Solution.Services;
using Moq;

namespace lib.Xunit.UnitTests
{
    public class UnitTestMethods
    {
        [Fact]
        public void TestLectureTheatre()
        {
            var testClass = new LectureTheatre();
            testClass.InitialiseStatistics(1,2);
            Assert.Equal(1, testClass.ClassSize);
            Assert.Equal(2, testClass.CancellationThreshold);
            Assert.Equal(default(int), testClass.OnTimeStudents);
            Assert.Equal(default(int), testClass.LateStudents);
            testClass.UpdateStatistics(0);
            Assert.Equal(1, testClass.ClassSize);
            Assert.Equal(2, testClass.CancellationThreshold);
            Assert.Equal(1, testClass.OnTimeStudents);
            Assert.Equal(default(int), testClass.LateStudents);
            testClass.UpdateStatistics(-1);
            Assert.Equal(1, testClass.ClassSize);
            Assert.Equal(2, testClass.CancellationThreshold);
            Assert.Equal(2, testClass.OnTimeStudents);
            Assert.Equal(default(int), testClass.LateStudents);
            testClass.UpdateStatistics(int.MinValue);
            Assert.Equal(1, testClass.ClassSize);
            Assert.Equal(2, testClass.CancellationThreshold);
            Assert.Equal(3, testClass.OnTimeStudents);
            Assert.Equal(default(int), testClass.LateStudents);
            testClass.UpdateStatistics(1);
            Assert.Equal(1, testClass.ClassSize);
            Assert.Equal(2, testClass.CancellationThreshold);
            Assert.Equal(3, testClass.OnTimeStudents);
            Assert.Equal(1, testClass.LateStudents);
            testClass.UpdateStatistics(int.MaxValue);
            Assert.Equal(1, testClass.ClassSize);
            Assert.Equal(2, testClass.CancellationThreshold);
            Assert.Equal(3, testClass.OnTimeStudents);
            Assert.Equal(2, testClass.LateStudents);
        }
        [Fact]
        public void TestUnsubscriberLambda() 
        {
            var canary=1;
            //disposable will run lambda when coming out of scope of using block
            using (var x = new UnsubscriberLambda(() => canary=2)) 
            {
                Assert.Equal(1, canary);
            }
            Assert.Equal(2, canary);
        }
      /*   [Fact]
        public void TestProfessorConfirmAttendance() 
        {
            var testClass = new ProfessorUtils();
            Mind.MentalState stateOfMind = Mind.MentalState.Pensive;
            Assert.Equal(Mind.MentalState.Pensive, stateOfMind);
            testClass.ConfirmAttendance(ref stateOfMind, new LectureTheatre{
                OnTimeStudents = 1, LateStudents = 1, ClassSize = 10, CancellationThreshold = 10});
            Assert.Equal(Mind.MentalState.Pensive, stateOfMind);
            testClass.ConfirmAttendance(ref stateOfMind, new LectureTheatre{
                OnTimeStudents = 1, LateStudents = 9, ClassSize = 10, CancellationThreshold = 10});
            Assert.Equal(Mind.MentalState.Calm, stateOfMind);
            testClass.ConfirmAttendance(ref stateOfMind, new LectureTheatre{ 
                OnTimeStudents = 9, LateStudents = 1, ClassSize = 10, CancellationThreshold = 10});
            Assert.Equal(Mind.MentalState.Calm, stateOfMind);
            testClass.ConfirmAttendance(ref stateOfMind, new LectureTheatre{ 
                OnTimeStudents = 9, LateStudents = 1, ClassSize = 10, CancellationThreshold = 0});
            Assert.Equal(Mind.MentalState.Angry, stateOfMind);
        } */
    }
    
    namespace moq { 
        using LectureObserver = IObserver<LectureTheatre>;
        using LectureObservable = IObservable<LectureTheatre>;
        public class UnitTestMocks : TestHelper
        {
            public UnitTestMocks (ITestOutputHelper output)
            : base(output) { }
            
            /* [Fact]  
            public void ScheduledClass_Constructor()
            {
                //TODO include new constructior optional parameters
                var mockLectureTheatre = new Mock<LectureTheatre>();
                mockLectureTheatre
                    .Setup(x => x.InitialiseStatistics(It.IsAny<int>(), It.IsAny<int>()))
                    .Verifiable() ;
                var mockClassUtils = new Mock<IClassUtils>();
                var myClass = new ScheduledClass(expectedClassSize: 1, 
                                                 classCancellationThreshold: 2, 
                                                 lectureTheatre: mockLectureTheatre.Object);
                mockLectureTheatre.Verify(x => x.InitialiseStatistics(It.IsAny<int>(), It.IsAny<int>()), 
                                          Times.Once(),
                                          "Statistics were not initialised hwen scheduled calss was initialised.");
            } */
            /* [Fact]
            public void ScheduledClass_RecordArrival()
            {
                var mockLectureTheatre = new Mock<LectureTheatre>();
                mockLectureTheatre
                    .Setup(x => x.UpdateStatistics(It.IsAny<int>()))
                    .Verifiable() ;
                 mockLectureTheatre
                    .Setup(x => x.InitialiseStatistics(It.IsAny<int>(), It.IsAny<int>()))
                    .Verifiable() ;
                var mockClassUtils = new Mock<IClassUtils>(); 
                mockClassUtils
                    .Setup(x => x.NotifyStaff(It.IsAny<List<LectureObserver>>(), It.IsAny<LectureTheatre>())) 
                    .Verifiable() ;                    
                var testClass = new ScheduledClass(expectedClassSize: 0, 
                                                   classCancellationThreshold: 0, 
                                                   classUtils: mockClassUtils.Object, 
                                                   lectureTheatre: mockLectureTheatre.Object);                                                   

                testClass.RecordArrival(0);

                mockLectureTheatre.Verify(x => x.UpdateStatistics(It.IsAny<int>()), 
                                          Times.Once(),
                                          "Statistics were not updated when arrival was being recorded.");
                mockLectureTheatre.Verify(x => x.InitialiseStatistics(It.IsAny<int>(), It.IsAny<int>()), 
                                          Times.Once(),
                                          "Statistics were not initialised hwen scheduled calss was initialised.");
                mockClassUtils.Verify(x => x.NotifyStaff(It.IsAny<List<LectureObserver>>(), It.IsAny<LectureTheatre>()), 
                                      Times.Once(),
                                      "Staff were not notified when arrival was being recorded.");
                 mockLectureTheatre.VerifyNoOtherCalls();
                mockClassUtils.VerifyNoOtherCalls(); 
            } */
           /*  [Fact]
            public void ScheduledClass_Subscribe()
            {
                var myClass = new ScheduledClass(1,2);
                var mockLectureObserver = new Mock<LectureObserver>();
                mockLectureObserver
                    .Setup(x => x.OnNext(It.IsAny<LectureTheatre>()));
                
                /*Assert.DoesNotContain(mockLectureObserver.Object, myClass._Staff); 
                IDisposable rageQuit = myClass.Subscribe(mockLectureObserver.Object); 
                Assert.Contains(mockLectureObserver.Object, myClass._Staff);   * /
                
                var mockLectureObserver = new Mock<LectureObserver>();
                mockLectureObserver
                    .Setup(x => x.OnNext(It.IsAny<LectureTheatre>()));
                var mockStaff = new Mock<List<LectureObserver>>();
                mockStaff
                    .Setup(x => x.OnNext(It.IsAny<LectureTheatre>()));
                    List<LectureObserver>
                var myClass = new ScheduledClass(expectedClassSize: 1, 
                                                 classCancellationThreshold: 2, 
                                                 lectureTheatre: mockLectureTheatre.Object);
                
                Assert.DoesNotContain(mockLectureObserver.Object, myClass._Staff); 
                IDisposable rageQuit = myClass.Subscribe(mockLectureObserver.Object); 
                Assert.Contains(mockLectureObserver.Object, myClass._Staff); 
                
                
                
                
                /*
                _ClassUtils.RecordSubscription(_Staff, lecturer);            
                // Provide observer with existing data.
                lecturer.OnNext(_Lesson);
                return _ClassUtils.CreateUnsubscriber(_Staff, lecturer);* / 
            } */
        }
    }

//TODO: unit test individual methods
}
