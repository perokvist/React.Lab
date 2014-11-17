using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json.Linq;
using Owin;

[assembly: OwinStartup(typeof(React.Lab.Startup))]

namespace React.Lab
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseFileServer(new FileServerOptions() { FileSystem = new PhysicalFileSystem("public") });
            var comments = JArray.Parse(File.ReadAllText("_comments.json"));

            app.Map("/comments.json",
                builder => builder.Run(ctx =>
                {
                    if (ctx.Request.Method == "POST")
                        comments.Add(ctx.Request.FormToJson());
                    ctx.Response.Headers.Add("Content-Type", new[] { "application/json" });
                    ctx.Response.Write(comments.ToString());
                    return Task.FromResult(0);
                }));
        }
    }
}
