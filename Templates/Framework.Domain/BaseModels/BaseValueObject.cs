using System;
using System.Collections.Generic;
using System.Text;

namespace $safeprojectname$.BaseModels
{
    public abstract class BaseValueObject<TValueObject> : IEquatable<TValueObject>
        where TValueObject : BaseValueObject<TValueObject>
    {
        public abstract bool ObjectIsEqual(TValueObject otherObject);
        public abstract int ObjectGetHashCode();

        public bool Equals(TValueObject other) => this == other;

        public override bool Equals(object obj)
        {
            if (!(obj is TValueObject otherObject))
                return false;
            return ObjectIsEqual(otherObject);
        }

        public override int GetHashCode() => ObjectGetHashCode();

        public static bool operator ==(BaseValueObject<TValueObject> right, BaseValueObject<TValueObject> left)
        {
            if (right is null && left is null)
                return true;
            if (right is null || left is null)
                return false;
            return right.Equals(left);
        }

        public static bool operator !=(BaseValueObject<TValueObject> right, BaseValueObject<TValueObject> left) => !(right == left);
    }
}