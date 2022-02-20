using Framework.Domain.Requests;

namespace YQB.Contracts.People.Requests
{
    public record DeletePerson(int Id) : IRequest;
}