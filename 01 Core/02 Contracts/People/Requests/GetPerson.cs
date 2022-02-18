using DDD.Contracts.People.Results;
using Framework.Domain.Requests;

namespace DDD.Contracts.People.Requests
{
    public record GetPerson(int Id) : IRequest<PersonDetails>;
}