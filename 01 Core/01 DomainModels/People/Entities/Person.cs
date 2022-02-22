using YQB.DomainModels.People.ValueObjects;
using Framework.Domain.BaseModels;
using YQB.DomainModels.People.Enums;

namespace YQB.DomainModels.People.Entities
{
    public class Person : BaseAggregateRoot<int>
    {
        public PersonFirstName FirstName { get; private set; }
        public PersonLastName LastName { get; private set; }
        public PersonAge Age { get; private set; }
        public Gender Gender { get; private set; }

        private Person() { }

        private Person(int id, PersonFirstName firstName, PersonLastName lastName, PersonAge age, Gender gender)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Gender = gender;
        }

        public static Person Create(int id, 
            PersonFirstName firstName,
            PersonLastName lastName, 
            PersonAge age,
            Gender gender)
            => new(id, firstName, lastName, age, gender);

        public void Rename(PersonFirstName firstName, PersonLastName lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void Delete()
        {

        }
    }
}