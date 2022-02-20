﻿using YQB.ApplicationServices._Common;
using YQB.Contracts._Common;
using YQB.Contracts.People.Requests;
using YQB.DomainModels.People.ValueObjects;
using Framework.Domain.EventBus;
using System.Threading.Tasks;

namespace YQB.ApplicationServices.People.Request
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