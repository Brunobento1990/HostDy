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
        private const string Countries_Key = "Countries";
        public EmailController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public IActionResult EnviarEmail(string email)
        {
            if (_memoryCache.TryGetValue(Countries_Key, out List<DadosIBGEDto> dadosIbge))
            {
                    var result = _serviceEmail.EnviarListCidades(dadosIbge,email);

                    if (result) return Ok("E-mail enviado com sucesso!");

            }
            else
            {
                var serviceCidades = new ServiceCidades();
                var serviceDadosIBGE = new ServiceDadosIBGE();
                var dados = serviceCidades.GetDadosIBGE();
                var dadosIBGE = serviceDadosIBGE.PreencherLista(dados);
                var result = _serviceEmail.EnviarListCidades(dadosIBGE, email);

                if (result) return Ok("E-mail enviado com sucesso!");
            }
            return NotFound("Não foi possível enviar o e-mail!");
        }
    }
}
