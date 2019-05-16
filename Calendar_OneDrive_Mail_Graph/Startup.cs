using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Calendar_OneDrive_Mail_Graph.Startup))]

namespace Calendar_OneDrive_Mail_Graph
{
    partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
