using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyShop.WebUI.StartupOwin))]

namespace MyShop.WebUI
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
