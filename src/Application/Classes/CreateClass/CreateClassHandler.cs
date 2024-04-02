using AutoMapper;
using Domain.Abstract;
using Domain.Classes;
using MediatR;

namespace Application.Classes.CreateClass
{
    public class CreateClassHandler(
        IClassRepository repository, 
        IMapper mapper) : IRequestHandler<CreateClassCommand, Result<Class>>
    {
        private readonly IClassRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<Class>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
        {
            Class classEntity = _mapper.Map<Class>(request);

            await _repository.Create(classEntity);

            return Result<Class>.Success(classEntity);
        }
    }
}
