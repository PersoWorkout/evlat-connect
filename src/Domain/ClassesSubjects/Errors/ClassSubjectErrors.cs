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

        public static readonly Error InvalidDates =
            new("Dates.Invalid", "The pasted dates are invalid. 'From' must be less than 'To'.");

        public static readonly Error InvalidClassId =
            new("ClassId.Invalid", "The 'classId' format is not valid.");

        public static readonly Error InvalidStartedDateFormat =
            new("StartedDate.Invalid", "The 'startedDate' formta is not valid");

        public static readonly Error ClassSubjectNotFound =
            new("ClassSubject.NotFound", "The requested class subject was not found.");
    }
}
