using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calendar_OneDrive_Mail_Graph.Utils
{
    public class OAuth2CodeRedeemerMiddleWare
    {
    }
    public sealed class OAuth2CodeRedeemerOptions
    {
        public string ClientId { get; set; }
        public string RedirectUri { get; set; }
        public string ClientSecret { get; set; }
    }

    internal static class OAuth2CodeRedeemerHandler
    {
        public static IAppBuilder UseOAuth2CodeRedeemer(this IAppBuilder app, OAuth2CodeRedeemerOptions options)
        {
            app.Use<OAuth2CodeRedeemerMiddleware>(options);
            return app;
        }
    }
}