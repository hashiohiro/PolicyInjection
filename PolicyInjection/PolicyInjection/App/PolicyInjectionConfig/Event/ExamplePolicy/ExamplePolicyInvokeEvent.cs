namespace PolicyInjectionSample.App.PolicyInjectionConfig.Event
{
    using System;
    using PolicyInjectionSample.Framework.Lib.PolicyInjection;

    public class ExamplePolicyInvokeEvent : IPolicyEvent
    {
        public ExecuteTiming GetExecuteTiming()
        {
            return ExecuteTiming.Invoking | ExecuteTiming.Invoked;
        }

        // TODO: 今はArgsが常にNullになる。ちゃんとArgsに値が来るようにする。
        public void Handler(object sender, EventArgs args)
        {
            Console.WriteLine("ExamplePolicyEvent executed. invoking or invoked:");
        }
    }
}
