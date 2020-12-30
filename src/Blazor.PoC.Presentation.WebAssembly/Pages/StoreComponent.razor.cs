using Blazor.PoC.Presentation.WebAssembly.Api;
using Blazor.PoC.Presentation.WebAssembly.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.WebAssembly.Pages
{
    public partial class StoreComponent
    {
        [Inject] private ApiService _api { get; set; }
        [Inject] private NotificationsService _notifications { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }

        public StoreProductsVm model;

        protected int selectedClient;


        protected override async Task OnInitializedAsync()
        {
            await UpdateList();
        }

        private async Task UpdateList()
        {
            model = await _api.Store.GetStoreProductsAsync();
        }

        public void ChangeQuantity(StoreProductDto item, bool add)
        {
            if (add)
                item.Quantity += 1;
            else
            {
                if (item.Quantity > 0)
                    item.Quantity -= 1;
                else
                    item.Quantity = 0;
            }
        }

        public async Task PlaceOrderAsync()
        {
            if (selectedClient == 0)
            {
                await _notifications.ShowWarning("You should first 'Login' to place an order");
                return;
            }
            else
            {
                var addedItems = model.Items.Where(x => x.Quantity > 0).ToList();
                if (!addedItems.Any())
                {
                    await _notifications.ShowWarning("Please select at least 1 item");
                    return;
                }

                var command = new PlaceOrderCommand()
                {
                    ClientId = selectedClient,
                    Items = addedItems.Select(x => new Item()
                    {
                        ProductId = x.Id,
                        Quantity = x.Quantity
                    }).ToList()
                };

                var orderId = await _api.Store.PlaceOrderAsync(command);

                navigationManager.NavigateTo($"/ordersuccessful/{orderId}");
            }
        }
    }
}
