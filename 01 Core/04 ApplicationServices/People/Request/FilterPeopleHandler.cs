using DDD.ApplicationServices._Common;
using DDD.Contracts._Common;
using DDD.Contracts.People.Requests;
using DDD.Contracts.People.Results;
using Framework.Domain.EventBus;
using Framework.Domain.Paging;
using System.Threading.Tasks;

namespace DDD.ApplicationServices.People.Request
{
    public class FilterPeopleHandler : RequestHandler<FilterPeople, PageResult<PersonDetails>>
    {
        public FilterPeopleHandler(IUnitOfWork unitOfWork, IServiceBus serviceBus)
            : base(unitOfWork, serviceBus) { }

        public override Task<PageResult<PersonDetails>> HandleAsync(FilterPeople req)
            => UnitOfWork.PersonRepository.FilterAsync(req);
    }
}