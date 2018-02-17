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
        //[Fact]
        public void TestProfessorConfirmAttendance() 
        {
            var testClass = new ProfessorUtils();
            Professor.MentalState stateOfMind = Professor.MentalState.Pensive;
            Assert.Equal(Professor.MentalState.Pensive, stateOfMind);
            testClass.ConfirmAttendance(ref stateOfMind, new LectureTheatre{
                OnTimeStudents = 1, LateStudents = 1, ClassSize = 10, CancellationThreshold = 10});
            Assert.Equal(Professor.MentalState.Pensive, stateOfMind);
            testClass.ConfirmAttendance(ref stateOfMind, new LectureTheatre{
                OnTimeStudents = 1, LateStudents = 9, ClassSize = 10, CancellationThreshold = 10});
            Assert.Equal(Professor.MentalState.Calm, stateOfMind);
            testClass.ConfirmAttendance(ref stateOfMind, new LectureTheatre{ 
                OnTimeStudents = 9, LateStudents = 1, ClassSize = 10, CancellationThreshold = 10});
            Assert.Equal(Professor.MentalState.Calm, stateOfMind);
            testClass.ConfirmAttendance(ref stateOfMind, new LectureTheatre{ 
                OnTimeStudents = 9, LateStudents = 1, ClassSize = 10, CancellationThreshold = 0});
            Assert.Equal(Professor.MentalState.Angry, stateOfMind);
        }
    }
    
    namespace moq { 
        using LectureObserver = IObserver<LectureTheatre>;
        using LectureObservable = IObservable<LectureTheatre>;
        public class UnitTestMocks : TestHelper
        {
            public UnitTestMocks (ITestOutputHelper output)
            : base(output) { }
            
            [Fact]  
            public void ScheduledClass_Constructor()
            {
                var myClass = new ScheduledClass(1,2);
                Assert.Equal(1, myClass._Lesson.ClassSize);
                Assert.Equal(2, myClass._Lesson.CancellationThreshold);
            }
            [Fact]
            public void ScheduledClass_RecordArrival()
            {
                var testClass = new ScheduledClass(0, 0);

                var mockLectureTheatre = new Mock<LectureTheatre>();
                mockLectureTheatre
                    .Setup(x => x.UpdateStatistics(It.IsAny<int>()))
                    .Verifiable() ;
                var mockClassUtils = new Mock<IClassUtils>();
                testClass._ClassUtils = mockClassUtils.Object;
                mockClassUtils
                    .Setup(x => x.NotifyStaff(It.IsAny<List<LectureObserver>>(), It.IsAny<LectureTheatre>())) 
                    .Verifiable() ;                    
                testClass._Lesson = mockLectureTheatre.Object;

                testClass.RecordArrival(0);

                mockLectureTheatre.Verify(x => x.UpdateStatistics(It.IsAny<int>()), 
                                          Times.Once(),
                                          "Statistics were not updated when arrival was being recorded.");
                mockClassUtils.Verify(x => x.NotifyStaff(It.IsAny<List<LectureObserver>>(), It.IsAny<LectureTheatre>()), 
                                      Times.Once(),
                                      "Staff were not notiified when arrival was being recorded.");
                mockLectureTheatre.VerifyNoOtherCalls();
                mockClassUtils.VerifyNoOtherCalls();
            }
            [Fact]
            public void ScheduledClass_Subscribe()
            {
                var myClass = new ScheduledClass(1,2);
                var mockLectureObserver = new Mock<LectureObserver>();
                mockLectureObserver
                    .Setup(x => x.OnNext(It.IsAny<LectureTheatre>()));
                
                Assert.DoesNotContain(mockLectureObserver.Object, myClass._Staff); 
                IDisposable rageQuit = myClass.Subscribe(mockLectureObserver.Object); 
                Assert.Contains(mockLectureObserver.Object, myClass._Staff); 
            }
        }
    }

//TODO: unit test individual methods
}
