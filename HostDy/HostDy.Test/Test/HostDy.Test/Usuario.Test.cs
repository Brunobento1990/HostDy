using ExpectedObjects;
using HostDy.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Test.HostDy.Builders;
using Xunit;

namespace Test.HostDy.Test
{
    
    public class Usuario
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCriarUsuarioSenhaInvalido(string senha)
        {
            Assert.Throws<ArgumentException>(() =>
            UsuarioBiulder.CriaUsuarioBiulder().ComSenha(senha).Build());
        }
        [Theory]
        [InlineData("")]
        [InlineData("brunoTest")]
        public void NaoDeveCriarUsuarioEmailInvalido(string email)
        {
            Assert.Throws<ArgumentException>(() =>
            UsuarioBiulder.CriaUsuarioBiulder().ComEmail(email).Build());
        }
    }
}
