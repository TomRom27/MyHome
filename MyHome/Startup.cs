using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyHome.Startup))]
namespace MyHome
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
