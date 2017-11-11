namespace PolicyInjectionSample.Framework.Lib.PolicyInjection
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BindPolicyEventAttribute : Attribute
    {
        public BindPolicyEventAttribute(Type eventType, ExecuteTiming executeTiming)
        {
            EventType = eventType;
            ExecuteTiming = ExecuteTiming;
        }

        public Type EventType { get; private set; }

        public ExecuteTiming ExecuteTiming { get; private set; }

    }
}
