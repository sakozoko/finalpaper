using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;
using WebApiApplication.Features.HelpRequestFeatures.Dto;
using WebApiApplication.Shared.Exceptions;
using WebApiCore.Models;

namespace WebApiApplication.Features.HelpRequestFeatures.Commands
{
    public class AnswerToHelpRequestCommand : IRequest<HelpRequestDto>
    {
        public Guid Id { get; set; }
        public string Answer { get; set; } = string.Empty;

        public class AnswerToHelpRequestCommandValidator : AbstractValidator<AnswerToHelpRequestCommand>
        {
            public AnswerToHelpRequestCommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
                RuleFor(x => x.Answer).NotEmpty().WithMessage("Answer is required")
                    .MinimumLength(50)
                    .MaximumLength(5000);
            }
        }

        public class AnswerToHelpRequestCommandHandler : IRequestHandler<AnswerToHelpRequestCommand, HelpRequestDto>
        {
            private readonly IWebApiDbContext _context;
            private readonly IMapper _mapper;

            public AnswerToHelpRequestCommandHandler(IWebApiDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<HelpRequestDto> Handle(AnswerToHelpRequestCommand request,
                CancellationToken cancellationToken)
            {
                var helpRequest = await _context.HelpRequests.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (helpRequest is null)
                    throw new NotFoundException(nameof(HelpRequestEntity), request.Id);
                helpRequest.Answer = request.Answer;
                helpRequest.Status = HelpRequestStatus.Processed;
                await _context.SaveChangesAsync();
                return _mapper.Map<HelpRequestDto>(helpRequest);
            }
        }
    }
}