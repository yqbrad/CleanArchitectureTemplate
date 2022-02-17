using DDD.ApplicationServices._Common;
using DDD.Contracts._Common;
using DDD.Contracts.People.Requests;
using DDD.Contracts.People.Results;
using DDD.DomainModels.People.Entities;
using DDD.DomainModels.People.ValueObjects;
using Framework.Domain.EventBus;
using System.Threading.Tasks;

namespace DDD.ApplicationServices.People.Request
{
    public class AddPersonHandler : RequestHandler<AddPerson, AddPersonResult>
    {
        public AddPersonHandler(IUnitOfWork unitOfWork, IServiceBus serviceBus)
            : base(unitOfWork, serviceBus) { }

        public override async Task<AddPersonResult> HandleAsync(AddPerson req)
        {
            var id = await UnitOfWork.PersonRepository.CreateIdAsync();
            var person = Person.Create(id,
                PersonFirstName.FromString(req.FirstName),
                PersonLastName.FromString(req.LastName),
                PersonAge.FromInt(req.Age));

            await UnitOfWork.PersonRepository.InsertAsync(person);
            await UnitOfWork.CommitAsync();

            return new(id);
        }
    }
}