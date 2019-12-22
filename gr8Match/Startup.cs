using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(gr8Match.Startup))]
namespace gr8Match
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
