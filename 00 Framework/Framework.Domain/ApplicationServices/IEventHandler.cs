using Framework.Domain.Events;
using System.Threading.Tasks;

namespace Framework.Domain.ApplicationServices
{
    public interface IEventHandler<in T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}