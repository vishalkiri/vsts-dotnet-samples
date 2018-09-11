using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using Microsoft.VisualStudio.Services.WebApi;

namespace Microsoft.TeamServices.Samples.Client.Test
{
    [ClientSample(TestPlanResourceIds.AreaName, TestPlanResourceIds.TestSuiteByCaseString)]
    public class TestSuiteByCaseSample : ClientSample
    {
        [ClientSampleMethod]
        public List<TestSuite> GetTestSuitesByCase()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            // Get Test Suites
            List<TestSuite> testSuites = testPlanClient.GetSuitesByTestCaseIdAsync(6).Result;

            foreach (TestSuite testSuite in testSuites)
            {
                Console.WriteLine("{0} {1}", testSuite.Id.ToString().PadLeft(6), testSuite.Name);
            }

            return testSuites;
        }
    }
}
