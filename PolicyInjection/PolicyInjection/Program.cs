namespace PolicyInjectionSample
{
    using System;
    using System.Collections.Generic;
    using ReportingTool.App.PolicyInjectionConfig.Policy;
    using ReportingTool.Framework.Lib.PolicyInjection;

    class Program
    {
        static void Main(string[] args)
        {
            var injection = new PolicyInjection();
            var applyPolicies = new List<Type>()
            {
                typeof(ExamplePolicy)
            };

            var proxy = (ApplyTargetClass)injection.GetProxy<ApplyTargetClass>(applyPolicies);
            proxy.TestMethod1();
        }
    }

    class ApplyTargetClass : MarshalByRefObject
    {
        public void TestMethod1()
        {
            Console.WriteLine("TestMethod1 called");
        }
    }
}
