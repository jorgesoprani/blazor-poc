using AutoMapper;
using Blazor.PoC.Application.Common.Mappings;
using Blazor.PoC.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.PoC.Application.Store.Queries.GetOrderDetails
{
    public class OrderDetailsDto : IMapFrom<OrderItem>
    {
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OriginalUnitPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal TotalPrice { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderItem, OrderDetailsDto>()
                .ForMember(x => x.Title, opt => opt.MapFrom(s => s.Product.Name))
                .ForMember(x => x.UnitPrice, opt => opt.MapFrom(s => s.Total / s.Quantity))
                .ForMember(x => x.OriginalUnitPrice, opt => opt.MapFrom(s => s.SubTotal / s.Quantity))
                .ForMember(x => x.TotalPrice, opt => opt.MapFrom(s => s.Total))
                .ForMember(x => x.OriginalPrice, opt => opt.MapFrom(s => s.SubTotal));
        }
    }
}