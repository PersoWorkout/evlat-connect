using Domain.Abstract;
using MediatR;

namespace Application.ClassesSubjects.DeleteClassSubject
{
    public class DeleteClassSubjectCommand
        (Guid id, 
        DateTime startedDate): IRequest<Result<object>>
    {
        public Guid Id { get; set; } = id;
        public DateTime StartedDate { get; set; } = startedDate;
    }
}
