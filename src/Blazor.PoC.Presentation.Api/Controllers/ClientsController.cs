using Blazor.PoC.Application.Clients.Commands.CreateClient;
using Blazor.PoC.Application.Clients.Commands.DeleteClient;
using Blazor.PoC.Application.Clients.Commands.UpdateClient;
using Blazor.PoC.Application.Clients.Queries.GetClientDetail;
using Blazor.PoC.Application.Clients.Queries.GetClientList;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.Api.Controllers
{
    public class ClientsController : BaseController
    {
        [HttpGet, Route("{id:int}")]
        public async Task<ActionResult<ClientDetailVm>> GetById(int id)
        {
            var client = await Mediator.Send(new GetClientDetailQuery() { Id = id });
            return Ok(client);
        }

        [HttpGet, Route("")]
        public async Task<ActionResult<ClientListVm>> GetAll()
        {
            var clients = await Mediator.Send(new GetClientListQuery());
            return Ok(clients);
        }

        [HttpPost, Route("")]
        public async Task<ActionResult<ClientDetailVm>> Create(CreateClientCommand command)
        {
            var client = await Mediator.Send(command);
            return Ok(client);
        }

        [HttpPut, Route("{id:int}")]
        public async Task<ActionResult<ClientDetailVm>> Update(int id, UpdateClientCommand command)
        {
            if (id != command?.Id)
                return BadRequest("Id must be the same from body");

            var client = await Mediator.Send(command);
            return Ok(client);
        }


        [HttpDelete, Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteClientCommand { Id = id });
            return Ok();
        }
    }
}
