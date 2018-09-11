﻿using System;
using System.Collections.Generic;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using TestConfiguration = Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi.TestConfiguration;

namespace Microsoft.TeamServices.Samples.Client.Test
{
    [ClientSample(TestPlanResourceIds.AreaName, TestPlanResourceIds.TestConfigurationLocationIdString)]
    public class TestConfigurationSample : ClientSample
    {
        [ClientSampleMethod]
        public List<TestConfiguration> GetTestConfigurations()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            // Get Test configurations
            List<TestConfiguration> configurations = testPlanClient.GetTestConfigurationsAsync(projectName).Result;

            foreach (TestConfiguration configuration in configurations)
            {
                Console.WriteLine("{0} {1}", configuration.Id.ToString().PadLeft(6), configuration.Name);
            }

            return configurations;
        }

        [ClientSampleMethod]
        public TestConfiguration GetTestConfigurationById()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            // Get Test configurations
            TestConfiguration configuration = testPlanClient.GetTestConfigurationByIdAsync(projectName, 1).Result;

            Console.WriteLine("{0} {1}", configuration.Id.ToString().PadLeft(6), configuration.Name);

            return configuration;
        }

        [ClientSampleMethod]
        public TestConfiguration CreateTestConfiguration()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            TestConfigurationCreateUpdateParameters TestConfigurationCreateUpdateParameters = new TestConfigurationCreateUpdateParameters()
            {
                Name = "SampleTestConfiguration",
                Description = "Sample Test configuration",
                IsDefault = true,
                State = TeamFoundation.TestManagement.WebApi.TestConfigurationState.Active,
                Values = new List<NameValuePair>()
                {
                    new NameValuePair("Operating System", "Windows 8"),
                }
            };

            // Create Test configuration
            TestConfiguration configuration = testPlanClient.CreateTestConfigurationAsync(TestConfigurationCreateUpdateParameters, projectName).Result;

            Console.WriteLine("{0} {1}", configuration.Id.ToString().PadLeft(6), configuration.Name);

            return configuration;
        }

        [ClientSampleMethod]
        public TestConfiguration UpdateTestConfiguration()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            //Get the test configuration first
            TestConfiguration configuration = testPlanClient.GetTestConfigurationByIdAsync(projectName, 8).Result;

            TestConfigurationCreateUpdateParameters TestConfigurationCreateUpdateParameters = new TestConfigurationCreateUpdateParameters()
            {
                Name = configuration.Name,
                Description = "Updated Description",
                Values = configuration.Values
            };

            TestConfigurationCreateUpdateParameters.Values.Add(new NameValuePair("Browser", "Microsoft Edge"));

            // Update Test configuration
            TestConfiguration updatedconfiguration = testPlanClient.UpdateTestConfigurationAsync(TestConfigurationCreateUpdateParameters, projectName, configuration.Id).Result;

            Console.WriteLine("{0} {1}", updatedconfiguration.Id.ToString().PadLeft(6), updatedconfiguration.Name);

            return configuration;
        }

        [ClientSampleMethod]
        public void DeleteTestConfiguration()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            //Delete the test configuration 
            testPlanClient.DeleteTestConfgurationAsync(projectName, 8).SyncResult();
        }
    }
}
