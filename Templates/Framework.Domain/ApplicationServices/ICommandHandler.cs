using $safeprojectname$.Commands;
using System.Threading.Tasks;

namespace $safeprojectname$.ApplicationServices
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task<object> HandleAsync(T command);
    }
}