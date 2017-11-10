namespace PolicyInjectionSample.Framework.Lib.PolicyInjection
{
    using System.Collections.Generic;

    public interface IPolicy
    {
        IEnumerable<IPolicyEvent> Events();
        IEnumerable<IPolicyEvent> EventsBy(ExecuteTiming timing);
        IEnumerable<IPolicyEvent> EventsBy(ExecuteTiming[] timings);
    }

    /// <summary>
    /// Policyのベースクラス。
    /// </summary>
    public abstract class PolicyBase : IPolicy
    {
        abstract public IEnumerable<IPolicyEvent> Events();

        /// <summary>
        /// ハンドラの実行タイミングを受け取り、実行可能なハンドラを持つイベントのリストを取得する
        /// </summary>
        /// <param name="timing">現時点で実行可能なイベントのリスト</param>
        /// <returns></returns>
        public IEnumerable<IPolicyEvent> EventsBy(ExecuteTiming timing)
        {
            foreach (var evt in Events())
            {
                if (evt.GetExecuteTiming().HasFlag(timing)) yield return evt;
            };
        }

        /// <summary>
        /// 複数のハンドラ実行タイミングを受け取り、実行可能なハンドラを持つイベントのリストを取得する
        /// </summary>
        /// <param name="timings">実行タイミングの配列</param>
        /// <returns>現時点で実行可能なイベントのリスト</returns>
        public IEnumerable<IPolicyEvent> EventsBy(ExecuteTiming[] timings)
        {
            var policyEvents = new List<IPolicyEvent>();

            foreach (var timing in timings)
            {
                policyEvents.AddRange(EventsBy(timing));
            }

            return policyEvents;
        }
    }
}
