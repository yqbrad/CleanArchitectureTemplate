using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Domain.EventBus;
using $safeprojectname$._Common;
using YQB.Contracts._Common;
using YQB.Contracts.People.Requests;
using YQB.Contracts.People.Results;

namespace $safeprojectname$.People.Requests
{
    public class GetAllPeopleHandler : RequestHandler<GetAllPeople, IEnumerable<PersonDetails>>
    {
        public GetAllPeopleHandler(IUnitOfWork unitOfWork, IServiceBus serviceBus)
            : base(unitOfWork, serviceBus) { }

        public override async Task<IEnumerable<PersonDetails>> HandleAsync(GetAllPeople req)
        {
            var people = await UnitOfWork.PersonRepository.GetAllAsync();
            return people.Select(s => new PersonDetails(s));
        }
    }
}