using AutoMapper;
using Blazor.PoC.Application.Common.Mappings;
using Blazor.PoC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.PoC.Application.Products.Queries.GetProductDetail
{
    public class ProductDetailVm : AuditableEntity, IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDetailVm>();
        }
    }
}
