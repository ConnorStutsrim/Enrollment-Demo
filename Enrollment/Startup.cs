using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Enrollment.Startup))]
namespace Enrollment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
