using System;

namespace $safeprojectname$.EventBus
{
    public interface IUserInfo
    {
        public Guid UserId { get; }
        public string Token { get; }
    }
}