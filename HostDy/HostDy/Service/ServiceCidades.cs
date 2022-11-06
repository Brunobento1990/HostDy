using HostDy.Controllers;
using HostDy.Dtos;
using HostDy.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HostDy.Service
{
    public class ServiceCidades
    {
        public List<DadosIBGE> GetDadosIBGE()
        {
            
            var dados = new List<DadosIBGE>();
            
            var url = string.Format("https://servicodados.ibge.gov.br/api/v1/localidades/municipios?orderBy=nome");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var respost = client.GetAsync(url).Result;
                    if (respost.IsSuccessStatusCode)
                    {
                        var resultado = respost.Content.ReadAsStringAsync().Result;
                        dados = JsonConvert.DeserializeObject<List<DadosIBGE>>(resultado);
                    }
                }
            }
            catch (Exception e)
            {
                dados = null;
            }

            return dados;
        }
    }
}
