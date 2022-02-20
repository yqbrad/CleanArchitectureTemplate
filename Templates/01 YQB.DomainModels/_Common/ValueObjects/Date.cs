using System;
using Framework.Domain.BaseModels;

namespace $safeprojectname$._Common.ValueObjects
{
    public class Date : BaseValueObject<Date>
    {
        public DateTime Value { get; private set; }

        private Date() { }

        public Date(DateTime value) => Value = value;

        public static Date Create(DateTime value) => new(value);

        public static Date FromDateTime(DateTime value) => new(value);

        public static Date Now() => new(DateTime.Now);

        public override bool ObjectIsEqual(Date otherObject) => Value == otherObject.Value;

        public override int ObjectGetHashCode() => Value.GetHashCode();

        public static explicit operator DateTime(Date date) => date.Value;

        public static implicit operator Date(DateTime value) => value;
    }
}