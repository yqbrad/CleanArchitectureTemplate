using Framework.Domain.BaseModels;
using System;

namespace DDD.DomainModels.People.Entities
{
    public class Person : BaseAggregateRoot<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Age { get; private set; }

        private Person() { }

        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
    }
}