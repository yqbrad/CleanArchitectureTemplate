using Framework.Domain.Results;

namespace DDD.Contracts.People.Results
{
    public record AddPersonResult(int Id) : IResult;
}