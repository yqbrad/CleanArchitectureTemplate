using YQB.Contracts.People.Results;
using Framework.Domain.Requests;
using System.Collections.Generic;

namespace YQB.Contracts.People.Requests
{
    public record GetAllPeople : IRequest<IEnumerable<PersonDetails>>;
}