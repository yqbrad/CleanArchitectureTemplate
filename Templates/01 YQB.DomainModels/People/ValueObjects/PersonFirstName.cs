using $safeprojectname$._Common;
using Framework.Domain.BaseModels;
using Framework.Domain.Exceptions;

namespace $safeprojectname$.People.ValueObjects;

public class PersonFirstName : BaseValueObject<PersonFirstName>
{
    public string Value { get; private set; }

    private PersonFirstName() { }

    public PersonFirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(StringResources.ValidationErrorRequired, StringResources.FirstName);

        if (value.Trim().Length is < 2 or > 250)
            throw new DomainException(StringResources.ValidationErrorStringLength, StringResources.FirstName, 2.ToString(), 250.ToString());

        Value = value.Trim();
    }

    public static PersonFirstName Create(string value) => new(value);

    public static PersonFirstName FromString(string value) => new(value);

    public override bool ObjectIsEqual(PersonFirstName otherObject) => Value == otherObject.Value;

    public override int ObjectGetHashCode() => Value.GetHashCode();

    public static explicit operator string(PersonFirstName title) => title.Value;

    public override string ToString() => Value;
}