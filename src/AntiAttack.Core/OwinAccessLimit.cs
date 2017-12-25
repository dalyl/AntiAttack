using Microsoft.Owin;
using Microsoft.Owin.Infrastructure;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AntiAttack.Core
{

    /// <summary>
    /// 访问限制中间件
    /// </summary>
    public class OwinAccessLimit : OwinMiddleware
    {
        /// <summary>
        /// 提供请求服务
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// 传入参数
        /// </summary>
        public OwinAccessLimitOptions Options { get; set; }

        /// <summary>
        /// 全局设置
        /// </summary>
        public IOwinGlobal Global { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next">中间件链对象</param>
        /// <param name="options">传入参数</param>
        public OwinAccessLimit(OwinMiddleware next, OwinAccessLimitOptions options) : base(next)
        {
            Options = options;
          //  Global = DependencyResolver.Current.GetService<IOwinGlobal>();
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(60);
            _httpClient.MaxResponseContentBufferSize = 1024 * 1024 * 10; // 10 MB
        }

        /// <summary>
        /// 中间件执行
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        public override async Task Invoke(IOwinContext context)
        {
            var token = Global.CookieProvider.GetRequestCookie(context, Options.VisitorTokenKey);

            var Visitor = new VisitorContext(context, Global.WebSite, token);

            if (Visitor.IsStatices())
            {
                await Next.Invoke(context);
                return;
            }

            await LimitValidAsync(Visitor);

            await InvokeAsync(context, Visitor);
        }

        /// <summary>
        /// 限制访问校验
        /// </summary>
        async Task LimitValidAsync(VisitorContext visitor)
        {
            Options.WriteRecord?.Invoke(visitor);
            visitor.Judgement = Options.ReadRecord(visitor);
            foreach (var strategy in Options.AccessStrategies)
            {
                var effected = strategy.Attempt(visitor);
                if (effected) break;
            }
        }

        /// <summary>
        /// 限制请求处理
        /// </summary>
        async Task InvokeAsync(IOwinContext context, VisitorContext visitor)
        {
            switch (visitor.Result)
            {
                case LimitLevel.All:; context.Response.StatusCode = 403; return;
                case LimitLevel.None: await Next.Invoke(context); return;
            }

            if (string.IsNullOrEmpty(Options.LimitAddress))
            {
                context.Response.StatusCode = 404;
                // await Next.Invoke(context);
                return;
            }

            var url = $"{Options.LimitAddress}?ul={Global.WebSite}{visitor.Link}";
            context.Response.Redirect(url);
        }
    }


}
