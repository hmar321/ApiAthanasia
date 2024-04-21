using ApiAthanasia.Models.Tables;
using ApiAthanasia.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiAthanasia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoProductosController : ControllerBase
    {
        private IRepositoryAthanasia repo;

        public PedidoProductosController(IRepositoryAthanasia repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// METODO PROTEGIDO Inserta una lista de productos en un pedido existente.
        /// </summary>
        /// <param name="idpedido">Id del pedido al cual se agregarán los productos.</param>
        /// <param name="productos">Lista de productos a agregar al pedido.</param>
        /// <response code="204">NoContent. Los productos se han agregado correctamente al pedido.</response>
        /// <response code="400">BadRequest. No se pudieron agregar los productos al pedido.</response>
        /// <response code="401">Unauthorized. No se ha proporcionado un token válido.</response>
        [Authorize]
        [HttpPost]
        [Route("[action]/{idpedido}")]
        public async Task<ActionResult> InsertPedidoProductos(int idpedido, List<PedidoProducto> productos)
        {
            int result = await this.repo.InsertListPedidoProductosAsync(idpedido, productos);
            if (result == 0)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
