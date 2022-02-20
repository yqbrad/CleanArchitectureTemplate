using YQB.ApplicationServices._Common;
using YQB.Contracts._Common;
using YQB.Contracts.People.Requests;
using YQB.Contracts.People.Results;
using Framework.Domain.EventBus;
using Framework.Domain.Paging;
using System.Threading.Tasks;

namespace YQB.ApplicationServices.People.Request
{
    public class FilterPeopleHandler : RequestHandler<FilterPeople, PageResult<PersonDetails>>
    {
        public FilterPeopleHandler(IUnitOfWork unitOfWork, IServiceBus serviceBus)
            : base(unitOfWork, serviceBus) { }

        public override Task<PageResult<PersonDetails>> HandleAsync(FilterPeople req)
            => UnitOfWork.PersonRepository.FilterAsync(req);
    }
}