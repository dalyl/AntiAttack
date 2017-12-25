using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiAttack.Core
{
    /// <summary>
    /// 白名单校验
    /// </summary>
    public class AccessWhiteTable : IAccessStrategy
    {
        /// <summary>
        /// 执行
        /// </summary>
        public bool Attempt(VisitorContext context)
        {
            if (context.Judgement == null) return false;
            if (context.Judgement.ContainedInWhite == null) return false;
            if (context.Judgement.ContainedInWhite()) return context.ChangeResult(true, LimitLevel.None);
            return false;
        }

    }
}
