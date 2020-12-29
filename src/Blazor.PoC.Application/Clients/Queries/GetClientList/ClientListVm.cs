using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.PoC.Application.Clients.Queries.GetClientList
{
    public class ClientListVm
    {
        public IList<ClientListDto> Items { get; set; }
    }
}
