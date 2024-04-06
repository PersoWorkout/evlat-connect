using Domain.Abstract;

namespace Domain.ClassesSubjects.Errors
{
    public static class ClassSubjectErrors
    {
        public static readonly Error StartingDateNull = 
            new("StartingDate.Null", "The starting date must not be null.");

        public static readonly Error FinishDateNull =
            new("StartingDate.Null", "The finish date must not be null.");

        public static readonly Error FinishDateInvalid =
            new("StartingDate.Null", "The finish date must be greater than starting date.");

        public static readonly Error CoursAlreadyExist =
            new("ClassSubject.AlreadyExist", "A cours already exist for this class and this period.");
    }
}
