using DDD.ApplicationServices.People.Request;
using DDD.Contracts.People.Requests;
using DDD.Contracts.People.Results;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Framework.Domain.Paging;

namespace DDD.EndPoints.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        public Task<IActionResult> Add(
            [FromServices] AddPersonHandler handler,
            [FromBody, Required] AddPerson request)
            => CreateAsync(handler.HandleAsync, request);

        [HttpPut("{id:int}")]
        public Task<IActionResult> Rename(
            [FromServices] RenamePersonHandler handler,
            [FromRoute, Required] int id,
            [FromBody, Required] RenamePerson request)
        {
            request.SetId(id);
            return UpdateAsync(handler.HandleAsync, request);
        }

        [HttpDelete]
        public Task<IActionResult> Delete(
            [FromServices] DeletePersonHandler handler,
            [FromRoute, Required] int id)
            => DeleteAsync(handler.HandleAsync, new DeletePerson(id));

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PersonDetails), 200)]
        public Task<IActionResult> Get(
            [FromServices] GetPersonHandler handler,
            [FromRoute, Required] int id)
            => GetAllAsync(handler.HandleAsync, new GetPerson(id));

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonDetails>), 200)]
        public Task<IActionResult> GetAll(
            [FromServices] GetAllPeopleHandler handler)
            => GetAllAsync(handler.HandleAsync, new GetAllPeople());

        [HttpGet("Filter")]
        [ProducesResponseType(typeof(PageResult<PersonDetails>), 200)]
        public Task<IActionResult> Filter(
            [FromServices] FilterPeopleHandler handler,
            [FromQuery, Required] FilterPeople filter)
            => GetAllAsync(handler.HandleAsync, filter);
    }
}