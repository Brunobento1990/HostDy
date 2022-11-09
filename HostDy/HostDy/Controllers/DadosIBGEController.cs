using HostDy.Dtos;
using HostDy.Service;
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
    public class DadosIBGEController : ControllerBase
    {
        private static ServiceCidades _serviceCidades;
        private static ServiceDadosIBGE _serviceDadosIBGE;
        private readonly ILogger<DadosIBGEController> _logger;
        public readonly IMemoryCache _memoryCache;
        private const string Countries_Key = "Countries";
        private readonly MemoryCacheEntryOptions _memory;

        public DadosIBGEController(ILogger<DadosIBGEController> logger,IMemoryCache memoryCache)
        {
            _serviceCidades = new ServiceCidades();
            _serviceDadosIBGE = new ServiceDadosIBGE();

            _memory = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                SlidingExpiration = TimeSpan.FromSeconds(1200)
            };
            _logger = logger;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult GetDadosIBGE()
        {
            if(_memoryCache.TryGetValue(Countries_Key , out List<DadosIBGEDto> dadosIbge))
            {
                return Ok(dadosIbge);
            }
            try
            {
                var dados = _serviceCidades.GetDadosIBGE();

                if (dados != null)
                {
                    
                    var dadosIBGE = _serviceDadosIBGE.PreencherLista(dados);
                    
                    if (dadosIBGE != null)
                    {
                        _memoryCache.Set(Countries_Key, dadosIBGE, _memory);
                        return Ok(dadosIBGE);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ocorreu uma exceção.");
            }
            _logger.LogError("Não há dados a serem apresentados!");

            return NotFound("Não há dados a serem apresentados!");
            
        }
    }
}
