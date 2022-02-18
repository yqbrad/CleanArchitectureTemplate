using DDD.ApplicationServices._Common;
using DDD.Contracts._Common;
using DDD.Contracts.People.Requests;
using DDD.Contracts.People.Results;
using Framework.Domain.EventBus;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.ApplicationServices.People.Request
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