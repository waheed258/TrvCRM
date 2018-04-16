using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TCRM.Startup))]
namespace TCRM
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
