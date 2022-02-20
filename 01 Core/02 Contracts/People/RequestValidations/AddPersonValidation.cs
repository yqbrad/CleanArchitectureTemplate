using YQB.Contracts.People.Requests;
using YQB.DomainModels._Common;
using FluentValidation;
using Framework.Domain.Translator;

namespace YQB.Contracts.People.RequestValidations;

public class AddPersonValidation : AbstractValidator<AddPerson>
{
    public AddPersonValidation(ITranslator translator)
    {
        RuleFor(s => s.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[StringResources.ValidationErrorRequired, StringResources.FirstName]);

        RuleFor(s => s.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[StringResources.ValidationErrorRequired, StringResources.LastName]);

        RuleFor(s => s.Age)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[StringResources.ValidationErrorRequired, StringResources.Age])
            .GreaterThan(0)
            .WithMessage(translator[StringResources.ValidationErrorValueGraterThan, StringResources.Age, StringResources.Zero]);
    }
}