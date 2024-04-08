using Domain.Abstract;
using Domain.ClassesSubjects.Errors;
using MediatR;

namespace Application.ClassesSubjects.DeleteClassSubject
{
    public class DeleteClassSubjectAction(ISender sender)
    {
        private readonly ISender _sender = sender;

        public async Task<Result<object>> Execute(string classId, string stringDate)
        {
            if (!Guid.TryParse(classId, out Guid parsedClassId))
                return Result<object>.Failure(
                    ClassSubjectErrors.InvalidClassId);

            if (!DateTime.TryParse(stringDate, out DateTime parsedDate))
                return Result<object>.Failure(
                    ClassSubjectErrors.InvalidDates);

            var command = new DeleteClassSubjectCommand(parsedClassId, parsedDate);

            return await _sender.Send(command);
        }
    }
}
