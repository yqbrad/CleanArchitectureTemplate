using YQB.DomainModels._Common;
using Framework.Domain.BaseModels;
using Framework.Domain.Exceptions;

namespace YQB.DomainModels.People.ValueObjects;

public class PersonAge : BaseValueObject<PersonAge>
{
    public int Value { get; private set; }

    private PersonAge() { }

    public PersonAge(int value) => Value = value;

    public static PersonAge Create(int value)
    {
        if (value < 0)
            throw new DomainException(StringResources.ValidationErrorDateGreaterThan, StringResources.Age, StringResources.Zero);

        return new PersonAge(value);
    }

    public static PersonAge FromInt(int value) => new(value);

    public override bool ObjectIsEqual(PersonAge otherObject) => Value == otherObject.Value;

    public override int ObjectGetHashCode() => Value.GetHashCode();

    public static explicit operator int(PersonAge money) => money.Value;
}