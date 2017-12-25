using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using System.Threading;

namespace AntiAttack.Core
{
    /// <summary>
    /// 排除策略 一般放在第一策略
    /// </summary>
    public class PrecludeStrategy : IAccessStrategy
    {
        /// <summary>
        /// 排除预期结果
        /// </summary>
        public bool Attempt(VisitorContext context)
        {
            // 只监测 Get 方法
            if (context.Method.Contains("GET")==false) return true;

            if (context.Judgement == null) return false;
            if (context.Judgement.ContainedInExcluded == null) return false;
            if (context.Judgement.ContainedInExcluded()) return context.ChangeResult(true, LimitLevel.None);

            return false;
        }
    }
}
