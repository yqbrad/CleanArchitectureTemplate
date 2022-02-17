using DDD.DomainModels.People.ValueObjects;
using Framework.Domain.BaseModels;

namespace DDD.DomainModels.People.Entities
{
    public class Person : BaseAggregateRoot<int>
    {
        public PersonFirstName FirstName { get; private set; }
        public PersonLastName LastName { get; private set; }
        public PersonAge Age { get; private set; }

        private Person() { }

        private Person(int id, PersonFirstName firstName, PersonLastName lastName, PersonAge age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public static Person Create(int id, PersonFirstName firstName, PersonLastName lastName, PersonAge age)
            => new(id, firstName, lastName, age);

        public void Rename(PersonFirstName firstName, PersonLastName lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}