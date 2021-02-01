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
using System.ComponentModel.DataAnnotations;
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

        public ClientFormData model;
        public string[] CountryOptions = new[] { "Brazil", "Canada", "UK", "Germany", "Mexico", "Sweden" };

        protected override async Task OnInitializedAsync()
        {
            if (ClientId.HasValue)
            {
                var itemToEdit = await _mediator.Send(new GetClientDetailQuery { Id = ClientId.Value });
                model = new ClientFormData
                {
                    Name = itemToEdit.Name,
                    Country = itemToEdit.Country,
                    Email = itemToEdit.Email,
                };
            }
            else
            {
                model = new();
            }
        }

        protected async Task Save()
        {
            ClientDetailVm updatedClient;
            if (ClientId.HasValue)
                updatedClient = await _mediator.Send(new UpdateClientCommand
                {
                    Id = ClientId.Value,
                    Country = model.Country,
                    Email = model.Email,
                    Name = model.Name
                });
            else
                updatedClient = await _mediator.Send(new CreateClientCommand
                {
                    Country = model.Country,
                    Email = model.Email,
                    Name = model.Name
                });

            await _notifications.ShowInfo("Saved");
            await ModalInstance.CloseAsync(ModalResult.Ok(updatedClient));
        }

        public class ClientFormData
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string Country { get; set; }
            [Required, EmailAddress]
            public string Email { get; set; }
        }

    }
}
