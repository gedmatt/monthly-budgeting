using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Budgeting.Web.Startup))]
namespace Budgeting.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
