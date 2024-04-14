using ApiAthanasia.Helpers;
using ApiAthanasia.Models.Tables;
using ApiAthanasia.Models.Views;
using ApiAthanasia.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace ApiAthanasia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosViewController : ControllerBase
    {
        private IRepositoryAthanasia repo;

        public ProductosViewController(IRepositoryAthanasia repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductoView>>> ProductosView()
        {
            List<ProductoView> productos = await this.repo.GetAllProductoViewAsync();
            return productos;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductoView>> ProductoView(int id)
        {
            ProductoView productos = await this.repo.GetProductoByIdAsync(id);
            return productos;
        }

        [HttpGet]
        [Route("[action]/{idformato}")]
        public async Task<ActionResult<List<ProductoView>>> ByFormato(int idformato)
        {
            Formato formato = await this.repo.GetFormatoByIdAsync(idformato);
            List<ProductoView> productos = await this.repo.GetProductoViewByFormatoAsync(formato.Nombre);
            return productos;
        }
    }
}
