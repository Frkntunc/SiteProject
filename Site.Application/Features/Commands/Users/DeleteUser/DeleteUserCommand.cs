using MediatR;

namespace Site.Application.Features.Commands.Users.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public DeleteUserCommand(int Id)
        {
            ID = Id;
        }
        public int ID { get; set; }
    }
}
