using Blazor.PoC.Application.Common.Mappings;
using Blazor.PoC.Domain.Entities;

namespace Blazor.PoC.Application.Store.Queries.GetOrdersByCountry
{
    public class OrdersByCountryDto
    {
        public string Country { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue { get; set; }
    }
}
