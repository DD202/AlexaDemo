using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexa_GWV.Web.Providers
{
    class BodyContentOAuthBearerProvider : OAuthBearerAuthenticationProvider
    {
        readonly string _name;
        public BodyContentOAuthBearerProvider(string name)
        {
            _name = name;
        }

        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            var request = context.Request;
            var requestBodyJson = JObject.Parse(new StreamReader(request.Body).ReadToEnd());
            var accessToken = requestBodyJson["session"]["user"]["accessToken"].ToString();
            if (!string.IsNullOrEmpty(accessToken))
            {
                context.Token = accessToken;
            }

            return Task.FromResult<object>(null);
        }
    }
}
