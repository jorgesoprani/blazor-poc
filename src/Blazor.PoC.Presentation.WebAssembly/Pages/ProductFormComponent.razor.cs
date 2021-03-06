﻿using Blazor.PoC.Presentation.WebAssembly.Api;
using Blazor.PoC.Presentation.WebAssembly.Services;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.WebAssembly.Pages
{
    public partial class ProductFormComponent
    {
        [Inject] private ApiService _api { get; set; }
        [Inject] private NotificationsService _notifications { get; set; }

        [Parameter] public int? ProductId { get; set; }
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }

        public ProductDetailVm productDetails;

        protected override async Task OnInitializedAsync()
        {
            if (ProductId.HasValue)
            {
                productDetails = await _api.Products.GetByIdAsync(ProductId.Value);
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
                updatedProduct = await _api.Products.UpdateAsync(ProductId.Value, new UpdateProductCommand
                {
                    Id = ProductId.Value,
                    Name = productDetails.Name,
                    Description = productDetails.Description,
                    Discount = productDetails.Discount,
                    Price = productDetails.Price,
                });
            else
                updatedProduct = await _api.Products.CreateAsync(new CreateProductCommand
                {
                    Name = productDetails.Name,
                    Description = productDetails.Description,
                    Discount = productDetails.Discount,
                    Price = productDetails.Price,
                });

            productDetails = updatedProduct;

            await _notifications.ShowInfo("Saved");
            await ModalInstance.Close(ModalResult.Ok(updatedProduct));
        }

    }
}
