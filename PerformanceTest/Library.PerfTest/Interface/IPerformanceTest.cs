using System;
using System.Collections.Generic;
using System.Text;

namespace Library.PerfTest.Interface
{

    /// <summary>
    /// Used so we can load the available tests
    /// </summary>
    public interface IPerformanceTest
    {
        string Command { get; }
        string Description { get; }
    }

}
