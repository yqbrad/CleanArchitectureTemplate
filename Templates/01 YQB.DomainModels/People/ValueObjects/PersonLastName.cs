using $safeprojectname$._Common;
using Framework.Domain.BaseModels;
using Framework.Domain.Exceptions;

namespace $safeprojectname$.People.ValueObjects;

public class PersonLastName : BaseValueObject<PersonLastName>
{
    public string Value { get; private set; }

    private PersonLastName() { }

    public PersonLastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(StringResources.ValidationErrorRequired, StringResources.LastName);

        if (value.Trim().Length is < 2 or > 250)
            throw new DomainException(StringResources.ValidationErrorStringLength, StringResources.LastName, 2.ToString(), 250.ToString());

        Value = value.Trim();
    }

    public static PersonLastName Create(string value) => new(value);

    public static PersonLastName FromString(string value) => new(value);

    public override bool ObjectIsEqual(PersonLastName otherObject) => Value == otherObject.Value;

    public override int ObjectGetHashCode() => Value.GetHashCode();

    public static explicit operator string(PersonLastName title) => title.Value;

    public override string ToString() => Value;
}