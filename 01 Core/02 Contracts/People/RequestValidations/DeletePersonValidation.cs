using YQB.Contracts.People.Repositories;
using YQB.Contracts.People.Requests;
using YQB.DomainModels._Common;
using FluentValidation;
using Framework.Domain.Translator;

namespace YQB.Contracts.People.RequestValidations;

public class DeletePersonValidation : AbstractValidator<DeletePerson>
{
    public DeletePersonValidation(ITranslator translator, IPersonRepository repo)
    {
        RuleFor(s => s.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[StringResources.ValidationErrorRequired, StringResources.Id])
            .Must(id => repo.Exists(x => x.Id == id))
            .WithMessage(translator[StringResources.ValidationErrorNotExist, StringResources.Person]);
    }
}