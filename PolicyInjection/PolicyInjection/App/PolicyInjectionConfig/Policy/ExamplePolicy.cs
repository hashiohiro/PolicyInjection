namespace PolicyInjectionSample.App.PolicyInjectionConfig.Policy
{
    using System.Collections.Generic;
    using PolicyInjectionSample.Framework.Lib.PolicyInjection;
    using PolicyInjectionSample.App.PolicyInjectionConfig.Event;

    public class ExamplePolicy : PolicyBase
    {
        /// <summary>
        /// ExamplePolicyに紐づけるPolicyEventのインスタンスを列挙する。
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<IPolicyEvent> Events()
        {
            return new List<IPolicyEvent>()
            {
                new ExamplePolicyAllEvent(),
                new ExamplePolicyInvokedEvent(),
                new ExamplePolicyInvokingEvent(),
                new ExamplePolicyInvokeEvent(),
            };
        }
    }
}
