using Blazor.PoC.Application.Store.Commands.PlaceOrder;
using Blazor.PoC.Application.Store.Queries.GetMostOrderedProducts;
using Blazor.PoC.Application.Store.Queries.GetOrderDetails;
using Blazor.PoC.Application.Store.Queries.GetOrdersByCountry;
using Blazor.PoC.Application.Store.Queries.GetStoreProducts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.Api.Controllers
{
    public class StoreController : BaseController
    {
        [HttpPost, Route("")]
        public async Task<ActionResult<int>> PlaceOrder(PlaceOrderCommand command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpGet, Route("")]
        public async Task<ActionResult<StoreProductsVm>> GetStoreProducts()
        {
            var vm = await Mediator.Send(new GetStoreProductsQuery());
            return Ok(vm);
        }

        [HttpGet, Route("{id:int}")]
        public async Task<ActionResult<OrderDetailsVm>> GetOrderDetails(int id)
        {
            var vm = await Mediator.Send(new GetOrderDetailsQuery { OrderId = id });
            return Ok(vm);
        }

        [HttpGet, Route("")]
        public async Task<ActionResult<MostOrderedProductsVm>> GetMostOrderedProducts()
        {
            var vm = await Mediator.Send(new GetMostOrderedProductsQuery());
            return Ok(vm);
        }

        [HttpGet, Route("")]
        public async Task<ActionResult<OrdersByCountryVm>> GetOrdersByCountry()
        {
            var vm = await Mediator.Send(new GetOrdersByCountryQuery());
            return Ok(vm);
        }
    }
}
