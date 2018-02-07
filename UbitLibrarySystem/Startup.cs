using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UbitLibrarySystem.Startup))]
namespace UbitLibrarySystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
