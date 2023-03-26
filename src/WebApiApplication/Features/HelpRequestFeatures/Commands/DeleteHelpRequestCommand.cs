using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;
using WebApiApplication.Features.HelpRequestFeatures.Dto;
using WebApiApplication.Shared.Exceptions;
using WebApiCore.Models;

namespace WebApiApplication.Features.HelpRequestFeatures.Commands
{
    public class DeleteHelpRequestCommand : IRequest<HelpRequestDto>
    {
        public Guid Id { get; set; }
        public class DeleteHelpRequestCommandHandler : IRequestHandler<DeleteHelpRequestCommand, HelpRequestDto>
        {
            private readonly IWebApiDbContext _context;
            private readonly IMapper _mapper;
            public DeleteHelpRequestCommandHandler(IWebApiDbContext context, IMapper mapper){
                _context = context;
                _mapper = mapper;
            }
            public async Task<HelpRequestDto> Handle(DeleteHelpRequestCommand request, CancellationToken cancellationToken)
            {
                var helpRequest = await _context.HelpRequests.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if(helpRequest is null)
                    throw new NotFoundException(nameof(HelpRequestEntity), request.Id);
                helpRequest.Status = HelpRequestStatus.Removed;
                await _context.SaveChangesAsync();
                return _mapper.Map<HelpRequestDto>(helpRequest);
            }
        }
    }   
}