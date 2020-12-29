using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blazor.PoC.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.PoC.Application.Products.Queries.GetProductList
{
    public class GetProductListQuery : IRequest<ProductListVm>
    {
    }

    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, ProductListVm>
    {
        private readonly IBlazorDbContext _context;
        private readonly IMapper _mapper;
        public GetProductListQueryHandler(IBlazorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductListVm> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.Products
                .ProjectTo<ProductListDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new()
            {
                Items = list
            };
        }
    }
}
