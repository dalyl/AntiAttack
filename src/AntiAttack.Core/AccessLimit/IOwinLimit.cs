using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiAttack.Core
{

    /// <summary>
    /// 访问限制处理中间件
    /// </summary>
    public interface IOwinLimit
    {

        /// <summary>
        /// 解锁客户端
        /// </summary>
        /// <returns></returns>
        Task<bool> UnLockClient();

       
    }
}
