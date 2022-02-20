using System.Threading.Tasks;
using Framework.Domain.EventBus;
using YQB.ApplicationServices._Common;
using YQB.Contracts._Common;
using YQB.Contracts.People.Requests;

namespace YQB.ApplicationServices.People.Requests
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