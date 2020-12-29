using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.PoC.Application.Store.Queries.GetStoreProducts
{
    public class StoreProductsVm
    {
        //TODO: Add paging?
        public IList<StoreProductDto> Items { get; set; }
        public IList<FakeAuthentication> FakeAuthenticationOptions { get; set; }

        public class FakeAuthentication
        {
            public FakeAuthentication(int clientId, string clientName)
            {
                ClientId = clientId;
                ClientName = clientName;
            }

            public int ClientId { get; set; }
            public string ClientName { get; set; }
        }
    }

}
