using YQB.Contracts.People.Requests;
using YQB.Contracts.People.Results;
using Framework.Domain.Paging;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace YQB.EndPoints.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        public Task<IActionResult> Add(
            [FromBody, Required] AddPerson request)
            => CreateAsync<AddPerson, int>(request);

        [HttpPut("{id:int}")]
        public Task<IActionResult> Rename(
            [FromRoute, Required] int id,
            [FromBody, Required] RenamePerson request)
        {
            request.SetId(id);
            return UpdateAsync(request);
        }

        [HttpDelete("{id:int}")]
        public Task<IActionResult> Delete(
            [FromRoute, Required] int id)
            => DeleteAsync(new DeletePerson(id));

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PersonDetails), StatusCodes.Status200OK)]
        public Task<IActionResult> Get(
            [FromRoute, Required] int id)
            => GetAllAsync<GetPerson, PersonDetails>(new(id));

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonDetails>), StatusCodes.Status200OK)]
        public Task<IActionResult> GetAll()
            => GetAllAsync<GetAllPeople, IEnumerable<PersonDetails>>(new());

        [HttpGet("Filter")]
        [ProducesResponseType(typeof(PageResult<PersonDetails>), StatusCodes.Status200OK)]
        public Task<IActionResult> Filter(
            [FromQuery, Required] FilterPeople filter)
            => GetAllAsync<FilterPeople, PageResult<PersonDetails>>(filter);
    }
}