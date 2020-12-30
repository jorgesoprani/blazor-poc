using Blazor.PoC.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.PoC.Application.Store.Queries.GetMostOrderedProducts
{
    public class GetMostOrderedProductsQuery : IRequest<MostOrderedProductsVm>
    {
    }

    public class GetMostOrderedProductsQueryHandler : IRequestHandler<GetMostOrderedProductsQuery, MostOrderedProductsVm>
    {
        private readonly IBlazorDbContext _context;
        public GetMostOrderedProductsQueryHandler(IBlazorDbContext context)
        {
            _context = context;
        }

        public async Task<MostOrderedProductsVm> Handle(GetMostOrderedProductsQuery request, CancellationToken cancellationToken)
        {
            var ordersData = await _context.OrderItems
                .GroupBy(x => new { x.Product.Id, x.Product.Name })
                .Select(x => new MostOrderedProductsDto
                {
                    Title = $"{x.Key.Id} | {x.Key.Name}",
                    TotalDiscount = x.Sum(x => x.SubTotal - x.Total),
                    TotalValue = x.Sum(x => x.Total)
                }).ToListAsync();

            return new()
            {
                Items = ordersData
            };
        }
    }
}
