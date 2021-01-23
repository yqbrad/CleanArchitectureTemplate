using Framework.Domain.Commands;
using System.Threading.Tasks;

namespace Framework.Domain.ApplicationServices
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task<object> HandleAsync(T command);
    }
}