using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site.Application.Features.Commands.Payment.PayBill;
using Site.Application.Models.Payment;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Site.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PayBill([FromBody]PaymentModel paymentModel)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _mediator.Send(new PayBillCommand(paymentModel, userId));
            return Ok(result);
        }
    }
}
