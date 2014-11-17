using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

//[assembly: OwinStartup(typeof(React.Lab.StartupWithWebApi))]

namespace React.Lab
{
    public class StartupWithWebApi
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseFileServer(new FileServerOptions() { FileSystem = new PhysicalFileSystem("public") });

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);
        }
    }
}
