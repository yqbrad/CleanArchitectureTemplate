using System;

namespace Framework.Domain.EventBus
{
    public interface IApplicant
    {
        public Guid UserId { get; }
        public string Token { get; }
    }
}