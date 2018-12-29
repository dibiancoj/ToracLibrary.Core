using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using Library.PerfTest.Interface;

namespace Library.PerfTest
{
    class Program
    {

        // *** to run from command line prompt "dotnet run -c Release" ***

        static void Main(string[] args)
        {
            var testsFound = typeof(Program).Assembly.GetTypes().Where(x => !x.IsInterface && typeof(IPerformanceTest).IsAssignableFrom(x)).ToList();

            var testToRun = WhichTestToRun(testsFound);

            if (testToRun != null)
            {
                var summary = BenchmarkRunner.Run(testToRun);

                Console.WriteLine("Performance Test Complete. Press Any Key To Exit.");
                Console.ReadKey();
            }
        }

        private static Type WhichTestToRun(IList<Type> availableTests)
        {
            do
            {
                Console.WriteLine("Please Enter The Test Id You Want To Run");
                Console.WriteLine("E - Exit");

                int i = 0;
                foreach (var test in availableTests)
                {
                    Console.WriteLine("{0} - {1}", i, test.Name);
                    i++;
                }

                var rawTestIdToRun = Console.ReadLine();

                if (int.TryParse(rawTestIdToRun, out var testIdToRun))
                {
                    if (testIdToRun >= 0 && testIdToRun < availableTests.Count)
                    {
                        return availableTests.ElementAt(testIdToRun);
                    }
                }
                else if (rawTestIdToRun.Equals("e", StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }

                Console.WriteLine(string.Empty);
                Console.WriteLine("Invalid Command");
                Console.WriteLine(string.Empty);

            } while (true);
        }

    }
}
