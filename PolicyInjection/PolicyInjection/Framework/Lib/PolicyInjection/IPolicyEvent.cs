using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingTool.Framework.Lib.PolicyInjection
{
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
