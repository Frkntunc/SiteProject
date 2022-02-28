using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site.Application.Features.Commands.Users.AddUser;
using Site.Application.Features.Commands.Users.DeleteUser;
using Site.Application.Features.Commands.Users.UpdateUser;
using Site.Application.Features.Queries.Users.GetAllUsers;
using Site.Application.Features.Queries.Users.GetUser;
using System.Threading.Tasks;

namespace Site.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            var result = await _mediator.Send(new GetUserQuery(Id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserCommand addUserCommand)
        {
            var result = await _mediator.Send(addUserCommand);

            if (result!=null)
                return Ok($"Your pin is : {result}");

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand updateUserCommand)
        {
            var result = await _mediator.Send(updateUserCommand);
            return Ok("User updated.");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(Id));
            return Ok("User deleted.");
        }
    }
}
