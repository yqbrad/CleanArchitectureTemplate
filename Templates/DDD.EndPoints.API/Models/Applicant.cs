using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Framework.Domain.EventBus;
using Microsoft.AspNetCore.Http;

namespace $safeprojectname$.Models
{
    public class Applicant : IApplicant
    {
        public Applicant(IHttpContextAccessor http)
        {
            if (http.HttpContext != null)
            {
                string authorization = http.HttpContext.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(authorization))
                    if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        Token = authorization.Substring("Bearer ".Length).Trim();
            }

            if (Token != null)
            {
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(Token);
                UserId = int.Parse(jwt.Claims.FirstOrDefault(s => s.Type == "userId")?.Value ?? "0");
                SessionExpireTime = jwt.ValidTo;
            }
        }

        public int UserId { get; }
        public string Token { get; }
        public DateTime SessionExpireTime { get; }
    }
}
