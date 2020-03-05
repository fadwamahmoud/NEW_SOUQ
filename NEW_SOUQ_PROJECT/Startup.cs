using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NEW_SOUQ_PROJECT.Startup))]
namespace NEW_SOUQ_PROJECT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
