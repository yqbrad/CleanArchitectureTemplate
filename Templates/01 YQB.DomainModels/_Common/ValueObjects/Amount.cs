using System.ComponentModel.DataAnnotations;
using Framework.Domain.BaseModels;
using Framework.Domain.Exceptions;

namespace $safeprojectname$._Common.ValueObjects
{
    public class Amount : BaseValueObject<Amount>
    {
        public decimal Value { get; private set; }

        private Amount() { }

        public Amount(decimal value) => Value = value;

        public static Amount Create(decimal value)
        {
            if (value < 0)
                throw new DomainException("مبلغ باید بزرگتر از صفر باشد");

            return new Amount(value);
        }

        public static Amount FromDecimal(decimal value) => new(value);

        public override bool ObjectIsEqual(Amount otherObject) => Value == otherObject.Value;

        public override int ObjectGetHashCode() => Value.GetHashCode();

        public static explicit operator decimal(Amount money) => money.Value;

        public static implicit operator Amount(decimal value) => value;
    }
}