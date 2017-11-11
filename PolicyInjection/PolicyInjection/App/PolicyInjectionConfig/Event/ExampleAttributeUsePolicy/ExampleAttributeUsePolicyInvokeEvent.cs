namespace PolicyInjectionSample.App.PolicyInjectionConfig.Event
{
    using System;
    using PolicyInjectionSample.Framework.Lib.PolicyInjection;

    public class ExampleAttributeUsePolicyInvokeEvent : IPolicyEvent
    {
        public ExecuteTiming GetExecuteTiming()
        {
            return ExecuteTiming.Invoking | ExecuteTiming.Invoked;
        }

        // TODO: 今はArgsが常にNullになる。ちゃんとArgsに値が来るようにする。
        public void Handler(object sender, EventArgs args)
        {
            Console.WriteLine("ExampleAttributeUsePolicyInvokeEvent executed. invoking or invoking:");
        }
    }
}
