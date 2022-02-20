using $safeprojectname$.People.Results;
using Framework.Domain.Requests;

namespace $safeprojectname$.People.Requests
{
    public record GetPerson(int Id) : IRequest<PersonDetails>;
}