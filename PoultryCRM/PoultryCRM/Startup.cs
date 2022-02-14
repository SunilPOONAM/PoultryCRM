using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PoultryCRM.Startup))]
namespace PoultryCRM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
