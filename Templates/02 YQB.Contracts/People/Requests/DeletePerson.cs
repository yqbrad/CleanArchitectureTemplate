using Framework.Domain.Requests;

namespace $safeprojectname$.People.Requests
{
    public record DeletePerson(int Id) : IRequest;
}