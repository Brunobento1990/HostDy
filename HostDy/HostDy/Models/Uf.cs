using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostDy.Models
{
    public class Uf
    {
        public int Id { get; set; }

        public string Sigla { get; set; }

        public string Nome { get; set; }

        public Regiao Regiao { get; set; }
    }
}
