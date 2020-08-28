using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Seabattle.Logic.Models;
using Seabattle.Logic.Services.Contracts;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Seabattle.API.Identity
{
    public class CustomOAuthProvider: OAuthAuthorizationServerProvider
    {
        private StandardKernel _kernel;
        public CustomOAuthProvider(StandardKernel kernel)
        {
            _kernel = kernel;
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var service = _kernel.Get<IUserService>();
            var user =  service.GetUser(context.UserName, context.Password);
            if (user.Value == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                context.Rejected();
                return Task.FromResult<object>(null);
            }

            var ticket = new AuthenticationTicket(SetClaimsIdentity(user.Value), new AuthenticationProperties());
            context.Validated(ticket);

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        private static ClaimsIdentity SetClaimsIdentity(UserDto user)
        {
            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim("sub", user.UserName));

            return identity;
        }
    }
}