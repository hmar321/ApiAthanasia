﻿using ApiAthanasia.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAthanasia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private RepositoryAthanasia repo;

        public PedidosController(RepositoryAthanasia repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// METODO PROTEGIDO Cancela un pedido existente.
        /// </summary>
        /// <param name="idpedido">Id del pedido que se desea cancelar.</param>
        /// <response code="204">NoContent. El pedido ha sido cancelado correctamente.</response>
        /// <response code="404">NotFound. No se ha encontrado el pedido especificado.</response>
        /// <response code="401">Unauthorized. No se ha proporcionado un token válido.</response>
        [Authorize]
        [HttpPut]
        [Route("[action]/{idpedido}")]
        public async Task<ActionResult> CancelarPedido(int idpedido)
        {
            int result = await this.repo.UpdatePedidoEstadoCancelarAsync(idpedido);
            if (result == -1)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
