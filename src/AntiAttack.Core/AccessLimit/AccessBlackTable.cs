using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiAttack.Core
{

    /// <summary>
    /// 黑名单校验
    /// </summary>
    public class AccessBlackTable : IAccessStrategy
    {

        /// <summary>
        /// 黑名单校验
        /// </summary>
        /// <returns></returns>
        public bool Attempt(VisitorContext context)
        {
            if (context.Judgement == null) return false;
            if (context.Judgement.ContainedInBlack == null) return false;
            if (context.Judgement.ContainedInBlack()) return context.ChangeResult(true, LimitLevel.All);
            return false;
        }



    }

}
