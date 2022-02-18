using DDD.DomainModels.People.Entities;
using Framework.Domain.Results;

namespace DDD.Contracts.People.Results
{
    public class PersonDetails : IResult
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public PersonDetails() { }

        public PersonDetails(Person person)
        {
            Id = person.Id;
            FirstName = person.FirstName.Value;
            LastName = person.LastName.Value;
            Age = person.Age.Value;
        }
    }
}