using YQB.Contracts.People.Requests;
using YQB.DomainModels._Common;
using FluentValidation;
using Framework.Domain.Translator;

namespace YQB.Contracts.People.RequestValidations;

public class FilterPeopleValidation : AbstractValidator<FilterPeople>
{
    public FilterPeopleValidation(ITranslator translator)
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(s => s.PageNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[StringResources.ValidationErrorRequired, StringResources.PageNumber])
            .GreaterThan(0)
            .WithMessage(translator[StringResources.ValidationErrorValueGraterThan, StringResources.PageNumber, StringResources.Zero]);

        RuleFor(s => s.PageSize)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(translator[StringResources.ValidationErrorRequired, StringResources.PageSize])
            .GreaterThan(0)
            .WithMessage(translator[StringResources.ValidationErrorValueGraterThan, StringResources.PageSize, StringResources.Zero]);
    }
}