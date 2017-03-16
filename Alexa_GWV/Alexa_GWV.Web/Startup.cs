using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Alexa_GWV.Web.Startup))]
namespace Alexa_GWV.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
