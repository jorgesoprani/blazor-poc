using Blazor.PoC.Presentation.WebAssembly.Api;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.WebAssembly.Pages {
    public partial class ClientsComponent {
        [Inject] private ApiService _api { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }

        private SfGrid<ClientListDto> grid;

        [CascadingParameter] public IModalService Modal { get; set; }
        public ClientListVm model;

        protected override async Task OnInitializedAsync() {
            await UpdateList();
        }

        private async Task UpdateList() {
            model = await _api.Clients.GetAllAsync();
        }

        protected async Task ShowForm(int? id) {
            var parameters = new ModalParameters();
            parameters.Add(nameof(ClientFormComponent.ClientId), id);
            Console.WriteLine("alksdja");
            var modalRef = Modal.Show<ClientFormComponent>("Editing", parameters);

            var result = await modalRef.Result;

            OnSaved(id, result);
        }

        protected async Task OnGridCommand(CommandClickEventArgs<ClientListDto> args) {
            switch (args.CommandColumn.Type) {
                case CommandButtonType.Edit:
                    await ShowForm(args.RowData.Id);
                    break;
                case CommandButtonType.Delete:
                    await Delete(args.RowData.Id);
                    break;
            }
        }

        protected async Task Delete(int id) {
            var confirmed = await JSRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to delete the client '{id}'?");
            if (!confirmed)
                return;

            await _api.Clients.DeleteAsync(id);

            var deletedClient = model.Items.FirstOrDefault(x => x.Id == id);
            model.Items.Remove(deletedClient);

            grid.Refresh();
        }

        protected void OnSaved(int? id, ModalResult result) {
            if (result.Cancelled) {
                //TODO
            } else {
                var updatedClient = model.Items.FirstOrDefault(x => x.Id == id);
                var resultData = result.Data as ClientDetailVm;

                if (updatedClient == null) {
                    updatedClient = new();
                    updatedClient.Id = resultData.Id;
                    model.Items.Add(updatedClient);
                }
                updatedClient.Name = resultData.Name;
                updatedClient.Country = resultData.Country;
                updatedClient.Created = resultData.Created;
                updatedClient.LastModified = resultData.LastModified;

                grid.Refresh();
            }
        }
    }
}
