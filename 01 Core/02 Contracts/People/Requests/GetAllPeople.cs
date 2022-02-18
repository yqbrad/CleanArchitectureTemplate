using DDD.Contracts.People.Results;
using Framework.Domain.Requests;
using System.Collections.Generic;

namespace DDD.Contracts.People.Requests
{
    public record GetAllPeople : IRequest<IEnumerable<PersonDetails>>;
}