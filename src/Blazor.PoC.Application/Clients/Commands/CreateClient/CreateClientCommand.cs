using AutoMapper;
using Blazor.PoC.Application.Clients.Queries.GetClientDetail;
using Blazor.PoC.Domain.Entities;
using Blazor.PoC.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.PoC.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommand : IRequest<ClientDetailVm>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
    }

    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ClientDetailVm>
    {
        private readonly IBlazorDbContext _context;
        private readonly IMapper _mapper;
        public CreateClientCommandHandler(IBlazorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClientDetailVm> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var client = new Client
            {
                Name = request.Name,
                Country = request.Country,
                Email = request.Email,                
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ClientDetailVm>(client);
        }
    }
}
