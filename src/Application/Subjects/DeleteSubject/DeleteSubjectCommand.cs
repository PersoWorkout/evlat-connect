using Domain.Abstract;
using MediatR;

namespace Application.Subjects.DeleteSubject
{
    public class DeleteSubjectCommand(Guid id): IRequest<Result<object>>
    {
        public Guid Id { get; set; } = id;
    }
}
