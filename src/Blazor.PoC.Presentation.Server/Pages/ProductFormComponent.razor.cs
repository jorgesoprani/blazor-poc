using Blazor.PoC.Application.Products.Commands.CreateProduct;
using Blazor.PoC.Application.Products.Commands.UpdateProduct;
using Blazor.PoC.Application.Products.Queries.GetProductDetail;
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
    public partial class ProductFormComponent
    {
        [Inject] private IMediator _mediator { get; set; }
        [Inject] private NotificationsService _notifications { get; set; }

        [Parameter] public int? ProductId { get; set; }
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }

        public ProductDetailVm productDetails;

        protected override async Task OnInitializedAsync()
        {
            if (ProductId.HasValue)
            {
                productDetails = await _mediator.Send(new GetProductDetailQuery { Id = ProductId.Value });
            }
            else
            {
                productDetails = new();
            }
        }

        protected async Task Save()
        {
            ProductDetailVm updatedProduct;
            if (ProductId.HasValue)
                updatedProduct = await _mediator.Send(new UpdateProductCommand
                {
                    Id = ProductId.Value,
                    Name = productDetails.Name,
                    Description = productDetails.Description,
                    Discount = productDetails.Discount,
                    Price = productDetails.Price,
                });
            else
                updatedProduct = await _mediator.Send(new CreateProductCommand
                {
                    Name = productDetails.Name,
                    Description = productDetails.Description,
                    Discount = productDetails.Discount,
                    Price = productDetails.Price,
                });

            productDetails = updatedProduct;

            await _notifications.ShowInfo("Saved");
            await ModalInstance.CloseAsync(ModalResult.Ok(updatedProduct));
        }

    }
}
