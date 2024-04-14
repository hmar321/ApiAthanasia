using ApiAthanasia.Models.Tables;
using ApiAthanasia.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiAthanasia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormatosController : ControllerBase
    {
        private IRepositoryAthanasia repo;

        public FormatosController(IRepositoryAthanasia repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Formato>>> Formatos()
        {
            List<Formato> formatos = await this.repo.GetAllFormatosAsync();
            return formatos;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Formato>> FindFormato(int id)
        {
            Formato formato = await this.repo.GetFormatoByIdAsync(id);
            return formato;
        }
    }
}
