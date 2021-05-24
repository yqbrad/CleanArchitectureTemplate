using System;

namespace $safeprojectname$.EventBus
{
    public interface IApplicant
    {
        public Guid UserId { get; }
        public string Token { get; }
    }
}