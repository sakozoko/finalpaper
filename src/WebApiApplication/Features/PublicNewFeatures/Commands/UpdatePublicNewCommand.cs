using AutoMapper;
using FluentValidation;
using MediatR;
using WebApiApplication.Context;
using WebApiApplication.Features.PublicNewFeatures.Dto;
using WebApiApplication.Shared.Exceptions;
using WebApiCore.Models;

namespace WebApiApplication.Features.PublicNewFeatures.Commands;

public class UpdatePublicNewCommand : IRequest<PublicNewDto>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public Guid UserId { get; set; }
    public string Author { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    
    public class EditPublicNewCommandValidator : AbstractValidator<UpdatePublicNewCommand>
    {
        public EditPublicNewCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100).MinimumLength(10);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(5000).MinimumLength(50);
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Author).NotEmpty().MaximumLength(30);
            RuleFor(x => x.CreatedAt).NotEmpty();
        }
    }
    
    public class EditPublicNewCommandHandler : IRequestHandler<UpdatePublicNewCommand, PublicNewDto>
    {
        private readonly IWebApiDbContext _context;
        private readonly IMapper _mapper;

        public EditPublicNewCommandHandler(IWebApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PublicNewDto> Handle(UpdatePublicNewCommand request, CancellationToken cancellationToken)
        {
            var publicNew = await _context.PublicNews.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
            if (publicNew == null) throw new NotFoundException(nameof(PublicNewEntity), request.Id);
            publicNew.Title = request.Title;
            publicNew.Description = request.Description;
            publicNew.ImageUrl = request.ImageUrl;
            publicNew.Author = request.Author;
            publicNew.CreatedAt = request.CreatedAt;
            publicNew.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return _mapper.Map<PublicNewDto>(publicNew);
        }
    }
}