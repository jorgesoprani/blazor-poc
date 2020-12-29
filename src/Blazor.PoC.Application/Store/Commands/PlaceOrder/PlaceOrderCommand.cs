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

namespace Blazor.PoC.Application.Store.Commands.PlaceOrder
{
    public class PlaceOrderCommand : IRequest<int>
    {
        public int ClientId { get; set; }
        public IList<Item> Items { get; set; }

        public class Item
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }

    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, int>
    {
        private readonly IBlazorDbContext _context;
        public PlaceOrderCommandHandler(IBlazorDbContext context)
        {
            _context = context;
        }


        public async Task<int> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order();
            order.Client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == request.ClientId);

            foreach (var item in request.Items)
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                order.Items.Add(new OrderItem
                {
                    Order = order,
                    Product = product,
                    SubTotal = product.Price * item.Quantity,
                    Total = product.Price * item.Quantity * (1 - (product.Discount / 100)),
                    Quantity = item.Quantity
                });
            }

            _context.Orders.Add(order);

            await _context.SaveChangesAsync(cancellationToken);
            
            return order.Id;
        }
    }
}
