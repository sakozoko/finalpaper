using AutoMapper;
using FluentValidation;
using MediatR;
using WebApiApplication.Context;
using WebApiApplication.Features.PublicNewFeatures.Dto;
using WebApiCore.Models;

namespace WebApiApplication.Features.PublicNewFeatures.Commands;

public class CreatePublicNewCommand : IRequest<PublicNewDto>
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public Guid UserId { get; set; }
    public string Author { get; set; } = default!;

    public class CreatePublicNewCommandValidator : AbstractValidator<CreatePublicNewCommand>
    {
        public CreatePublicNewCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100).MinimumLength(10);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(5000).MinimumLength(50);
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Author).NotEmpty().MaximumLength(30);
        }
    }
    public class CreatePublicNewCommandHandler : IRequestHandler<CreatePublicNewCommand, PublicNewDto>
    {
        private readonly IWebApiDbContext _context;
        private readonly IMapper _mapper;

        public CreatePublicNewCommandHandler(IWebApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PublicNewDto> Handle(CreatePublicNewCommand request, CancellationToken cancellationToken)
        {
            var publicNew = _mapper.Map<PublicNewEntity>(request);
            publicNew.CreatedAt = DateTime.UtcNow;
            publicNew.IsDeleted = false;
            _context.PublicNews.Add(publicNew);
            await _context.SaveChangesAsync();
            return _mapper.Map<PublicNewDto>(publicNew);
        }
    }
}