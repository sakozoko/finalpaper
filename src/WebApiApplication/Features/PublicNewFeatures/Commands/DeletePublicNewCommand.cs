using AutoMapper;
using FluentValidation;
using MediatR;
using WebApiApplication.Context;
using WebApiApplication.Features.PublicNewFeatures.Dto;
using WebApiApplication.Shared.Exceptions;
using WebApiCore.Models;

namespace WebApiApplication.Features.PublicNewFeatures.Commands;

public class DeletePublicNewCommand : IRequest<PublicNewDto>
{
    public Guid Id { get; set; }
    
    public class DeletePublicNewCommandValidator : AbstractValidator<DeletePublicNewCommand>
    {
        public DeletePublicNewCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
    
    public class DeletePublicNewCommandHandler : IRequestHandler<DeletePublicNewCommand, PublicNewDto>
    {
        private readonly IWebApiDbContext _context;
        private readonly IMapper _mapper;

        public DeletePublicNewCommandHandler(IWebApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PublicNewDto> Handle(DeletePublicNewCommand request, CancellationToken cancellationToken)
        {
            var publicNew = await _context.PublicNews.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
            if (publicNew == null) throw new NotFoundException(nameof(PublicNewEntity), request.Id);
            publicNew.IsDeleted = true;
            await _context.SaveChangesAsync();
            return _mapper.Map<PublicNewDto>(publicNew);
        }
    }
}