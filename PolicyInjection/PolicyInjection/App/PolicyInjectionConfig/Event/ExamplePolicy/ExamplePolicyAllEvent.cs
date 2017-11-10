namespace ReportingTool.App.PolicyInjectionConfig.Event
{
    using System;
    using ReportingTool.Framework.Lib.PolicyInjection;

    public class ExamplePolicyAllEvent : IPolicyEvent
    {
        public ExecuteTiming GetExecuteTiming()
        {
            return ExecuteTiming.All;
        }

        // TODO: 今はArgsが常にNullになる。ちゃんとArgsに値が来るようにする。
        public void Handler(object sender, EventArgs args)
        {
            Console.WriteLine("ExamplePolicyEvent executed. all:");
        }
    }
}
