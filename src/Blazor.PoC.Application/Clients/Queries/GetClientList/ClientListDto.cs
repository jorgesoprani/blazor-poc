using AutoMapper;
using Blazor.PoC.Application.Common.Mappings;
using Blazor.PoC.Domain.Entities;

namespace Blazor.PoC.Application.Clients.Queries.GetClientList
{
    public class ClientListDto : IMapFrom<Client>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, ClientListDto>();
        }
    }
}