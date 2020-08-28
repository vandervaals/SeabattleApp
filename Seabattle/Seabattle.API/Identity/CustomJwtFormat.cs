using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Owin.Security.DataHandler.Encoder;


namespace Seabattle.API.Identity
{
    public class CustomJwtFormat: ISecureDataFormat<AuthenticationTicket>
    {
        private static readonly byte[] _secret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["secret"]);
        private readonly string _issuer;

        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var signingKey = new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256);
            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;

            return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(_issuer, "Any", data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey));
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}