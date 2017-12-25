using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiAttack.Core
{
    /// <summary>
    /// 访问策略(只提供访问处理等级，不做具体处理)
    /// </summary>
    public interface IAccessStrategy
    {
        /// <summary>
        /// 策略执行, 返回 true 表示 策略生效，结果赋值给   <see cref="LimitResult"/>  context.Result，后续策略继续无需校验;返回 false 表示 策略未生效, 继续后续策略。
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns> </returns>
        bool Attempt(VisitorContext context);
    }
}
