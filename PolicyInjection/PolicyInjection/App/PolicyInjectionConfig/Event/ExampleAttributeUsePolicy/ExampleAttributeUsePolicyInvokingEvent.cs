namespace PolicyInjectionSample.App.PolicyInjectionConfig.Event
{
    using System;
    using PolicyInjectionSample.Framework.Lib.PolicyInjection;

    public class ExampleAttributeUsePolicyInvokingEvent : IPolicyEvent
    {
        public ExecuteTiming GetExecuteTiming()
        {
            return ExecuteTiming.Invoking;
        }

        // TODO: 今はArgsが常にNullになる。ちゃんとArgsに値が来るようにする。
        public void Handler(object sender, EventArgs args)
        {
            Console.WriteLine("ExampleAttributeUsePolicyEvent executed. invoking:");
        }
    }
}
