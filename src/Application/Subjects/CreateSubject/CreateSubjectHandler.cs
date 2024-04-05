using AutoMapper;
using Domain.Abstract;
using Domain.Subjects;
using MediatR;

namespace Application.Subjects.CreateSubject
{
    public class CreateSubjectHandler(
        ISubjectRepository repository, 
        IMapper mapper) : IRequestHandler<CreateSubjectCommand, Result<Subject>>
    {
        private readonly ISubjectRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<Subject>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _repository.Create(
                _mapper.Map<Subject>(request));

            return Result<Subject>.Success(subject);

        }
    }
}
