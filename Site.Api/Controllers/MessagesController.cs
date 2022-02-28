using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site.Application.Features.Commands.Messages.SendMessage;
using Site.Application.Features.Queries.Messages.GetMessage;
using Site.Application.Models.Message;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Site.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _mediator.Send(new GetMessageQuery(userId));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageModel messageModel)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _mediator.Send(new SendMessageCommand(userId,messageModel));
            return Ok("Message sent.");
        }
    }
}
