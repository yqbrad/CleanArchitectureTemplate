using $safeprojectname$.People.Results;
using Framework.Domain.Requests;
using System.Collections.Generic;

namespace $safeprojectname$.People.Requests
{
    public record GetAllPeople : IRequest<IEnumerable<PersonDetails>>;
}