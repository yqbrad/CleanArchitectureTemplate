using Framework.Domain.BaseModels;
using Framework.Domain.Exceptions;

namespace YQB.DomainModels._Common.ValueObjects
{
    public class Description : BaseValueObject<Description>
    {
        public string Value { get; private set; }

        private Description() { }

        public Description(string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Trim().Length > 500)
                throw new DomainException("طول توضیحات باید کمتر از 500 کاراکتر باشد");

            Value = value?.Trim();
        }

        public static Description Create(string value) => new(value);

        public static Description FromString(string value) => new(value);

        public override bool ObjectIsEqual(Description otherObject) => Value == otherObject.Value;

        public override int ObjectGetHashCode() => Value.GetHashCode();

        public static explicit operator string(Description description) => description.Value;

        public static implicit operator Description(string value) => value;

        public override string ToString() => Value;
    }
}