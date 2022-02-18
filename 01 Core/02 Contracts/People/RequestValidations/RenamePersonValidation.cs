using DDD.Contracts.People.Repositories;
using DDD.Contracts.People.Requests;
using FluentValidation;

namespace DDD.Contracts.People.RequestValidations
{
    public class RenamePersonValidation : AbstractValidator<RenamePerson>
    {
        public RenamePersonValidation(IPersonRepository repo)
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(s => s.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("ای دی خالی است")
                .When(s => !repo.Exists(x => x.Id == s.Id))
                .WithMessage("شخص مورد نظر یافت نشد");

            RuleFor(s => s.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("نام ارسال نشده است")
                .NotEmpty()
                .WithMessage("نام خالی است");

            RuleFor(s => s.LastName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("نام خانوادگی ارسال نشده است")
                .NotEmpty()
                .WithMessage("نام خانوادگی خالی است");
        }
    }
}