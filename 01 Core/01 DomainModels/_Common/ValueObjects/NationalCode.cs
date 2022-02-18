using Framework.Domain.BaseModels;
using Framework.Domain.Exceptions;
using Framework.Tools;

namespace DDD.DomainModels._Common.ValueObjects
{
    public class NationalCode : BaseValueObject<NationalCode>
    {
        public string Value { get; private set; }

        private NationalCode() { }

        public NationalCode(string value)
        {
            if (!value.IsNationalCode())
                throw new DomainException("کد ملی صحیح نمی باشد");

            Value = value;
        }

        public static NationalCode FromString(string value) => new(value);

        public override bool ObjectIsEqual(NationalCode otherObject) => Value == otherObject.Value;

        public override int ObjectGetHashCode() => Value.GetHashCode();

        public static explicit operator string(NationalCode nationalCode) => nationalCode.Value;

        public static implicit operator NationalCode(string value) => value;

        public override string ToString() => Value;
    }
}