namespace PolicyInjectionSample.App.PolicyInjectionConfig.Policy
{
    using PolicyInjectionSample.Framework.Lib.PolicyInjection;
    using PolicyInjectionSample.App.PolicyInjectionConfig.Event;

    /// <summary>
    /// BindPolicyEventアトリビュートでポリシーが作用したときのイベントを設定する例。
    /// </summary>
    [BindPolicyEvent(typeof(ExampleAttributeUsePolicyAllEvent), ExecuteTiming.All)]
    [BindPolicyEvent(typeof(ExampleAttributeUsePolicyInvokeEvent), ExecuteTiming.Invoking | ExecuteTiming.Invoked)]
    [BindPolicyEvent(typeof(ExampleAttributeUsePolicyInvokingEvent), ExecuteTiming.Invoking)]
    [BindPolicyEvent(typeof(ExampleAttributeUsePolicyInvokedEvent), ExecuteTiming.Invoked)]
    public class ExampleAttributeUsePolicy : PolicyBase {}
}
