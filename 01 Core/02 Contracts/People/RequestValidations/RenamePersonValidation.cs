using DDD.Contracts.People.Repositories;
using DDD.Contracts.People.Requests;
using DDD.DomainModels._Common;
using FluentValidation;
using Framework.Domain.Translator;

namespace DDD.Contracts.People.RequestValidations;

public class RenamePersonValidation : AbstractValidator<RenamePerson>
{
    public RenamePersonValidation(ITranslator translator, IPersonRepository repo)
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(s => s.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[StringResources.ValidationErrorRequired, StringResources.Id])
            .Must(id => repo.Exists(s => s.Id == id))
            .WithMessage(translator[StringResources.ValidationErrorNotExist, StringResources.Person]);

        RuleFor(s => s.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[StringResources.ValidationErrorRequired, StringResources.FirstName]);

        RuleFor(s => s.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[StringResources.ValidationErrorRequired, StringResources.LastName]);
    }
}