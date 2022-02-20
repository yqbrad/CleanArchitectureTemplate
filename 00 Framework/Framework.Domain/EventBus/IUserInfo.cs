using System;

namespace Framework.Domain.EventBus
{
    public interface IUserInfo
    {
        public Guid UserId { get; }
        public string Token { get; }
    }
}