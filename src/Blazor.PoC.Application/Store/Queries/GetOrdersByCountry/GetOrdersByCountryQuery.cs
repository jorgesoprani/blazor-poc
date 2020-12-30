using Blazor.PoC.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.PoC.Application.Store.Queries.GetOrdersByCountry
{
    public class GetOrdersByCountryQuery : IRequest<OrdersByCountryVm>
    {
    }

    public class GetOrdersByCountryQueryHandler : IRequestHandler<GetOrdersByCountryQuery, OrdersByCountryVm>
    {
        private readonly IBlazorDbContext _context;
        public GetOrdersByCountryQueryHandler(IBlazorDbContext context)
        {
            _context = context;
        }

        public async Task<OrdersByCountryVm> Handle(GetOrdersByCountryQuery request, CancellationToken cancellationToken)
        {
            var ordersData = _context.OrderItems
                .GroupBy(x => new { x.Order.Id, x.Order.Client.Country })
                .Select(x => new
                {
                    OrderId = x.Key.Id,
                    Country = x.Key.Country,
                    OrderTotal = x.Sum(x => x.Total)
                });

            var items = await ordersData
                .GroupBy(x => x.Country)
                .Select(x => new OrdersByCountryDto
                {
                    Country = x.Key,
                    Quantity = x.Count(),
                    TotalValue = x.Sum(x => x.OrderTotal)
                }).ToListAsync();

            return new()
            {
                Items = items
            };
        }
    }
}
