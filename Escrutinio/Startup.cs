using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Escrutinio.Startup))]
namespace Escrutinio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
