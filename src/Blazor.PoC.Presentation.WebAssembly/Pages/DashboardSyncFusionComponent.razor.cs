using Blazor.PoC.Presentation.WebAssembly.Api;
using Blazor.PoC.Presentation.WebAssembly.Data;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.WebAssembly.Pages {
    public partial class DashboardSyncFusionComponent {
        [Inject] ApiService Api { get; set; }

        public int StartAngle = 0, value = 0, EndAngle = 360, radiusValue = 70, exploderadius = 10;
        public double ExplodeIndex = 1;
        public string OuterRadius = "70%", ExplodeRadius = "10%";

        public ChartVm ordersByCountryDataSource;
        public ChartVm mostOrderedProductsDataSource;
        public bool isLoaded = false;

        protected override async Task OnInitializedAsync() {
            await SetOrdersByCountry();
            await SetMostOrderedProducts();
            isLoaded = true;
        }

        private async Task SetOrdersByCountry() {
            var ordersByCountry = await Api.Store.GetOrdersByCountryAsync();
            ordersByCountryDataSource = new ChartVm {
                Series = ordersByCountry.Items.Select(x => new ChartVmSeries {
                    Category = x.Country,
                    Quantity = x.Quantity,
                    Value = (double)x.TotalValue
                }).ToList()
            };
        }

        private async Task SetMostOrderedProducts() {
            var mostOrderedProducts = await Api.Store.GetMostOrderedProductsAsync();
            mostOrderedProductsDataSource = new ChartVm {
                Series = mostOrderedProducts.Items.Select(x => new ChartVmSeries {
                    Category = x.Title,
                    Quantity = (double)x.TotalDiscount,
                    Value = (double)x.TotalValue
                }).ToList()
            };
        }
    }
}