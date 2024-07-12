using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AventStack.ExtentReports;

namespace Core.Reports
{
    public class ExtentTestManager
    {
        private static AsyncLocal<ExtentTest> _parentTest = new AsyncLocal<ExtentTest>();
        private static AsyncLocal<ExtentTest> _childTest = new AsyncLocal<ExtentTest>();

        public static ExtentTest CreateParentTest(string testName, string description = null)
        {
            _parentTest.Value = ExtentReportManager.Instance.CreateTest(testName, description);
            return _parentTest.Value;
        }

        public static ExtentTest CreateTest(string testName, string description = null)
        {
            if (_parentTest.Value == null)
            {
                throw new InvalidOperationException("Parent test is not set. Ensure CreateParentTest is called before CreateTest.");
            }
            _childTest.Value = _parentTest.Value.CreateNode(testName, description);
            return _childTest.Value;
        }

        public static ExtentTest GetTest()
        {
            if (_childTest.Value == null)
            {
                throw new InvalidOperationException("Child test is not set. Ensure CreateTest is called before GetTest.");
            }
            return _childTest.Value;
        }

    }
}