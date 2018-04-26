using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LeadManagementSystem.Startup))]
namespace LeadManagementSystem
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
