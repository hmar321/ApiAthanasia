using ApiAthanasia.Helpers;
using ApiAthanasia.Models.Api;
using ApiAthanasia.Models.Tables;
using ApiAthanasia.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using static System.Net.Mime.MediaTypeNames;
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
        public async Task<ActionResult<Usuario>> FindUsuario(int id)
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
        public async Task<ActionResult<Usuario>> UsuarioTokenMail()
        {
            string[] header = HttpContext.Request.Headers["Authorization"].ToString().Split();
            string token = header[1];
            Usuario usuario = await this.repo.GetUsuarioByTokenAsync(token);
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Registro(UsuarioPost u)
        {
            Usuario usuario = await this.repo.RegistrarUsuarioAsync(u.Nombre, u.Apellido, u.Email, u.Password);
            if (usuario == null)
            {
                return BadRequest();
            }
            return usuario;
        }

        [HttpPut]
        public async Task<ActionResult<Usuario>> Update(UsuarioPut u)
        {
            Usuario usuario = await this.repo.UpdateUsuarioAsync(u.IdUsuario, u.Nombre, u.Apellido, u.Email, u.Imagen);
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpPut]
        [Route("[action]/{idusuario}/{newpassword}")]
        public async Task<ActionResult> UpdatePassword(int idusuario, string newpassword)
        {
            Usuario usuario = await this.repo.UpdateUsuarioPasswordAsync(idusuario, newpassword);
            if (usuario == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut]
        [Route("[action]/{idusuario}")]
        public async Task<ActionResult> UpdateToken(int idusuario)
        {
            Usuario usuario = await this.repo.UpdateUsuarioTokenAsync(idusuario);
            if (usuario == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut]
        [Route("[action]/{token}")]
        public async Task<ActionResult> ActivarUsuario(string token)
        {
            Usuario usuario = await this.repo.ActivarUsuarioAsync(token);
            if (usuario == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            int result = await this.repo.DeleteUsuarioAsync(id);
            if (result == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

       
    }
}
