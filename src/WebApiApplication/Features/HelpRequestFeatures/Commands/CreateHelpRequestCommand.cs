using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using WebApiApplication.Context;
using WebApiCore.Models;

namespace WebApiApplication.Features.HelpRequestFeatures.Commands
{
    public class CreateHelpRequestCommand : IRequest<Guid>
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; }= default!;
        public Guid UserId { get; set; }
        public bool EmailConfirmed { get; set; }

        public class CreateHelpRequestCommandValidator : AbstractValidator<CreateHelpRequestCommand>
        {
            public CreateHelpRequestCommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
                RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
                RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
                RuleFor(x => x.EmailConfirmed).NotEmpty().WithMessage("EmailConfirmed is required")
                .Equal(true).WithMessage("Email must be confirmed");
            }
        }

        public class CreateHelpRequestCommandHandler : IRequestHandler<CreateHelpRequestCommand, Guid>
        {
            private readonly IWebApiDbContext _context;
            private readonly IMapper _mapper;

            public CreateHelpRequestCommandHandler(IWebApiDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper  = mapper;
            }

            public async Task<Guid> Handle(CreateHelpRequestCommand command, CancellationToken cancellationToken)
            {
                var helpRequest = new HelpRequestEntity();
                helpRequest = _mapper.Map<HelpRequestEntity>(command);
                _context.HelpRequests.Add(helpRequest);
                await _context.SaveChangesAsync();
                return helpRequest.Id;
            }
        }
    }
}