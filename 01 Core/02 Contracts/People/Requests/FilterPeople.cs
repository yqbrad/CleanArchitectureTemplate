using DDD.Contracts.People.Results;
using Framework.Domain.Paging;

namespace DDD.Contracts.People.Requests
{
    public class FilterPeople : PageRequest<PersonDetails>
    {
        public string Name { get; set; }
    }
}