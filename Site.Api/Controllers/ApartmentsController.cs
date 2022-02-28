using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Site.Application.Features.Commands.Apartments.AddApartment;
using Site.Application.Features.Commands.Apartments.DeleteApartment;
using Site.Application.Features.Commands.Apartments.UpdateApartment;
using Site.Application.Features.Queries.Apartments.GetAllApartments;
using Site.Application.Features.Queries.Apartments.GetApartment;
using System.Threading.Tasks;

namespace Site.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApartment()
        {
            return Ok(await _mediator.Send(new GetAllApartmentsQuery()));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetApartmentById(int Id)
        {
            var result = await _mediator.Send(new GetApartmentQuery(Id));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddApartment([FromBody]AddApartmentCommand addApartmentCommand)
        {
            var result = await _mediator.Send(addApartmentCommand);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateApartment([FromBody]UpdateApartmentCommand updateApartmentCommand)
        {
            var result = await _mediator.Send(updateApartmentCommand);

            return Ok("Apartment updated");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteApartment(int Id)
        {
            var result = await _mediator.Send(new DeleteApartmentCommand(Id));
            
            return Ok("Apartment deleted.");
        }
    }
}
