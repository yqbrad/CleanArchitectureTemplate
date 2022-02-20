using $safeprojectname$.People.Results;
using Framework.Domain.Paging;

namespace $safeprojectname$.People.Requests
{
    public class FilterPeople : PageRequest<PersonDetails>
    {
        public string Name { get; set; }
    }
}