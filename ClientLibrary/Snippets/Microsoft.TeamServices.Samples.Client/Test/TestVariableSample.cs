using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Services.TestManagement.TestPlanning.WebApi;
using Microsoft.VisualStudio.Services.WebApi;

namespace Microsoft.TeamServices.Samples.Client.Test
{
    [ClientSample(TestPlanResourceIds.AreaName, TestPlanResourceIds.TestVariableLocationIdString)]
    public class TestVariableSample : ClientSample
    {
        [ClientSampleMethod]
        public List<TestVariable> GetTestVariables()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            // Get Test Variables
            List<TestVariable> variables = testPlanClient.GetTestVariablesAsync(projectName).Result;

            foreach (TestVariable variable in variables)
            {
                Console.WriteLine("{0} {1}", variable.Id.ToString().PadLeft(6), variable.Name);
            }

            return variables;
        }

        [ClientSampleMethod]
        public TestVariable GetTestVariableById()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            // Get Test Variables
            TestVariable variable = testPlanClient.GetTestVariableByIdAsync(projectName, 2).Result;

            Console.WriteLine("{0} {1}", variable.Id.ToString().PadLeft(6), variable.Name);

            return variable;
        }

        [ClientSampleMethod]
        public TestVariable CreateTestVariable()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            TestVariableCreateUpdateParameters testVariableCreateUpdateParameters = new TestVariableCreateUpdateParameters()
            {
                Name = "SampleTestVariable",
                Description = "Sample Test Variable",
                Values = new List<string>()
                {
                    "Test Value 1",
                    "Test Value 2"
                }
            };

            // Create Test Variable
            TestVariable variable = testPlanClient.CreateTestVariableAsync(testVariableCreateUpdateParameters, projectName).Result;

            Console.WriteLine("{0} {1}", variable.Id.ToString().PadLeft(6), variable.Name);

            return variable;
        }

        [ClientSampleMethod]
        public TestVariable UpdateTestVariable()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            //Get the test variable first
            TestVariable variable = testPlanClient.GetTestVariableByIdAsync(projectName, 12).Result;

            TestVariableCreateUpdateParameters testVariableCreateUpdateParameters = new TestVariableCreateUpdateParameters()
            {
                Name = variable.Name,
                Description = "Updated Description",
                Values = variable.Values
            };

            testVariableCreateUpdateParameters.Values.Add("New Value");
            
            // Update Test Variable
            TestVariable updatedVariable = testPlanClient.UpdateTestVariableAsync(testVariableCreateUpdateParameters, projectName, variable.Id).Result;

            Console.WriteLine("{0} {1}", updatedVariable.Id.ToString().PadLeft(6), updatedVariable.Name);

            return variable;
        }

        [ClientSampleMethod]
        public void DeleteTestVariable()
        {
            string projectName = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            // Get a testplan client instance
            VssConnection connection = Context.Connection;
            TestPlanHttpClient testPlanClient = connection.GetClient<TestPlanHttpClient>();

            //Delete the test variable first
            testPlanClient.DeleteTestVariableAsync(projectName, 12).SyncResult();
        }
    }
}
