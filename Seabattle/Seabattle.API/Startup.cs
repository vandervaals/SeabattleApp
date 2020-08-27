using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Ninject;
using Owin;
using Microsoft.Owin.Cors;
using System.Web.Cors;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System.Configuration;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Seabattle.API.Identity;
using System.Net.Http;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Seabattle.Logic;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;

[assembly: OwinStartup(typeof(Seabattle.API.Startup))]

namespace Seabattle.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            WebApiConfig.Register(config);

            var kernel = new StandardKernel(new NinjectSettings { LoadExtensions = true });
            kernel.Load(new SeabattleDIModule());

            var provide = new CorsPolicyProvider();
            provide.PolicyResolver = ctx => Task.FromResult(new CorsPolicy
            {
                AllowAnyHeader = true,
                AllowAnyMethod = true,
                AllowAnyOrigin = true
            });
            app.UseCors(new CorsOptions { PolicyProvider = provide });
            app.MapSignalR(new Microsoft.AspNet.SignalR.HubConfiguration { EnableDetailedErrors = true, EnableJSONP = true });
            //config.MessageHandlers.Add(new PreflightRequestsHandler());

            var issuer = ConfigurationManager.AppSettings["issuer"];
            var secret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["secret"]);

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { "Any" },
                IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                {
                    new SymmetricKeyIssuerSecurityKeyProvider(issuer, secret)
                }
            });

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new CustomOAuthProvider(kernel),
                AccessTokenFormat = new CustomJwtFormat(issuer)
            });

            app.UseNinjectMiddleware(() => kernel).UseNinjectWebApi(config);
        }

        public class PreflightRequestsHandler : DelegatingHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (request.Headers.Contains("Origin") && request.Method.Method == "OPTIONS")
                {
                    var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
                    response.Headers.Add("Access-Control-Allow-Origin", "*");
                    response.Headers.Add("Access-Control-Allow-Headers", "Origin, Content-Type, Accept, Authorization");
                    response.Headers.Add("Access-Control-Allow-Methods", "*");
                    var tsc = new TaskCompletionSource<HttpResponseMessage>();
                    tsc.SetResult(response);
                    return tsc.Task;
                }
                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}
