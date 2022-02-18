using DDD.ApplicationServices._Common;
using DDD.Contracts._Common;
using DDD.Contracts.People.Requests;
using Framework.Domain.EventBus;
using System.Threading.Tasks;

namespace DDD.ApplicationServices.People.Request
{
    public class DeletePersonHandler : RequestHandler<DeletePerson>
    {
        public DeletePersonHandler(IUnitOfWork unitOfWork, IServiceBus serviceBus)
            : base(unitOfWork, serviceBus) { }

        public override async Task HandleAsync(DeletePerson req)
        {
            var person = await UnitOfWork.PersonRepository.GetAsync(req.Id);
            person.Delete();

            UnitOfWork.PersonRepository.Delete(person);
            await UnitOfWork.CommitAsync();
        }
    }
}