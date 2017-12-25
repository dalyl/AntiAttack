using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiAttack.Core
{
    /// <summary>
    /// 中间件传入参数
    /// </summary>
    public class OwinAccessLimitOptions
    {
        /// <summary>
        /// 访问者临时令牌
        /// </summary>
        public string VisitorTokenKey { get;  set; }
    
        /// <summary>
        /// 提供限制服务
        /// </summary>
        public string LimitAddress { get; set; }

        /// <summary>
        /// 访问记录
        /// </summary>
        public Action<VisitorContext> WriteRecord { get; set; }

        /// <summary>
        ///  读取历史记录
        /// </summary>
        public Func<VisitorContext, LimitProvider> ReadRecord { get; set; }

        /// <summary>
        /// 策略集合
        /// </summary>
        public List<IAccessStrategy> AccessStrategies { get; set; } = new List<IAccessStrategy>();

    }
}
