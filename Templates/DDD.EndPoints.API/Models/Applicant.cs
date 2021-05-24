using Framework.Domain.EventBus;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace $safeprojectname$.Models
{
    public class Applicant: IApplicant
    {
        public Guid UserId { get; }
        public string Token { get; }

        public Applicant(IHttpContextAccessor http)
        {
            if (http.HttpContext is null)
                return;

            var isExistToken = http.HttpContext.Request.Headers.TryGetValue("Authorization",
                out var authorization);

            if (isExistToken && !string.IsNullOrEmpty(authorization))
            {
                var strAuth = authorization.ToString();
                if (strAuth.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    Token = strAuth["Bearer ".Length..].Trim();
            }

            if (Token == null)
                return;

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(Token);
            if (!string.IsNullOrEmpty(jwt.Subject))
                UserId = Guid.Parse(jwt.Subject);
        }
    }
}