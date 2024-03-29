﻿using Framework.Domain.EventBus;
using System.IdentityModel.Tokens.Jwt;

namespace YQB.EndPoints.API.Models
{
    public class UserInfo: IUserInfo
    {
        public Guid UserId { get; }
        public string Token { get; }

        public UserInfo(IHttpContextAccessor http)
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