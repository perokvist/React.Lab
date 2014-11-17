using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Owin;
using Owin.Routing;

namespace React.Lab
{
    public static class ExpressExtensions
    {
        public static IAppBuilder Get(this IAppBuilder app, string url, Action<IOwinRequest, IOwinResponse> action)
        {
            return app.Get(url, ctx =>
            {
                action(ctx.Request, ctx.Response);
                return Task.FromResult(0);
            });
        }

        public static IAppBuilder Post(this IAppBuilder app, string url, Action<IOwinRequest, IOwinResponse> action)
        {
            return app.Post(url, ctx =>
            {
                action(ctx.Request, ctx.Response);
                return Task.FromResult(0);
            });
        }


        public static JToken FormToJson(this IOwinRequest req)
        {
            var r = req.ReadFormAsync().Result;
            var json = JsonConvert.SerializeObject(r.ToDictionary(pair => pair.Key, pair => pair.Value));
            return JToken.Parse(json);
        }
    }
}