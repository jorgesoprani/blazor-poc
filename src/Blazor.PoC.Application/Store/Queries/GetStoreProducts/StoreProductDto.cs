using AutoMapper;
using Blazor.PoC.Application.Common.Mappings;
using Blazor.PoC.Domain.Entities;

namespace Blazor.PoC.Application.Store.Queries.GetStoreProducts
{
    public class StoreProductDto : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal ItemWithDiscount => Price - (Price * Discount / 100);
        public decimal Discount { get; set; }
        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, StoreProductDto>();
        }
    }
}