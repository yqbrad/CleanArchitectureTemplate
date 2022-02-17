using DDD.DomainModels._Common.ValueObjects;
using Framework.Domain.BaseModels;
using Framework.Domain.Exceptions;

namespace DDD.DomainModels.People.ValueObjects
{
    public class PersonLastName:BaseValueObject<PersonLastName>
    {
        public string Value { get; private set; }

        private PersonLastName() { }

        public PersonLastName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainValidationException("نام خانوادگی خالی است");

            if (value.Trim().Length is < 2 or > 250)
                throw new DomainValidationException("طول نام خانوادگی باید بین 2 تا 250 کاراکتر باشد");

            Value = value.Trim();
        }

        public static PersonLastName Create(string value) => new(value);

        public static PersonLastName FromString(string value) => new(value);

        public override bool ObjectIsEqual(PersonLastName otherObject) => Value == otherObject.Value;

        public override int ObjectGetHashCode() => Value.GetHashCode();

        public static explicit operator string(PersonLastName title) => title.Value;

        public override string ToString() => Value;
    }
}