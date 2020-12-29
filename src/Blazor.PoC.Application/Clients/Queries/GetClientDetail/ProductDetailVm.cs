using AutoMapper;
using Blazor.PoC.Application.Common.Mappings;
using Blazor.PoC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.PoC.Application.Clients.Queries.GetClientDetail
{
    public class ClientDetailVm : AuditableEntity, IMapFrom<Client>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, ClientDetailVm>();
        }
    }
}
