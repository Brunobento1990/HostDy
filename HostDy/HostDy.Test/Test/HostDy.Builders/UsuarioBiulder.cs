using HostDy.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.HostDy.Builders
{
    public class UsuarioBiulder
    {
        private int IdUsuario { get; set; }
        private string Email { get; set; } = "bruno.test";
        private string Senha { get; set; } = "123";
        private bool Ativo { get; set; }
        public static UsuarioBiulder CriaUsuarioBiulder()
        {
            return new UsuarioBiulder();
        }

        public UsuarioBiulder ComSenha(string senha)
        {
            this.Senha = senha;
            return this;
        }
        public UsuarioBiulder ComEmail(string email)
        {
            this.Email = email;
            return this;
        }
        public UsuarioDto Build()
        {
            return new UsuarioDto(this.Email,this.Senha);
        }
    }
}
