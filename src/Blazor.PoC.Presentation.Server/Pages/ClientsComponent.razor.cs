using Blazor.PoC.Application.Clients.Commands.DeleteClient;
using Blazor.PoC.Application.Clients.Queries.GetClientDetail;
using Blazor.PoC.Application.Clients.Queries.GetClientList;
using Blazored.Modal;
using Blazored.Modal.Services;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.Server.Pages
{
    public partial class ClientsComponent
    {
        [Inject] private IMediator mediator { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }

        [CascadingParameter] public IModalService Modal { get; set; }
        public ClientListVm model;

        protected override async Task OnInitializedAsync()
        {
            await UpdateList();
        }

        private async Task UpdateList()
        {
            model = await mediator.Send(new GetClientListQuery());
        }

        protected async Task ShowForm(int? id)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(ClientFormComponent.ClientId), id);
            var modalRef = Modal.Show<ClientFormComponent>("Editing", parameters);

            var result = await modalRef.Result;

            OnSaved(id, result);
        }

        protected async Task Delete(int id)
        {
            var confirmed = await JSRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to delete the client '{id}'?");
            if (!confirmed)
                return;

            await mediator.Send(new DeleteClientCommand { Id = id });

            var deletedClient = model.Items.FirstOrDefault(x => x.Id == id);
            model.Items.Remove(deletedClient);

        }

        protected void OnSaved(int? id, ModalResult result)
        {
            if (result.Cancelled)
            {
                //TODO
            }
            else
            {
                var updatedClient = model.Items.FirstOrDefault(x => x.Id == id);
                var resultData = result.Data as ClientDetailVm;

                if (updatedClient == null)
                {
                    updatedClient = new();
                    updatedClient.Id = resultData.Id;
                    model.Items.Add(updatedClient);
                }
                updatedClient.Name = resultData.Name;
                updatedClient.Country = resultData.Country;
                updatedClient.Created = resultData.Created;
                updatedClient.LastModified = resultData.LastModified;
            }
        }
    }
}
