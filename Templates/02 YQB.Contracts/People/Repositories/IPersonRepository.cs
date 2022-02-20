using $safeprojectname$._Common;
using $safeprojectname$.People.Requests;
using $safeprojectname$.People.Results;
using YQB.DomainModels.People.Entities;
using Framework.Domain.Paging;
using System.Threading.Tasks;

namespace $safeprojectname$.People.Repositories
{
    public interface IPersonRepository : IRepository<Person, int>
    {
        Task<PageResult<PersonDetails>> FilterAsync(FilterPeople filter);
    }
}