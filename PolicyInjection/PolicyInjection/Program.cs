namespace PolicyInjectionSample
{
    using System;
    using System.Collections.Generic;
    using PolicyInjectionSample.App.PolicyInjectionConfig.Policy;
    using PolicyInjectionSample.Framework.Lib.PolicyInjection;
    using System.Reflection;

    public class Program
    {
        static void Main(string[] args)
        {
            var injection = new PolicyInjection(Assembly.GetExecutingAssembly());
            var applyPolicies = new List<Type>()
            {
                typeof(ExamplePolicy)
            };

            // 適用したいポリシーをハードコードで指定する場合
            var proxy = injection.InjectByTypeList<ApplyTargetClass>(applyPolicies);

            // 適用したいポリシーをApplyPolicyアトリビュートで指定する場合
            var proxy2 = injection.InjectByAttribute<ApplyTargetAttributeVersionClass>();

            proxy.TestMethod1();
            proxy2.TestMethod2();
        }
    }

    public class ApplyTargetClass : MarshalByRefObject
    {
        public void TestMethod1()
        {
            Console.WriteLine("TestMethod1 called");
        }
    }

    [ApplyPolicy(typeof(ExampleAttributeUsePolicy))]
    public class ApplyTargetAttributeVersionClass : MarshalByRefObject
    {
        public void TestMethod2()
        {
            Console.WriteLine("TestMethod2 called");
        }
    }
}
