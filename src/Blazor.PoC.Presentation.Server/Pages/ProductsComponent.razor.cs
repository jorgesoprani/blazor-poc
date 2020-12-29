using Blazor.PoC.Application.Products.Commands.DeleteProduct;
using Blazor.PoC.Application.Products.Queries.GetProductDetail;
using Blazor.PoC.Application.Products.Queries.GetProductList;
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
    public partial class ProductsComponent
    {
        [Inject] private IMediator mediator { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }

        [CascadingParameter] public IModalService Modal { get; set; }
        public ProductListVm model;

        protected override async Task OnInitializedAsync()
        {
            await UpdateList();
        }

        private async Task UpdateList()
        {
            model = await mediator.Send(new GetProductListQuery());
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

            await mediator.Send(new DeleteProductCommand { Id = id });

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
