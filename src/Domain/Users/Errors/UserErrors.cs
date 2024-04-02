using Domain.Abstract;

namespace Domain.Users.Errors
{
    public static class UserErrors
    {
        public static readonly Error FirstnameEmpty = new("Firstname.Empty", "'Firstname' must not be empty.");
        public static readonly Error LastnameEmpty = new("Lastname.Empty", "'Lastname' must not be empty.");
        public static readonly Error RoleNotDefined = new("Role.NotDefined", "The value is not a valid user role");
    }
}