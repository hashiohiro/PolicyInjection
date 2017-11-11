namespace PolicyInjectionSample.App.PolicyInjectionConfig.Event
{
    using System;
    using PolicyInjectionSample.Framework.Lib.PolicyInjection;

    public class ExampleAttributeUsePolicyAllEvent : IPolicyEvent
    {
        public ExecuteTiming GetExecuteTiming()
        {
            return ExecuteTiming.All;
        }

        // TODO: 今はArgsが常にNullになる。ちゃんとArgsに値が来るようにする。
        public void Handler(object sender, EventArgs args)
        {
            Console.WriteLine("ExampleAttributeUsePolicyEvent executed. all:");
        }
    }
}
