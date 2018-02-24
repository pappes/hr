using System;
using System.IO;

namespace Solution.Services {
    public interface ISolution {
        void TestHarness(StreamReader input, StreamWriter output);
        void TimeLine(StreamReader source, StreamWriter destination) ;
    }
}