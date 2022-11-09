using HostDy.Dtos;
using HostDy.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HostDy.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioController> _logger;
        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _usuarioRepository = new UsuarioRepository();
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetUsuario(string email,string senha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                _logger.LogError("Parâmetro incorreto!");
                var errorParametro = new
                {
                    message = "Parâmetro incorreto!"
                };
                return BadRequest(errorParametro);
            }
            var usuario = _usuarioRepository.GetUsuario(email,senha);

            if (usuario != null)
            {
                _logger.LogWarning("Login = " + usuario.Email);
                return Ok(usuario);
            }
            _logger.LogError("Acesso negado!");
            var error = new
            {
                message = "Usuário ou senha incorreta!"
            };
            return NotFound(error);
        }
        [HttpPost]
        public IActionResult CreateUsuario(UsuarioDto usuario)
        {
            var usuarioCriado = new UsuarioDto(usuario.Email,usuario.Senha);
            if (usuarioCriado == null)
            {
                _logger.LogError("Parâmetro inválidos!");
                var errorCreate = new
                {
                    message = "E-mail ou senha inválidos!"
                };
                return BadRequest(errorCreate);
            }

            var usuarioVerifica = _usuarioRepository.GetUsuarioEmail(usuario.Email,usuario.Senha);
            if(usuarioVerifica != null)
            {
                _logger.LogError("Usuário já cadastrado");
                var errorCreate = new
                {
                    message = "Usuário já cadastrado"
                };
                return BadRequest(errorCreate);
            }

            if (_usuarioRepository.CreateUsuario(usuario))
            {
                _logger.LogWarning("Login = " + usuario.Email);

                return Created("Usuário cadastrado com sucesso!", usuario);
            }

            _logger.LogError("Erro ao cadastrar usuario!");
            var errorCreated = new
            {
                message = "Ocorreu um erro, tente novamente mais tarde!"
            };
            return NotFound(errorCreated);
        }
    }
}
