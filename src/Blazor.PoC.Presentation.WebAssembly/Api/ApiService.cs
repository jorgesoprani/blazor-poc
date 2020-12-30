using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.WebAssembly.Api
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public ClientsApi _clients;
        public ClientsApi Clients => _clients ??= new ClientsApi(_httpClient);

        public ProductsApi _products;
        public ProductsApi Products => _products ??= new ProductsApi(_httpClient);


        public StoreApi _store;
        public StoreApi Store => _store ??= new StoreApi(_httpClient);
    }
}
