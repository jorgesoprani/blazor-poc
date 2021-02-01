using Blazor.PoC.Presentation.WebAssembly.Api;
using Blazor.PoC.Presentation.WebAssembly.Services;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.WebAssembly.Pages
{
    public partial class ClientFormComponent
    {
        [Inject] private NotificationsService _notifications { get; set; }
        [Inject] private ApiService _api { get; set; }

        [Parameter] public int? ClientId { get; set; }
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }

        public ClientFormData model;
        public string[] CountryOptions = new[] { "Brazil", "Canada", "UK", "Germany", "Mexico", "Sweden" };

        protected override async Task OnInitializedAsync()
        {
            if (ClientId.HasValue)
            {
                var itemToEdit = await _api.Clients.GetByIdAsync(ClientId.Value);
                model = new ClientFormData
                {
                    Name = itemToEdit.Name,
                    Country = itemToEdit.Country,
                    Email = itemToEdit.Email,
                    DateOfBirth = itemToEdit.DateOfBirth
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
                updatedClient = await _api.Clients.UpdateAsync(ClientId.Value, new UpdateClientCommand
                {
                    Id = ClientId.Value,
                    Country = model.Country,
                    Email = model.Email,
                    Name = model.Name,
                    DateOfBirth = model.DateOfBirth
                });
            else
                updatedClient = await _api.Clients.CreateAsync(new CreateClientCommand
                {
                    Country = model.Country,
                    Email = model.Email,
                    Name = model.Name,
                    DateOfBirth = model.DateOfBirth
                });

            await _notifications.ShowInfo("Saved");
            await ModalInstance.Close(ModalResult.Ok(updatedClient));
        }

        public class ClientFormData
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string Country { get; set; }
            [Required, EmailAddress]
            public string Email { get; set; }
            [Required]
            public DateTime DateOfBirth { get; set; }
        }

    }
}
