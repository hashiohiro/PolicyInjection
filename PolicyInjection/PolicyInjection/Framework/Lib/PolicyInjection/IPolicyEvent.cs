namespace PolicyInjectionSample.Framework.Lib.PolicyInjection
{
    using System;

    public interface IPolicyEvent
    {
        ExecuteTiming GetExecuteTiming();
        void Handler(object sender, EventArgs args);
    }

    public enum ExecuteTiming
    {
        Nope = 0,
        Invoking = 1,
        Invoked = 1 << 2,
        All = 1 << 3,
    }
}
