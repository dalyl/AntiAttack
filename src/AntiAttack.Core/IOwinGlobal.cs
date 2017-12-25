using Microsoft.Owin.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiAttack.Core
{

    /// <summary>
    /// 全局设置
    /// </summary>
    public interface IOwinGlobal
    {
        /// <summary>
        /// 部署域名
        /// </summary>
        string WebSite { get; }

        /// <summary>
        /// cookie 管理
        /// </summary>
        ICookieManager CookieProvider { get; }

        /// <summary>
        /// 脱敏工具
        /// </summary>
        /// IDataSecure DesensitizeProvider { get; }
    }
}
