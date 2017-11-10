namespace ReportingTool.Framework.Lib.PolicyInjection
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Runtime.Remoting.Proxies;
    using System.Runtime.Remoting.Messaging;
    using System.Reflection;

    public class PolicyInjection
    {
        public object GetProxy<T>(List<Type> applyPolicies)
        {
            return new ApplyPolicyProxy<T>(applyPolicies).GetTransparentProxy();
        }
    }

    /// <summary>
    /// ポリシーを適用するためのプロキシ。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApplyPolicyProxy<T> : RealProxy
    {
        public ApplyPolicyProxy(List<Type> applyPolicies)
            :base(typeof(T))
        {
            if (applyPolicies.Any(policy => policy.GetInterface(nameof(IPolicy)) == null))
                throw new ArgumentException("applyPolicies invalid type");

            RealClassType = typeof(T);
            RealInstance = Activator.CreateInstance(RealClassType);
            ApplyPolicies = applyPolicies.Select(policy => (IPolicy)Activator.CreateInstance(policy)).ToList();
        }

        /// <summary>
        /// ポリシーを適用する対象クラス
        /// </summary>
        public object RealInstance { get; private set; }

        public Type RealClassType { get; private set; }

        /// <summary>
        /// 適用するポリシーリスト
        /// </summary>
        public List<IPolicy> ApplyPolicies{ get; private set; }

        /// <summary>
        /// 対象クラスのメソッド呼び出し時に、ポリシーイベントハンドラの処理を注入する。
        /// </summary>
        /// <param name="msg">メソッド呼び出しのメッセージ</param>
        /// <returns>呼び出し元へ返却するメッセージ</returns>
        public override IMessage Invoke(IMessage msg)
        {
            var message = msg as IMethodMessage;
            var method = (MethodInfo)message.MethodBase;
            var args = message.Args;
            
            ExecuteHandler(
                RealClassType,
                message,
                ExecuteTiming.All,
                ExecuteTiming.Invoking);

            var ret = method.Invoke(RealInstance, args);

            ExecuteHandler(
                RealClassType,
                message,
                ExecuteTiming.All,
                ExecuteTiming.Invoked);
            return new ReturnMessage(ret, null, 0, message.LogicalCallContext, (IMethodCallMessage)msg);

        }

        /// <summary>
        /// ポリシーイベントハンドラを実行する。
        /// </summary>
        /// <param name="applyTargetClass">ハンドラ絞り込みオプション(ポリシーの適用対象クラス)</param>
        /// <param name="msg">メソッド呼び出し時のメッセージ</param>
        /// <param name="timings">ハンドラ絞り込みオプション(実行タイミング)</param>
        public void ExecuteHandler(Type applyTargetClass, IMethodMessage msg, params ExecuteTiming[] timings)
        {
            var events = ApplyPolicies.SelectMany(policy => policy.EventsBy(timings));

            foreach (var evt in events)
            {
                evt.Handler(applyTargetClass, null);
            }
        }
    }
}
