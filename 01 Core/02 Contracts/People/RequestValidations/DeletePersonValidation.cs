using DDD.Contracts.People.Repositories;
using DDD.Contracts.People.Requests;
using FluentValidation;

namespace DDD.Contracts.People.RequestValidations
{
    public class DeletePersonValidation : AbstractValidator<DeletePerson>
    {
        public DeletePersonValidation(IPersonRepository repo)
        {
            RuleFor(s => s.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("ای دی خالی است")
                .When(s => !repo.Exists(x => x.Id == s.Id))
                .WithMessage("شخص مورد نظر یافت نشد");
        }
    }
}