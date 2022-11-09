using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HostDy.Models
{
    public class DadosIBGE : IDadosIBGE
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Microrregiao Microrregiao { get; set; }
        [JsonProperty("regiao-imediata")]
        public Regiaoimediata Regiaoimediata { get; set; }

    }
}
