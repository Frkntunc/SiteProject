using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Features.Commands.CreditCards.AddCreditCard;
using System.Threading.Tasks;

namespace PaymentService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CreditCardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddCreditCard([FromBody]AddCreditCardCommand addCreditCardCommand)
        {
            return Ok(_mediator.Send(addCreditCardCommand));
        }
    }
}
