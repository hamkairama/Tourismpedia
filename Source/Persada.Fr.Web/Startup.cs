using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Persada.Fr.Web.Startup))]
namespace Persada.Fr.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
