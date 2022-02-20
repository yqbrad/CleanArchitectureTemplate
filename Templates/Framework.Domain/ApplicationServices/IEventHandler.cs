using $safeprojectname$.Events;
using System.Threading.Tasks;

namespace $safeprojectname$.ApplicationServices
{
    public interface IEventHandler<in T> where T : IDomainEvent
    {
        Task HandleAsync(T @event);
    }
}