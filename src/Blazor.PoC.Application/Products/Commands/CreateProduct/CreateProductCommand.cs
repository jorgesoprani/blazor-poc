using AutoMapper;
using Blazor.PoC.Application.Products.Queries.GetProductDetail;
using Blazor.PoC.Domain.Entities;
using Blazor.PoC.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.PoC.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductDetailVm>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDetailVm>
    {
        private readonly IBlazorDbContext _context;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IBlazorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDetailVm> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Discount = request.Discount ?? 0,
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductDetailVm>(product);
        }
    }
}
