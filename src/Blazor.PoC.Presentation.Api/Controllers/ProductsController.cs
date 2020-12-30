using Blazor.PoC.Application.Products.Commands.CreateProduct;
using Blazor.PoC.Application.Products.Commands.DeleteProduct;
using Blazor.PoC.Application.Products.Commands.UpdateProduct;
using Blazor.PoC.Application.Products.Queries.GetProductDetail;
using Blazor.PoC.Application.Products.Queries.GetProductList;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.Api.Controllers
{
    public class ProductsController : BaseController
    {
        [HttpGet, Route("{id:int}")]
        public async Task<ActionResult<ProductDetailVm>> GetById(int id)
        {
            var product = await Mediator.Send(new GetProductDetailQuery() { Id = id });
            return Ok(product);
        }

        [HttpGet, Route("")]
        public async Task<ActionResult<ProductListVm>> GetAll()
        {
            var products = await Mediator.Send(new GetProductListQuery());
            return Ok(products);
        }

        [HttpPost, Route("")]
        public async Task<ActionResult<ProductDetailVm>> Create(CreateProductCommand command)
        {
            var product = await Mediator.Send(command);
            return Ok(product);
        }

        [HttpPut, Route("{id:int}")]
        public async Task<ActionResult<ProductDetailVm>> Update(int id, UpdateProductCommand command)
        {
            if (id != command?.Id)
                return BadRequest("Id must be the same from body");

            var product = await Mediator.Send(command);
            return Ok(product);
        }

        [HttpDelete, Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProductCommand { Id = id });
            return Ok();
        }
    }
}
