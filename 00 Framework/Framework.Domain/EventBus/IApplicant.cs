using System;

namespace Framework.Domain.EventBus
{
    public interface IApplicant
    {
        public int UserId { get; }
        public string Token { get; }
        public DateTime SessionExpireTime { get; }
    }
}