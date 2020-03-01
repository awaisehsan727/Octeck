using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OcTeck.Startup))]
namespace OcTeck
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
