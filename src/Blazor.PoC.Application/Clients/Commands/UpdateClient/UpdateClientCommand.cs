using AutoMapper;
using Blazor.PoC.Application.Clients.Queries.GetClientDetail;
using Blazor.PoC.Domain.Entities;
using Blazor.PoC.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.PoC.Application.Clients.Commands.UpdateClient
{
    public class UpdateClientCommand : IRequest<ClientDetailVm>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ClientDetailVm>
    {
        private readonly IBlazorDbContext _context;
        private readonly IMapper _mapper;
        public UpdateClientCommandHandler(IBlazorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClientDetailVm> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == request.Id);
            client.Name = request.Name;
            client.Country = request.Country;
            client.Email = request.Email;
            client.DateOfBirth = request.DateOfBirth;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ClientDetailVm>(client);
        }
    }
}
