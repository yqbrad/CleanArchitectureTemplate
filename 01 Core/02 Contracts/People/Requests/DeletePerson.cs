using Framework.Domain.Requests;

namespace DDD.Contracts.People.Requests
{
    public record DeletePerson(int Id) : IRequest;
}