using Blazor.PoC.Presentation.WebAssembly.Api;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.WebAssembly.Pages
{
    public partial class ProductsComponent
    {
        [Inject] private ApiService _api { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }

        [CascadingParameter] public IModalService Modal { get; set; }
        public ProductListVm model;

        protected override async Task OnInitializedAsync()
        {
            await UpdateList();
        }

        private async Task UpdateList()
        {
            model = await _api.Products.GetAllAsync(); ;
        }

        protected async Task ShowForm(int? id)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(ProductFormComponent.ProductId), id);
            var modalRef = Modal.Show<ProductFormComponent>(id.HasValue ? "Editing" : "New Product", parameters);

            var result = await modalRef.Result;

            OnSaved(id, result);
        }

        protected async Task Delete(int id)
        {
            var confirmed = await JSRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to delete the Product '{id}'?");
            if (!confirmed)
                return;

            await _api.Products.DeleteAsync(id);

            var deletedProduct = model.Items.FirstOrDefault(x => x.Id == id);
            model.Items.Remove(deletedProduct);

        }

        protected void OnSaved(int? id, ModalResult result)
        {
            if (result.Cancelled)
            {
                //TODO
            }
            else
            {
                var updatedProduct = model.Items.FirstOrDefault(x => x.Id == id);
                var resultData = result.Data as ProductDetailVm;

                if (updatedProduct == null)
                {
                    updatedProduct = new();
                    updatedProduct.Id = resultData.Id;
                    model.Items.Add(updatedProduct);
                }
                updatedProduct.Name = resultData.Name;
                updatedProduct.Price = resultData.Price;
                updatedProduct.Discount = resultData.Discount;
                updatedProduct.Created = resultData.Created;
                updatedProduct.LastModified = resultData.LastModified;
            }
        }
    }
}
