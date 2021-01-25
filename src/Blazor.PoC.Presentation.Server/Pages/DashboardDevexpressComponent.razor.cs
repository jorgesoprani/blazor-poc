using Blazor.PoC.Application.Store.Queries.GetMostOrderedProducts;
using Blazor.PoC.Application.Store.Queries.GetOrdersByCountry;
using Blazor.PoC.Presentation.Server.Data;
using MediatR;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.Server.Pages {
    public partial class DashboardDevexpressComponent {
        [Inject] IMediator mediator { get; set; }

        public ChartVm ordersByCountryDataSource;
        public ChartVm mostOrderedProductsDataSource;
        public bool isLoaded = false;

        protected override async Task OnInitializedAsync() {
            await SetOrdersByCountry();
            await SetMostOrderedProducts();
            isLoaded = true;
        }

        private async Task SetOrdersByCountry() {
            var ordersByCountry = await mediator.Send(new GetOrdersByCountryQuery());
            ordersByCountryDataSource = new ChartVm {
                Series = ordersByCountry.Items.Select(x => new ChartVmSeries {
                    Category = x.Country,
                    Quantity = x.Quantity,
                    Value = (double)x.TotalValue
                }).ToList()
            };
        }

        private async Task SetMostOrderedProducts() {
            var mostOrderedProducts = await mediator.Send(new GetMostOrderedProductsQuery());
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