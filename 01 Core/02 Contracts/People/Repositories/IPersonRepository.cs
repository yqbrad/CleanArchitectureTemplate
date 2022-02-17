using DDD.Contracts._Common;
using DDD.DomainModels.People.Entities;

namespace DDD.Contracts.People.Repositories
{
    public interface IPersonRepository : IRepository<Person, int>
    {
    }
}