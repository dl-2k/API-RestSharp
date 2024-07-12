using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.API;
using Core.Configuration;
using Core.Reports;
using Core.ShareData;
using NUnit.Framework.Interfaces;

namespace DemoQA.Test.TestCases
{
    [TestFixture]
    public class BaseTest
    {

        protected static APIClient ApiClient;

        public BaseTest()
        {
            ApiClient = new APIClient(ConfigurationHelper.GetValueByKey("application:url"));
            ExtentTestManager.CreateParentTest(TestContext.CurrentContext.Test.ClassName);
        }

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Base Test set up");
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            DataStorage.ClearData(); 
            UpdateTestReport();      
            Console.WriteLine("Base Test tear down");
        }

        public void UpdateTestReport()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : TestContext.CurrentContext.Result.StackTrace;
            var message = TestContext.CurrentContext.Result.Message;

            switch (status)
            {
                case TestStatus.Failed:
                    ReportLog.Fail($"Test failed with message: {message}");
                    ReportLog.Fail($"Stacktrace: {stacktrace}");
                    break;
                case TestStatus.Inconclusive:
                    ReportLog.Skip($"Test inconclusive with message: {message}");
                    ReportLog.Skip($"Stacktrace: {stacktrace}");
                    break;
                case TestStatus.Skipped:
                    ReportLog.Skip($"Test skipped with message: {message}");
                    break;
                default:
                    ReportLog.Pass("Test passed");
                    break;
            }
    }
}
}