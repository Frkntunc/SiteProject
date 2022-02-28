using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site.Application.Features.Commands.Bills.AddBill;
using Site.Application.Features.Queries.Bills.GetAllBills;
using Site.Application.Features.Queries.Bills.GetBill;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Site.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddBill([FromBody]AddBillCommand addBillCommand)
        {
            var result = await _mediator.Send(addBillCommand);
            return Ok("Bill Added.");
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAllBills()
        {
            var result = await _mediator.Send(new GetAllBillsQuery());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBill()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _mediator.Send(new GetBillQuery(userId));
            return Ok(result);
        }
    }
}
