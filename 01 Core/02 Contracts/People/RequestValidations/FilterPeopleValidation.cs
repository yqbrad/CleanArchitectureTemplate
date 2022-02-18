using DDD.Contracts.People.Requests;
using FluentValidation;

namespace DDD.Contracts.People.RequestValidations
{
    public class FilterPeopleValidation : AbstractValidator<FilterPeople>
    {
        public FilterPeopleValidation()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(s => s.PageNumber)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("شماره صفحه ارسال نشده است")
                .GreaterThan(0)
                .WithMessage("شماره صفحه باید بزرگتر از صفر باشد");

            RuleFor(s => s.PageSize)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("تعداد رکورد ارسال نشده است")
                .GreaterThan(0)
                .WithMessage("تعداد رکورد باید بزرگتر از صفر باشد");
        }
    }
}