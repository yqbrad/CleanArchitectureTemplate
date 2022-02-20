using Framework.Domain.Requests;

namespace YQB.Contracts.People.Requests
{
    public class AddPerson : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}