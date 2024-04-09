using Domain.Abstract;

namespace Domain.Competences.Errors
{
    public static class CompetenceError
    {
        public static readonly Error NameEmpty = 
            new("Name.Empty", "The'Name' must not be empty.");

        public static Error CompetenceNotFound(string id) => 
            new("Competence.NotFound", $"The competence with id {id} was not found.");
        
    }
}
