namespace PolicyInjectionSample.Framework.Lib.PolicyInjection
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ApplyPolicyAttribute : Attribute
    {
        public ApplyPolicyAttribute(Type policyType)
        {
            PolicyType = policyType;
        }

        public Type PolicyType { get; private set; }
    }
}
