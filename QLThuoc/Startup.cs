using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLThuoc.Startup))]
namespace QLThuoc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
