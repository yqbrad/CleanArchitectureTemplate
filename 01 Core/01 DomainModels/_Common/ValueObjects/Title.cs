using Framework.Domain.BaseModels;
using Framework.Domain.Exceptions;

namespace YQB.DomainModels._Common.ValueObjects
{
    public class Title : BaseValueObject<Title>
    {
        public string Value { get; private set; }

        private Title() { }

        public Title(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("عنوان خالی است");

            if (value.Trim().Length is < 2 or > 250)
                throw new DomainException("طول عنوان باید بین 2 تا 250 کاراکتر باشد");

            Value = value.Trim();
        }

        public static Title Create(string value) => new(value);

        public static Title FromString(string value) => new(value);

        public override bool ObjectIsEqual(Title otherObject) => Value == otherObject.Value;

        public override int ObjectGetHashCode() => Value.GetHashCode();

        public static explicit operator string(Title title) => title.Value;

        public static implicit operator Title(string value) => value;

        public override string ToString() => Value;
    }
}