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
using static Blazor.PoC.Application.Store.Queries.GetStoreProducts.StoreProductsVm;

namespace Blazor.PoC.Application.Store.Queries.GetStoreProducts
{
    public class GetStoreProductsQuery : IRequest<StoreProductsVm>
    {
    }

    public class GetStoreProductsQueryHandler : IRequestHandler<GetStoreProductsQuery, StoreProductsVm>
    {
        private readonly IBlazorDbContext _context;
        private readonly IMapper _mapper;
        public GetStoreProductsQueryHandler(IBlazorDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StoreProductsVm> Handle(GetStoreProductsQuery request, CancellationToken cancellationToken)
        {
            //TODO: Add paging
            //TODO: Add stock amount
            //TODO: Add quantity to products if saving cart
            var list = await _context.Products
                .ProjectTo<StoreProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var clients = await _context.Clients.Select(x => new FakeAuthentication(x.Id, x.Name)).ToListAsync();

            return new()
            {
                Items = list,
                FakeAuthenticationOptions = clients
            };
        }
    }
}
