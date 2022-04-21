using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BotAgentHubApp.Startup))]
namespace BotAgentHubApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
