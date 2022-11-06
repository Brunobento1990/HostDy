using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostDy.Models
{
    public class Regiaoimediata
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        [JsonProperty("regiao-intermediaria")]
        public Regiaointermediaria Regiaontermediaria { get; set; }
    }
}
