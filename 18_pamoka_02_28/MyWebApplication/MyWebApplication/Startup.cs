using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyWebApplication.Startup))]
namespace MyWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
