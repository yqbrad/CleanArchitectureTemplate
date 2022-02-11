using DDD.ApplicationServices._Common;
using DDD.Contracts._Common;
using DDD.Contracts.People.Requests;
using DDD.DomainModels.People.Entities;
using Framework.Domain.EventBus;
using System.Threading.Tasks;

namespace DDD.ApplicationServices.People.Request
{
    public class AddPersonHandler : RequestHandler<AddPerson>
    {
        public AddPersonHandler(IUnitOfWork unitOfWork, IServiceBus serviceBus)
            : base(unitOfWork, serviceBus) { }

        public override Task HandleAsync(AddPerson req)
        {
            var person = new Person(req.FirstName, req.LastName, req.Age);
            return Task.CompletedTask;
        }
    }
}