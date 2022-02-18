using DDD.Contracts._Common;
using DDD.Contracts.People.Requests;
using DDD.Contracts.People.Results;
using DDD.DomainModels.People.Entities;
using Framework.Domain.Paging;
using System.Threading.Tasks;

namespace DDD.Contracts.People.Repositories
{
    public interface IPersonRepository : IRepository<Person, int>
    {
        Task<PageResult<PersonDetails>> FilterAsync(FilterPeople filter);
    }
}