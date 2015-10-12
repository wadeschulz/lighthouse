using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lighthouse.Web.Startup))]
namespace Lighthouse.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
