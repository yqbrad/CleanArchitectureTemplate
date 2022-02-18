﻿using DDD.Contracts.People.Requests;
using FluentValidation;

namespace DDD.Contracts.People.RequestValidations
{
    public class AddPersonValidation : AbstractValidator<AddPerson>
    {
        public AddPersonValidation()
        {
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

            RuleFor(s => s.Age)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("سن ارسال نشده است")
                .GreaterThan(0)
                .WithMessage("سن باید بزرگتر از 0 باشد");
        }
    }
}