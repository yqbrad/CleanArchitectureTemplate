using YQB.Contracts.People.Repositories;
using YQB.Contracts.People.Requests;
using YQB.Contracts.People.Results;
using YQB.DomainModels.People.Entities;
using YQB.Infrastructure.DataAccess._Common;
using Framework.Domain.Paging;
using Framework.Tools;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace YQB.Infrastructure.DataAccess.People
{
    public class PersonRepository : BaseRepository<Person, int, ServiceDbContext>, IPersonRepository
    {
        public PersonRepository(ServiceDbContext dbContext) : base(dbContext) { }

        public async Task<PageResult<PersonDetails>> FilterAsync(FilterPeople filter)
        {
            var query = DbContext.People
                .AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(filter.Name),
                    s => ((string)s.FirstName).Contains(filter.Name) ||
                         ((string)s.LastName).Contains(filter.Name));

            if (!string.IsNullOrWhiteSpace(filter.SortBy))
                query = query.OrderByField(filter.SortBy, filter.SortAscending);

            var people = await query
                .Skip(filter.SkipCount)
                .Take(filter.PageSize)
                .ToListAsync();

            var result = new PageResult<PersonDetails>
            {
                Data = people.Select(s => new PersonDetails(s)),
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalCount = null
            };

            if (filter.NeedTotalCount)
                result.TotalCount = await query.CountAsync();

            return result;
        }
    }
}