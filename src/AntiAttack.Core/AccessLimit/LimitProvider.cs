using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiAttack.Core
{
    /// <summary>
    /// 历史记录信息上下文
    /// </summary>
    public class LimitProvider
    {
        /// <summary>
        /// 被包含于白名单
        /// </summary>
        public Func<bool> ContainedInWhite { get; set; }

        /// <summary>
        /// 被包含于白名单
        /// </summary>
        public Func<bool> ContainedInBlack { get; set; }

        /// <summary>
        /// 监控层级
        /// </summary>
        public Func<int> GradeInMonitoring { get; set; }

        /// <summary>
        /// 被包含于排除名单
        /// </summary>
        public Func<bool> ContainedInExcluded { get; set; }

    }
}
