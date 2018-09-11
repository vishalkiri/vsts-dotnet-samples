using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using Microsoft.VisualStudio.Services.WebApi;

namespace Microsoft.TeamServices.Samples.Client.Test
{
    [ClientSample(TestPlanResourceIds.AreaName, TestPlanResourceIds.TestSuiteProjectLocationIdString)]
    public class TestSuiteSample : ClientSample
    {
        [ClientSampleMethod]
        public List<TestSuite> GetTestSuitesForPlan()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            // Get Test Suites
            List<TestSuite> testSuites = testPlanClient.GetTestSuitesForPlanAsync(projectName, 79, SuiteExpand.Children | SuiteExpand.DefaultTesters).Result;

            foreach (TestSuite testSuite in testSuites)
            {
                Console.WriteLine("{0} {1}", testSuite.Id.ToString().PadLeft(6), testSuite.Name);
            }

            return testSuites;
        }

        [ClientSampleMethod]
        public List<TestSuite> GetTestSuitesAsTreeView()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            // Get Test Suites
            List<TestSuite> testSuites = testPlanClient.GetTestSuitesForPlanAsync(projectName, 79, asTreeView: true).Result;

            return testSuites;
        }

        [ClientSampleMethod]
        public TestSuite GetTestSuiteById()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            // Get Test Suite
            TestSuite suite = testPlanClient.GetTestSuiteByIdAsync(projectName, 79, 83, SuiteExpand.Children).Result;

            Console.WriteLine("{0} {1}", suite.Id.ToString().PadLeft(6), suite.Name);

            return suite;
        }

        [ClientSampleMethod]
        public TestSuite CreateTestSuite()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            TestSuiteCreateParams testSuiteCreateParams = new TestSuiteCreateParams()
            {
                Name = "SubSuite 1.1.1",
                SuiteType = TestSuiteType.StaticTestSuite,
                ParentSuite = new TestSuiteReference()
                {
                    Id = 85
                },
                InheritDefaultConfigurations = false,
                DefaultConfigurations = new List<TestConfigurationReference>()
                {
                    new TestConfigurationReference(){Id=1},
                    new TestConfigurationReference(){Id=2}
                }
            };

            // Create Test Variable
            TestSuite suite = testPlanClient.CreateTestSuiteAsync(testSuiteCreateParams, projectName, 79).Result;

            Console.WriteLine("{0} {1}", suite.Id.ToString().PadLeft(6), suite.Name);

            return suite;
        }

        [ClientSampleMethod]
        public TestSuite UpdateTestSuiteParent()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();


            TestSuiteUpdateParams testSuiteUpdateParams = new TestSuiteUpdateParams()
            {
                ParentSuite = new TestSuiteReference()
                {
                    Id = 81
                }
            };

            TestSuite updtaetdTestSuite = testPlanClient.UpdateTestSuiteAsync(testSuiteUpdateParams, projectName, 79, 87).Result;

            Console.WriteLine("{0} {1}", updtaetdTestSuite.Id.ToString().PadLeft(6), updtaetdTestSuite.Name);

            return updtaetdTestSuite;
        }

        [ClientSampleMethod]
        public TestSuite UpdateTestSuiteProperties()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            TestSuiteUpdateParams testSuiteUpdateParams = new TestSuiteUpdateParams()
            {
                DefaultTesters = new List<IdentityRef>()
                {
                    new IdentityRef()
                    {
                        Id = "9b6bee0e-28b2-42b6-ab5b-5122b63d473c"
                    }
                }
            };

            TestSuite updtaetdTestSuite = testPlanClient.UpdateTestSuiteAsync(testSuiteUpdateParams, projectName, 79, 83).Result;

            Console.WriteLine("{0} {1}", updtaetdTestSuite.Id.ToString().PadLeft(6), updtaetdTestSuite.Name);

            return updtaetdTestSuite;
        }

        [ClientSampleMethod]
        public void DeleteTestSuite()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            //Delete the test variable first
            testPlanClient.DeleteTestSuiteAsync(projectName, 79, 86).SyncResult();
        }
    }
}
