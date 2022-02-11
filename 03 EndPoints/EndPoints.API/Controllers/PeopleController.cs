using System.ComponentModel.DataAnnotations;
using DDD.ApplicationServices.People.Request;
using DDD.Contracts.People.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DDD.EndPoints.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : BaseController
    {
        [HttpPost]

        public Task<IActionResult> Add(
            [FromServices] AddPersonHandler handler,
            [FromBody, Required] AddPerson request)
            => CreateAsync(handler.HandleAsync, request);
    }
}