using HostDy.Dtos;
using HostDy.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostDy.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private static ServiceEmail _serviceEmail = new ServiceEmail();
        private readonly IMemoryCache _memoryCache;
        private const string Countries_Key_Usuario = "Countries_Usuario";
        private const string Countries_Key = "Countries";
        public EmailController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public IActionResult EnviarEmail()
        {
            if (_memoryCache.TryGetValue(Countries_Key, out List<DadosIBGEDto> dadosIbge))
            {
                if (_memoryCache.TryGetValue(Countries_Key_Usuario, out UsuarioDto usuario))
                {
                    var result = _serviceEmail.EnviarListCidades(dadosIbge,usuario.Email);

                    if (result) return Ok("E-mail enviado com sucesso!");
                }
            }
            return NotFound("Não foi possível enviar o e-mail!");
        }
    }
}
