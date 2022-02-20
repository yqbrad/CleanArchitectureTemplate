using YQB.Contracts._Common;
using YQB.Contracts.People.Requests;
using YQB.Contracts.People.Results;
using YQB.DomainModels.People.Entities;
using Framework.Domain.Paging;
using System.Threading.Tasks;

namespace YQB.Contracts.People.Repositories
{
    public interface IPersonRepository : IRepository<Person, int>
    {
        Task<PageResult<PersonDetails>> FilterAsync(FilterPeople filter);
    }
}