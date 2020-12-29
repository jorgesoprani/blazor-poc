using AutoMapper;
using Blazor.PoC.Application.Common.Mappings;
using Blazor.PoC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.PoC.Application.Store.Queries.GetOrderDetails
{
    public class OrderDetailsVm : IMapFrom<Order>
    {
        public int OrderId { get; set; }
        public string ClientName { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Total { get; set; }
        public decimal Discounts => (SubTotal ?? 0) - (Total ?? 0);
        public IList<OrderDetailsDto> Items { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDetailsVm>()
                .ForMember(x => x.ClientName, opt => opt.MapFrom(s => s.Client.Name))
                .ForMember(x => x.OrderId, opt => opt.MapFrom(s => s.Id))
                .ForMember(x => x.SubTotal, opt => opt.MapFrom(s => s.Items == null ? new decimal?() : s.Items.Sum(x => x.SubTotal)))
                .ForMember(x => x.Total, opt => opt.MapFrom(s => s.Items == null ? new decimal?() : s.Items.Sum(x => x.Total)));
        }
    }

}
