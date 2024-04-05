using Domain.Abstract;
using Domain.Users;
using Domain.Users.ValueObjects;
using MediatR;

namespace Application.Users.UpdateUser
{
    public class UpdateUserCommand: IRequest<Result<User>>
    {
        public Guid UserId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Username { get; set; }
        public PasswordValueObject? Password { get; set; }
        public PhoneNumberValueObject? PhoneNumber { get; set; }
        public Guid? ClassId { get; set; }
    }
}