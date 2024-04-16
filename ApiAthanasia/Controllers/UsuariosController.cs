using ApiAthanasia.Models.Tables;
using ApiAthanasia.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiAthanasia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IRepositoryAthanasia repo;

        public UsuariosController(IRepositoryAthanasia repo)
        {
            this.repo = repo;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Usuario(int id)
        {
            Usuario usuario = await this.repo.FindUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Usuario>> UsuarioToken([FromHeader]string authentication)
        {
            string token = authentication.Replace("bearer ", "");
            Usuario usuario = await this.repo.GetUsuarioByTokenAsync(token);
            if (usuario==null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpPost]
        [Route("[action]/{nombre}/{apellido}/{email}/{password}")]
        public async Task<ActionResult<Usuario>> Registro(string nombre,string apellido,string email,string password)
        {
            Usuario user = await this.repo.RegistrarUsuarioAsync(nombre, apellido, email, password);
            if (user == null)
            {
                return BadRequest();
            }
            return user;
        }

        //[HttpPut]
        //public async Task<ActionResult<Usuario>> 



        //UpdateUsuarioAsync(int idusuario, string nombre, string apellido, string email, string? imagen)
        //DeleteUsuarioAsync(int idusuario),
        //ActivarUsuarioAsync(string token),LogInUserAsync(string email, string password)
        //UpdateUsuarioTokenAsync(int idusuario),
        //UpdateUsuarioPasswordAsync(int idusuario, string password)
    }
}
