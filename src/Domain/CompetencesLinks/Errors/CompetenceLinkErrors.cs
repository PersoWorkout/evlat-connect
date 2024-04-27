using Domain.Abstract;

namespace Domain.CompetencesLinks.Errors
{
    public static class CompetenceLinkErrors
    {
        public static readonly Error NameEmpty = 
            new("Name.Empty", "The 'name' must not be empty");

        public static readonly Error PathEmpty =
            new("Path.Empty", "The 'path' must not be empty");

        public static readonly Error TypeNull =
            new("Type.Null", "The 'type' must not be null");

        public static readonly Error TypeInvalid =
            new("Type.Invalid", "The 'type' is not valid");

        public static readonly Error CompetenceNull =
            new("CompetenceId.Null", "The 'competenceId' must not be null");

        public static readonly Error CompetenceInvalid =
            new("CompetenceId.Invalid", "The 'competenceId' is not valid");

        public static Error CompetenceNotFound(string id) =>
            new("CompetenceId.NotFound", $"The competence with id {id} was not found");
    }
}
