using Framework.Domain.Events;
using System.Threading.Tasks;

namespace Framework.Domain.ApplicationServices
{
    public interface IEventHandler<in T> where T : IDomainEvent
    {
        Task HandleAsync(T @event);
    }
}