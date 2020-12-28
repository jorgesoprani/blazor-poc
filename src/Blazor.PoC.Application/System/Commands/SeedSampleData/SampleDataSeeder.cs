using Blazor.PoC.Domain.Entities;
using Blazor.PoC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor.PoC.Application.System.Commands.SeedSampleData
{
    public class SampleDataSeeder
    {
        private readonly IBlazorDbContext _context;

        private readonly Dictionary<int, Client> Clients = new Dictionary<int, Client>();

        public SampleDataSeeder(IBlazorDbContext context)
        {
            _context = context;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (_context.Clients.Any())
            {
                return;
            }

            await SeedCustomersAsync(cancellationToken);
        }

        private async Task SeedCustomersAsync(CancellationToken cancellationToken)
        {
            var customers = new[]
            {
                new Client { Name = "Maria Anders", Country = "Germany", Email = "maria.anders@blazorpoc.com" },
                new Client { Name = "Ana Trujillo Emparedados y helados", Country = "Mexico", Email = "ana.trujillo@blazorpoc.com" },
                new Client { Name = "Antonio Moreno Taquería", Country = "Mexico", Email = "antonio.moreno@blazorpoc.com" },
                new Client { Name = "Around the Horn", Country = "UK", Email = "around.horn@blazorpoc.com" },
                new Client { Name = "Berglunds snabbköp", Country = "Sweden", Email = "berglunds.snabbkop@blazorpoc.com" },
            };

            _context.Clients.AddRange(customers);

            await _context.SaveChangesAsync(cancellationToken);
        }

        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }

    //internal static class OrderExtensions
    //{
    //    public static Order AddOrderDetails(this Order order, params OrderDetail[] orderDetails)
    //    {
    //        foreach (var orderDetail in orderDetails)
    //        {
    //            order.OrderDetails.Add(orderDetail);
    //        }

    //        return order;
    //    }
    //}
}
