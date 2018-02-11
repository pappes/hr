using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Solution.Services;
using Moq;

namespace lib.Xunit
{
    public class UnitTestTheories : TestHelper
    {
        public UnitTestTheories (ITestOutputHelper output)
        : base(output) { }


        [Theory]
        [InlineData("NO", "1\r\n1 1\r\n0 0")]
        [InlineData("NO", "1\r\n1 1\r\n0")]
        [InlineData("NO", "1\r\n10 5\r\n-4 -3 -2 -1 0 1 2 3 4 5 6")]
        [InlineData("NO", "1\r\n1 1\r\n0")]
        [InlineData("YES", "1\r\n10 5\r\n-3 -2 -1 0 1 2 3 4 5 6")]
        [InlineData("YES", "1\r\n1 1\r\n1")]
        [InlineData("NO", "1\r\n10 5\r\n-2 -1 0 1")]
        [InlineData(
@"YES
NO", 
@"2
4 3
-1 -3 4 2
4 2
0 -1 2 1")]

        public void TestProff(string expectedResult, string testData)
        {
            string actualResult = TestHelper.StreamDataToTestHarness(testData, Solution.Services.Solution.TestHarness); 
            Assert.Equal(expectedResult, actualResult);
        }

    }

    public class UnitTestFacts : TestHelper
    {
        public UnitTestFacts (ITestOutputHelper output)
        : base(output) { }

        [Fact]
        public void TestMaxClassOK()
        {
            TestLargeClass();
        }
        public void TestMaxClassFail()
        {
            TestLargeClass(lastStudentTime: 1);
        }
        public void TestSuperLargeClass()
        {
            TestLargeClass(classSize: 10000);
        }
        public void TestLargeClass(int classSize = 1000, int lastStudentTime = default(int))
        {
            //construct a test case dynamically by building up the input and output strings
            var combinedResults = ""; 
            char[] charsToTrim = {'\r', '\n', ' '};
            var maxTests = 10;
            var threshold = classSize;
            var expectedResult = lastStudentTime >0 ? "YES": "NO";
            //start the string builder big enough to hold the final sring size
            StringBuilder testData = new StringBuilder("", maxTests*classSize*2+maxTests*15);
            testData.Append( $"{maxTests}\r\n");
    //XUnitWriteLine($"maxTests:{maxTests} classSize:{classSize} threshold:{threshold} testData:{testData}");
            for (var i=0; i< maxTests; i++){
                testData.Append($"{classSize} {threshold}\r\n");
                for (var j=0; j<classSize; j++){
                    testData.Append( j<classSize-1 ? "0 " : lastStudentTime.ToString() );
                }
                testData.AppendLine();
                combinedResults = combinedResults + $"{expectedResult}\r\n";
            }
            string actualResult = TestHelper.StreamDataToTestHarness(testData.ToString(), Solution.Services.Solution.TestHarness);
            Assert.Equal(combinedResults.TrimEnd(charsToTrim), actualResult.TrimEnd(charsToTrim));
        }
    }
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
            Professor.ConfirmAttendance(ref stateOfMind, new LectureTheatre{
                OnTimeStudents = 1, LateStudents = 1, ClassSize = 10, CancellationThreshold = 10});
            Assert.Equal(Professor.MentalState.Pensive, stateOfMind);
            Professor.ConfirmAttendance(ref stateOfMind, new LectureTheatre{
                OnTimeStudents = 1, LateStudents = 9, ClassSize = 10, CancellationThreshold = 10});
            Assert.Equal(Professor.MentalState.Calm, stateOfMind);
            Professor.ConfirmAttendance(ref stateOfMind, new LectureTheatre{ 
                OnTimeStudents = 9, LateStudents = 1, ClassSize = 10, CancellationThreshold = 10});
            Assert.Equal(Professor.MentalState.Calm, stateOfMind);
            Professor.ConfirmAttendance(ref stateOfMind, new LectureTheatre{ 
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
            /*var mockLectureObserver = new Mock<LectureObserver>();
            mockLectureObserver
                .Setup(lo => lo.OnNext(LectureTheatre));

            private TestingObject<UserService> GetTestingObject()
            {
                var testingObject = new TestingObject<UserService>();
                testingObject.AddDependency(new Mock<LectureObserver>(MockBehavior.Strict));
                return testingObject;
            }

            [Fact]
            public async Task ScheduledClass_Subscribe()
            {
                TestingObject<UserService> testingObject = this.GetTestingObject();

                UserService userService = testingObject.GetResolvedTestingObject();

                await Assert.ThrowsAsync<ArgumentException>(async () =>
                    await userService.GetAsync(Guid.Empty));
            }
            public async Task ScheduledClass_Subscribe()
            {
                TestingObject<LectureObserver> testingObject = this.GetTestingObject();

                Guid userIdArg = Guid.NewGuid();

                var mockDbContext = testingObject.GetDependency<Mock<IDatabaseContext>>();
                mockDbContext
                    .Setup(dbc => dbc.FindSingleAsync<UserSql>(It.Is<Guid>(id => id == userIdArg)))
                    .ReturnsAsync(null);

                UserService userService = testingObject.GetResolvedTestingObject();
                await Assert.ThrowsAsync<Exception>(async ()
                    => await userService.GetAsync(userIdArg));
            }

*/           
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
