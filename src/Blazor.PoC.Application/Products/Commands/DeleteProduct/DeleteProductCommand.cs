using Blazor.PoC.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.PoC.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IBlazorDbContext _context;
        public DeleteProductCommandHandler(IBlazorDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var Product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
            _context.Products.Remove(Product);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
