namespace PolicyInjectionSample.Framework.Lib.PolicyInjection
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Runtime.Remoting.Proxies;
    using System.Runtime.Remoting.Messaging;
    using System.Reflection;

    public class PolicyInjection
    {
        public PolicyInjection(Assembly attributeResolveTarget)
        {
            AttributeResolveTarget = attributeResolveTarget;
        }

        /// <summary>
        /// PolicyAttributeの探索範囲
        /// </summary>
        public Assembly AttributeResolveTarget { get; private set; }

        /// <summary>
        /// 与えられたポリシーを対象クラスに注入する。
        /// </summary>
        /// <typeparam name="T">対象クラス</typeparam>
        /// <param name="applyPolicies">注入するポリシーのリスト</param>
        /// <returns>透過プロキシ</returns>
        public T InjectByTypeList<T>(List<Type> applyPolicies)
        {
            return (T)new ApplyPolicyProxy<T>(applyPolicies).GetTransparentProxy();
        }

        /// <summary>
        /// カスタムアトリビュートによって与えられたポリシーを対象クラスに注入する。
        /// </summary>
        /// <typeparam name="T">対象クラス</typeparam>
        /// <returns>透過プロキシ</returns>
        public T InjectByAttribute<T>()
        {
            return (T) InjectByTypeList<T>(ApplyPoliciesListByAttribute(typeof(T)).ToList());
        }

        /// <summary>
        /// 対象クラスに付与されたApplyPolicyアトリビュートを列挙する。
        /// </summary>
        /// <param name="applyTargetClass">対象クラス</param>
        /// <returns>ApplyPolicyアトリビュートのリスト</returns>
        protected IEnumerable<Type> ApplyPoliciesListByAttribute(Type applyTargetClass)
        {
            var attrs = applyTargetClass.GetCustomAttributes(false);
            foreach (var atr in attrs)
            {
                if (atr.GetType() == typeof(ApplyPolicyAttribute))
                    yield return (atr as ApplyPolicyAttribute).PolicyType;

            }
        }
    }

    /// <summary>
    /// ポリシーを適用するためのプロキシ。
    /// </summary>
    /// <typeparam name="T">プロキシが内包するクラスの型</typeparam>
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
