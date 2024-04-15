using ApiAthanasia.Models.Util;
using ApiAthanasia.Models.Views;
using ApiAthanasia.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAthanasia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosSimplesViewController : ControllerBase
    {
        private IRepositoryAthanasia repo;

        public ProductosSimplesViewController(IRepositoryAthanasia repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductoSimpleView>>> ProductosSimplesView()
        {
            List<ProductoSimpleView> prodSimples = await this.repo.GetAllProductoSimpleViewAsync();
            return prodSimples;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoSimpleView>> ProductoSimple(int id)
        {
            ProductoSimpleView prodSimple = await this.repo.GetProductoSimpleByIdAsync(id);
            if (prodSimple == null)
            {
                return NotFound();
            }
            return prodSimple;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<ProductoSimpleView>>> DistinctTituloAutor()
        {
            List<ProductoSimpleView> prodSimple = await this.repo.GetProductoSimpleViewTituloAutorAsync();
            if (prodSimple == null)
            {
                return NotFound();
            }
            return prodSimple;
        }
        [HttpGet]
        [Route("[action]/{posicion}/{registros}")]
        public async Task<ActionResult<PaginacionModel<ProductoSimpleView>>> Paginacion(int posicion, int registros)
        {
            PaginacionModel<ProductoSimpleView> model = await this.repo.GetProductosSimplesPaginacionAsyn(posicion, registros);
            if (model == null)
            {
                return NotFound();
            }
            return model;
        }
        [HttpGet]
        [Route("[action]/{busqueda}/{posicion}/{registros}")]
        public async Task<ActionResult<PaginacionModel<ProductoSimpleView>>> PaginacionBusqueda(string busqueda, int posicion, int datos)
        {
            PaginacionModel<ProductoSimpleView> model = await this.repo.GetAllProductoSimpleViewSearchPaginacionAsync(busqueda, posicion, datos);
            if (model.Lista.Count == 0)
            {
                return NotFound();
            }
            return model;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<ProductoSimpleView>>> ByListId([FromQuery] List<int> id)
        {
            List<ProductoSimpleView> productos = await this.repo.GetAllProductoSimpleViewByIds(id);
            if (productos.Count == 0)
            {
                return NotFound();
            }
            return productos;
        }
        [HttpGet]
        [Route("[action]/{busqueda}/{posicion}/{registros}")]
        public async Task<ActionResult<PaginacionModel<ProductoSimpleView>>> PaginacionBusquedaCategoriasGeneros(string busqueda, int posicion, int registros, [FromQuery] List<int> idcategoria, [FromQuery] List<int> idgenero)
        {
            PaginacionModel<ProductoSimpleView> model = await this.repo.GetProductoSimpleViewsCategoriasGeneroAsync(busqueda, posicion, registros, idcategoria,idgenero);
            if (model.Lista.Count == 0)
            {
                return NotFound();
            }
            return model;
        }

    }
}
