using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GetCats.Startup))]
namespace GetCats
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
