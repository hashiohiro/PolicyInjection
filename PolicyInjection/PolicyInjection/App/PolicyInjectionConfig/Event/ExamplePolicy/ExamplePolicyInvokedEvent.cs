﻿namespace ReportingTool.App.PolicyInjectionConfig.Event
{
    using System;
    using ReportingTool.Framework.Lib.PolicyInjection;

    public class ExamplePolicyInvokedEvent : IPolicyEvent
    {
        public ExecuteTiming GetExecuteTiming()
        {
            return ExecuteTiming.Invoked;
        }

        // TODO: 今はArgsが常にNullになる。ちゃんとArgsに値が来るようにする。
        public void Handler(object sender, EventArgs args)
        {
            Console.WriteLine("ExamplePolicyEvent executed. invoked:");
        }
    }
}