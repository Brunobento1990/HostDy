using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HostDy.Dtos
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public UsuarioDto(string email,string senha)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (!rg.IsMatch(email))
                throw new ArgumentException();
                

            if(string.IsNullOrEmpty(senha))
                throw new ArgumentException();

            this.Email = email;
            this.Senha = senha;
            this.Ativo = true;
        }
        public UsuarioDto() { }

    }
}
