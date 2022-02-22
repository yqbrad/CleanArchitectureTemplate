using System.Threading.Tasks;
using Framework.Domain.EventBus;
using YQB.ApplicationServices._Common;
using YQB.Contracts._Common;
using YQB.Contracts.People.Requests;
using YQB.DomainModels.People.Entities;
using YQB.DomainModels.People.ValueObjects;

namespace YQB.ApplicationServices.People.Requests
{
    public class AddPersonHandler : RequestHandler<AddPerson, int>
    {
        public AddPersonHandler(IUnitOfWork unitOfWork, IServiceBus serviceBus)
            : base(unitOfWork, serviceBus) { }

        public override async Task<int> HandleAsync(AddPerson req)
        {
            var id = await UnitOfWork.PersonRepository.CreateIdAsync();
            var person = Person.Create(id,
                PersonFirstName.FromString(req.FirstName),
                PersonLastName.FromString(req.LastName),
                PersonAge.FromInt(req.Age),
                req.Gender);

            await UnitOfWork.PersonRepository.InsertAsync(person);
            await UnitOfWork.CommitAsync();

            return id;
        }
    }
}