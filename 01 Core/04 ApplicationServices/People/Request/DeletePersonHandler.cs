﻿using YQB.ApplicationServices._Common;
using YQB.Contracts._Common;
using YQB.Contracts.People.Requests;
using Framework.Domain.EventBus;
using System.Threading.Tasks;

namespace YQB.ApplicationServices.People.Request
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