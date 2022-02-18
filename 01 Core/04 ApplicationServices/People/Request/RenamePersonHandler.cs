using DDD.ApplicationServices._Common;
using DDD.Contracts._Common;
using DDD.Contracts.People.Requests;
using DDD.DomainModels.People.ValueObjects;
using Framework.Domain.EventBus;
using System.Threading.Tasks;

namespace DDD.ApplicationServices.People.Request
{
    public class RenamePersonHandler : RequestHandler<RenamePerson>
    {
        public RenamePersonHandler(IUnitOfWork unitOfWork, IServiceBus serviceBus)
            : base(unitOfWork, serviceBus) { }

        public override async Task HandleAsync(RenamePerson req)
        {
            var person = await UnitOfWork.PersonRepository.GetAsync(req.Id);
            person.Rename(PersonFirstName.FromString(req.FirstName),
                PersonLastName.FromString(req.LastName));

            //UnitOfWork.PersonRepository.Update(person);
            await UnitOfWork.CommitAsync();
        }
    }
}