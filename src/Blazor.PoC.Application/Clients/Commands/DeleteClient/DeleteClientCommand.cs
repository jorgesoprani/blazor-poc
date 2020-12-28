using Blazor.PoC.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.PoC.Application.Clients.Commands.DeleteClient
{
    public class DeleteClientCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
    {
        private readonly IBlazorDbContext _context;
        public DeleteClientCommandHandler(IBlazorDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == request.Id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
