using Blazor.PoC.Application.Store.Commands.PlaceOrder;
using Blazor.PoC.Application.Store.Queries.GetStoreProducts;
using Blazor.PoC.Presentation.Server.Services;
using MediatR;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.Server.Pages
{
    public partial class StoreComponent
    {
        [Inject] private IMediator mediator { get; set; }
        [Inject] private NotificationsService notifications { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }

        public StoreProductsVm model;

        protected int selectedClient;


        protected override async Task OnInitializedAsync()
        {
            await UpdateList();
        }

        private async Task UpdateList()
        {
            model = await mediator.Send(new GetStoreProductsQuery());
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
                await notifications.ShowWarning("You should first 'Login' to place an order");
                return;
            }
            else
            {
                var addedItems = model.Items.Where(x => x.Quantity > 0).ToList();
                if (!addedItems.Any())
                {
                    await notifications.ShowWarning("Please select at least 1 item");
                    return;
                }

                var command = new PlaceOrderCommand()
                {
                    ClientId = selectedClient,
                    Items = addedItems.Select(x => new PlaceOrderCommand.Item()
                    {
                        ProductId = x.Id,
                        Quantity = x.Quantity
                    }).ToList()
                };

                var orderId = await mediator.Send(command);

                navigationManager.NavigateTo($"/ordersuccessful/{orderId}");
            }
        }
    }
}
