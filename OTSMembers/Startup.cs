using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OTSMembers.Startup))]
namespace OTSMembers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
