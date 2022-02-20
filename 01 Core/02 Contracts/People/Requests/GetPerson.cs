using YQB.Contracts.People.Results;
using Framework.Domain.Requests;

namespace YQB.Contracts.People.Requests
{
    public record GetPerson(int Id) : IRequest<PersonDetails>;
}