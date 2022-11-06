using HostDy.Dtos;
using HostDy.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostDy.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly IMemoryCache _memoryCache;
        private const string Countries_Key_Usuario = "Countries_Usuario";
        private readonly MemoryCacheEntryOptions _memory;
        private readonly ILogger<UsuarioController> _logger;
        public UsuarioController(ILogger<UsuarioController> logger,IMemoryCache memoryCache)
        {
            _memory = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                SlidingExpiration = TimeSpan.FromSeconds(1200)
            };
            _memoryCache = memoryCache;
            _usuarioRepository = new UsuarioRepository();
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetUsuario(string email,string senha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                _logger.LogError("Parâmetro incorreto!");
                return BadRequest("Parâmetro incorreto!");
            }
            var usuario = _usuarioRepository.GetUsuario(email,senha);

            if (usuario != null)
            {
                _logger.LogError("Login = " + usuario.Email);
                _memoryCache.Set(Countries_Key_Usuario, usuario, _memory);
                return Ok(usuario);
            }
            _logger.LogError("Acesso negado!");
            return NotFound("Usuário ou senha incorreta!");
        }
        [HttpPost]
        public IActionResult CreateUsuario(UsuarioDto usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Senha))
            {
                _logger.LogError("Parâmetro incorreto!");
                return BadRequest("Parâmetros inválidos");
            }
            if (_usuarioRepository.CreateUsuario(usuario))
            {
                _logger.LogError("Login = " + usuario.Email);
                _memoryCache.Set(Countries_Key_Usuario, usuario, _memory);
                return Created("Usuário cadastrado com sucesso!", usuario);
            }
            _logger.LogError("Erro ao cadastrar usuario!");
            return NotFound("Ocorreu um erro, tente novamente mais tarde!");
        }
    }
}
