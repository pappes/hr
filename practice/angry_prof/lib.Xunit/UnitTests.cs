using System;
using System.Collections.Generic;
using Xunit;
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
        public void TestProfessorConfirmAttendance() 
        {
            var stateOfMind = new Professor.MentalState();
            ProfessorUtils.ConfirmAttendance(ref stateOfMind, new LectureTheatre{
                OnTimeStudents = 1, LateStudents = 1, ClassSize = 10, CancellationThreshold = 10});
            Assert.Equal(Professor.MentalState.Pensive, stateOfMind);
            ProfessorUtils.ConfirmAttendance(ref stateOfMind, new LectureTheatre{
                OnTimeStudents = 1, LateStudents = 9, ClassSize = 10, CancellationThreshold = 10});
            Assert.Equal(Professor.MentalState.Calm, stateOfMind);
            ProfessorUtils.ConfirmAttendance(ref stateOfMind, new LectureTheatre{ 
                OnTimeStudents = 9, LateStudents = 1, ClassSize = 10, CancellationThreshold = 10});
            Assert.Equal(Professor.MentalState.Calm, stateOfMind);
            ProfessorUtils.ConfirmAttendance(ref stateOfMind, new LectureTheatre{ 
                OnTimeStudents = 9, LateStudents = 1, ClassSize = 10, CancellationThreshold = 0});
            Assert.Equal(Professor.MentalState.Angry, stateOfMind);
        }
    }
    
    namespace moq { 
        using LectureObserver = IObserver<LectureTheatre>;
        using LectureObservable = IObservable<LectureTheatre>;
        public class NullifyNotifyStaff
        {
            private static Action _NotifyStaffCallback; 
            public void SetCallback(Action cb) => _NotifyStaffCallback = cb;
            internal static void NotifyStaff(List<LectureObserver> staff, LectureTheatre lesson)
            {
                _NotifyStaffCallback();
            }
        }
        public class UnitTestMocks
        {
            [Fact]  
            public void ScheduledClass_Constructor()
            {
                var myClass = new ScheduledClass(1,2);
                Assert.Equal(1, myClass._Lesson.ClassSize);
                Assert.Equal(2, myClass._Lesson.CancellationThreshold);
            }
            public void ScheduledClass_RecordArrival()
            {
               /*  var mockLectureObserver = new Mock<LectureObserver>();
                mockLectureObserver
                    .Setup(x => x.OnNext(It.IsAny<LectureTheatre>())) 
                    .Verifiable() ; */
                var mockLectureTheatre = new Mock<LectureTheatre>();
                

                
                mockLectureTheatre
                    .Setup(x => x.UpdateStatistics(It.IsAny<int>()))
                    .Callback((int arrivalTime) => Assert.Equal(0,arrivalTime))
                    .Verifiable() ;


                /*var myClass = new NullifyNotifyStaff(1,2);
                myClass.RecordArrival(0);


                myClass._Staff.Add(mockLectureObserver.Object); 
                Assert.Contains(mockLectureObserver.Object, myClass._Staff); 
                
                mockLectureObserver
                    .Verify(x => x.OnNext(It.IsAny<LectureTheatre>()), 
                            Times.Once()); */
            }
            public void ScheduledClass_Subscribe()
            {
                var myClass = new ScheduledClass(1,2);
                var mockLectureObserver = new Mock<LectureObserver>();
                mockLectureObserver
                    .Setup(x => x.OnNext(It.IsAny<LectureTheatre>()))/* 
                    .Returns(void) */;
                
                Assert.DoesNotContain(mockLectureObserver.Object, myClass._Staff); 
                IDisposable rageQuit = myClass.Subscribe(mockLectureObserver.Object); 
                Assert.Contains(mockLectureObserver.Object, myClass._Staff); 
            }
        }
    }

//TODO: unit test individual methods
}
