using Domain.Abstract;
using Domain.Users;
using Domain.Users.ValueObjects;
using MediatR;

namespace Application.Users.CreateUser
{
    public class CreateUserCommand: IRequest<Result<User>>
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required UserRole Role { get; set; }
        public required PasswordValueObject Password { get; set; }
        public required PhoneNumberValueObject PhoneNumber { get; set; }
    }
}
