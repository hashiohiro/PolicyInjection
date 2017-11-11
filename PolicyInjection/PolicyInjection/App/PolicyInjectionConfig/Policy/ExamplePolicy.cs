namespace PolicyInjectionSample.App.PolicyInjectionConfig.Policy
{
    using System.Collections.Generic;
    using PolicyInjectionSample.Framework.Lib.PolicyInjection;
    using PolicyInjectionSample.App.PolicyInjectionConfig.Event;

    public class ExamplePolicy : PolicyBase
    {
        /// <summary>
        /// ポリシーに紐づけるイベントを列挙する。
        /// </summary>
        /// <remarks>
        /// BindPolicyEventアトリビュートを使わず、
        /// イベントをバインドする場合はEvents()をオーバーライドする。
        /// </remarks>
        /// <returns>イベントインスタンスのリスト</returns>
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
