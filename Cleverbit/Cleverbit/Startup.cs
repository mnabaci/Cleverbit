using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cleverbit.Startup))]
namespace Cleverbit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
