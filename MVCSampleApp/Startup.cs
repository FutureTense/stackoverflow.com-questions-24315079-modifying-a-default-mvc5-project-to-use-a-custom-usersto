using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCSampleApp.Startup))]
namespace MVCSampleApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
