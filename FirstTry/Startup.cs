using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FirstTry.Startup))]
namespace FirstTry
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
