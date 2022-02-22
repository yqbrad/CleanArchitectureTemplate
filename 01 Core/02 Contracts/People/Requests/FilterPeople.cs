using YQB.Contracts.People.Results;
using Framework.Domain.Paging;
using YQB.DomainModels.People.Enums;

namespace YQB.Contracts.People.Requests
{
    public class FilterPeople : PageRequest<PersonDetails>
    {
        public string Name { get; set; }
        public Gender? Gender { get; set; }
    }
}