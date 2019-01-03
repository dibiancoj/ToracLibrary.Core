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
            var testToRun = WhichTestToRun(AvailableTests());

            if (testToRun != null)
            {
                var summary = BenchmarkRunner.Run(testToRun);

                Console.WriteLine("Performance Test Complete. Press Any Key To Exit.");
                Console.ReadKey();
            }
        }

        private static Type WhichTestToRun(IDictionary<string, (Type TestType, string DescriptionOfTest)> availableTests)
        {
            do
            {
                Console.WriteLine("Please Enter The Test Id You Want To Run");
                Console.WriteLine("E - Exit");

                foreach (var testConfig in availableTests.OrderBy(x => x.Key))
                {
                    Console.WriteLine("{0} - {1}", testConfig.Key, testConfig.Value.DescriptionOfTest);
                }

                var rawTestIdToRun = Console.ReadLine();

                if (rawTestIdToRun.Equals("e", StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }
                else
                {
                    if (availableTests.TryGetValue(rawTestIdToRun, out var tryToGetTest))
                    {
                        return tryToGetTest.TestType;
                    }
                }

                Console.WriteLine(string.Empty);
                Console.WriteLine("Invalid Command");
                Console.WriteLine(string.Empty);

            } while (true);
        }

        private static IDictionary<string, (Type TestType, string DescriptionOfTest)> AvailableTests()
        {
            var lookup = new Dictionary<string, (Type TestType, string DescriptionOfTest)>(StringComparer.InvariantCultureIgnoreCase);

            foreach (var testType in typeof(Program).Assembly.GetTypes().Where(x => !x.IsInterface && typeof(IPerformanceTest).IsAssignableFrom(x)))
            {
                var instanceOfTest = (IPerformanceTest)Activator.CreateInstance(testType);

                lookup.Add(instanceOfTest.Command, (testType, instanceOfTest.Description));
            }

            return lookup;
        }

    }
}
