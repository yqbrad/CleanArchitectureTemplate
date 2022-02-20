using Framework.Domain.Requests;

namespace YQB.Contracts.People.Requests
{
    public class RenamePerson : IRequest
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void SetId(int id) => Id = id;
    }
}