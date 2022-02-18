using DDD.ApplicationServices._Common;
using DDD.Contracts._Common;
using DDD.Contracts.People.Requests;
using DDD.Contracts.People.Results;
using Framework.Domain.EventBus;
using System.Threading.Tasks;

namespace DDD.ApplicationServices.People.Request
{
    public class GetPersonHandler : RequestHandler<GetPerson, PersonDetails>
    {
        public GetPersonHandler(IUnitOfWork unitOfWork, IServiceBus serviceBus)
            : base(unitOfWork, serviceBus) { }

        public override async Task<PersonDetails> HandleAsync(GetPerson req)
        {
            var person = await UnitOfWork.PersonRepository.GetAsync(req.Id);
            return person is null ? null : new(person);
        }
    }
}