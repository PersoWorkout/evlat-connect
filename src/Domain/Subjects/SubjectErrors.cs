using Domain.Abstract;

namespace Domain.Subjects
{
    public static class SubjectErrors
    {
        public static readonly Error NameEmpty =
            new("Name.Empty", "The subject name must not be empty.");

        public static Error NotFound(string id) =>
            new("Subject.NotFound", $"The subject with id {id} was not found.");
    }
}
