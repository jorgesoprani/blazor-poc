using Blazor.PoC.Application.Common.Mappings;
using Blazor.PoC.Domain.Entities;

namespace Blazor.PoC.Application.Store.Queries.GetMostOrderedProducts
{
    public class MostOrderedProductsDto
    {
        public string Title { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalValue { get; set; }
    }
}
