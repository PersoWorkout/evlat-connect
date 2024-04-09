using Domain.Abstract;

namespace Domain.Competences.Errors
{
    public static class CompetenceErrors
    {
        public static readonly Error NameEmpty = 
            new("Name.Empty", "The'Name' must not be empty.");

        public static readonly Error SubjectIdNull =
            new("SubjectId.Null", "The 'SubjectId' must not be null.");

        public static readonly Error SubjectIdInvalid =
            new("SubjectId.Invalid", "The 'SubjectId' is not valid.");

        public static Error CompetenceNotFound(string id) => 
            new("Competence.NotFound", $"The competence with id {id} was not found.");

        
    }
}
