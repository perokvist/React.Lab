using System.IO;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json.Linq;
using Owin;

//[assembly: OwinStartup(typeof(React.Lab.StartupExpressLike))]

namespace React.Lab
{
    public class StartupExpressLike
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseFileServer(new FileServerOptions() { FileSystem = new PhysicalFileSystem("public") });
            var comments = JArray.Parse(File.ReadAllText("_comments.json"));

            app.Get("comments.json", (req, res) =>
                        {
                            res.Headers.Add("Content-Type", new[] { "application/json" });
                            res.Write(comments.ToString());
                        });

            app.Post("comments.json", (req, res) =>
            {
                comments.Add(req.FormToJson());
                res.Headers.Add("Content-Type", new[] { "application/json" });
                res.Write(comments.ToString());
            });
        }
    }
}
