using Framework.Domain.Requests;

namespace DDD.Contracts.People.Requests
{
    public class AddPerson : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}