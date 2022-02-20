using Framework.Domain.Requests;

namespace $safeprojectname$.People.Requests
{
    public class AddPerson : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}