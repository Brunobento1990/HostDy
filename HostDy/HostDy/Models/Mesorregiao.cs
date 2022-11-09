using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostDy.Models
{
    public class Mesorregiao : IDadosIBGE
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public Uf Uf { get; set; }
    }
}
