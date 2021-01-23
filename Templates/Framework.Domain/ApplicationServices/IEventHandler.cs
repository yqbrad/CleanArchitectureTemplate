using $safeprojectname$.Events;
using System.Threading.Tasks;

namespace $safeprojectname$.ApplicationServices
{
    public interface IEventHandler<in T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}