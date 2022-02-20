using YQB.Contracts.People.Results;
using Framework.Domain.Paging;

namespace YQB.Contracts.People.Requests
{
    public class FilterPeople : PageRequest<PersonDetails>
    {
        public string Name { get; set; }
    }
}