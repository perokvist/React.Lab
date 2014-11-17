using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNet.Builder;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNet.StaticFiles;
using Microsoft.AspNet.FileSystems;
using Microsoft.AspNet.Http;

namespace ReactStart
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
             app.UseFileServer(new FileServerOptions() {EnableDefaultFiles = true });

            var comments = JArray.Parse(File.ReadAllText("_comments.json"));

            app.Map("/comments.json",
                builder => builder.Run(ctx =>
                {
                    if (ctx.Request.Method == "POST")
                        comments.Add(ctx.Request.FormToJson());
                    ctx.Response.Headers.Add("Content-Type", new[] { "application/json" });
                    ctx.Response.WriteAsync(comments.ToString());
                    return Task.FromResult(0);
                }));
        }
    }

    public static class Extensions {
        public static JToken FormToJson(this HttpRequest req)
        {
            var r = req.GetFormAsync().Result;
            var json = JsonConvert.SerializeObject(r.ToDictionary(pair => pair.Key, pair => pair.Value));
            return JToken.Parse(json);
        }
    }
}
