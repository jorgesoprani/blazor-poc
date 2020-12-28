using AutoMapper;
using Blazor.PoC.Application.Common.Mappings;
using Blazor.PoC.Domain.Entities;

namespace Blazor.PoC.Application.Clients.Queries.GetClientList
{
    public class ClientListDto : AuditableEntity, IMapFrom<Client>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        //Could be ommited as there is no changes from original props
        //Could be useful as in commented example
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, ClientListDto>()
            //    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClientId));
            ;
        }
    }
}