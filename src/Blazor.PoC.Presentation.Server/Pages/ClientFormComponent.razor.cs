using Blazor.PoC.Application.Clients.Commands.CreateClient;
using Blazor.PoC.Application.Clients.Commands.UpdateClient;
using Blazor.PoC.Application.Clients.Queries.GetClientDetail;
using Blazor.PoC.Presentation.Server.Services;
using Blazored.Modal;
using Blazored.Modal.Services;
using MediatR;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.Server.Pages
{
    public partial class ClientFormComponent
    {
        [Inject] private IMediator _mediator { get; set; }
        [Inject] private NotificationsService _notifications { get; set; }

        [Parameter] public int? ClientId { get; set; }
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }

        public ClientDetailVm clientDetails;
        public string[] CountryOptions = new[] { "Brazil", "Canada", "UK", "Germany", "Mexico", "Sweden" };

        protected override async Task OnInitializedAsync()
        {
            if (ClientId.HasValue)
            {
                clientDetails = await _mediator.Send(new GetClientDetailQuery { Id = ClientId.Value });
            }
            else
            {
                clientDetails = new();
            }
        }

        protected async Task Save()
        {
            ClientDetailVm updatedClient;
            if (ClientId.HasValue)
                updatedClient = await _mediator.Send(new UpdateClientCommand
                {
                    Id = ClientId.Value,
                    Country = clientDetails.Country,
                    Email = clientDetails.Email,
                    Name = clientDetails.Name
                });
            else
                updatedClient = await _mediator.Send(new CreateClientCommand
                {
                    Country = clientDetails.Country,
                    Email = clientDetails.Email,
                    Name = clientDetails.Name
                });

            clientDetails = updatedClient;

            await _notifications.ShowInfo("Saved");
            await ModalInstance.Close(ModalResult.Ok(updatedClient));
        }

    }
}
