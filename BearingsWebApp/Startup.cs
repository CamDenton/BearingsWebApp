using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BearingsWebApp.Startup))]
namespace BearingsWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
