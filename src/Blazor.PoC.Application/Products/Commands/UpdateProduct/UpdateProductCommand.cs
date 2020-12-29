using AutoMapper;
using Blazor.PoC.Application.Products.Queries.GetProductDetail;
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

namespace Blazor.PoC.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ProductDetailVm>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDetailVm>
    {
        private readonly IBlazorDbContext _context;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IBlazorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDetailVm> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Discount = request.Discount ?? 0;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductDetailVm>(product);
        }
    }
}
