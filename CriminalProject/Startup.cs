using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CriminalProject.Startup))]
namespace CriminalProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
