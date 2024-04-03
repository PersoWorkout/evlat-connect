using Domain.Abstract;

namespace Domain.Classes.Errors
{
    public static class ClassErrors
    {
        public static readonly Error NameEmpty = new("Name.Empty", "'Name' must not be empty.");
        public static readonly Error PromotionEmpty = new("Promotion.Empty", "'Promotion' must not be empty.");
        public static readonly Error TypeNotDefined = new("Role.NotDefined", "'Type' is not a valid class type.");
        public static readonly Error ProfessorIdEmpty = new("ProfessorId.Empty", "'ProfessorId' must not be empty.");
        public static readonly Error ProfessorIdInvalid = new("ProfessorId.Invalid", "'ProfessorId' is not valid.");

        public static Error ClassNotFound(string id) => new("Class.NotFound",
                                                            $"The class with Id {id} was not found");
    }
}
