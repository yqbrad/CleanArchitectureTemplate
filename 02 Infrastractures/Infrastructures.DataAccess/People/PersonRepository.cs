using DDD.Contracts.People.Repositories;
using DDD.Contracts.People.Requests;
using DDD.Contracts.People.Results;
using DDD.DomainModels.People.Entities;
using DDD.Infrastructure.DataAccess._Common;
using Framework.Domain.Paging;
using Framework.Tools;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Infrastructure.DataAccess.People
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