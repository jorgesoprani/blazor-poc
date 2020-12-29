using AutoMapper;
using Blazor.PoC.Application.Common.Mappings;
using Blazor.PoC.Domain.Entities;

namespace Blazor.PoC.Application.Products.Queries.GetProductList
{
    public class ProductListDto : AuditableEntity, IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductListDto>();
        }
    }
}