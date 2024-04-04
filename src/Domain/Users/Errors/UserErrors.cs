using Domain.Abstract;

namespace Domain.Users.Errors
{
    public static class UserErrors
    {
        public static readonly Error FirstnameEmpty = new("Firstname.Empty", "'Firstname' must not be empty.");
        public static readonly Error LastnameEmpty = new("Lastname.Empty", "'Lastname' must not be empty.");
        public static readonly Error RoleNotDefined = new("Role.NotDefined", "The value is not a valid user role");

        public static Error UserNotFound(string id) => new("User.NotFound", $"The user with id {id} was not found");
        public static Error ProfessorNotFound(string id) => new("Professor.NotFound", $"The professor with id {id} was not found");
        public static Error StudentNotFound(string id) => new("Student.NotFound", $"The student with id {id} was not found");

    }
}