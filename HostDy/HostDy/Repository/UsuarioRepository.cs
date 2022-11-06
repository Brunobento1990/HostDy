using HostDy.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HostDy.Dtos;

namespace HostDy.Repository
{
    public class UsuarioRepository
    {
        private static string _stringConection = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=HostDy;Data Source=" + Environment.MachineName;

        private SqlConnection _conexaoBanco
        {
            get
            {
                return new SqlConnection(_stringConection);
            }
        }
        public UsuarioDto GetUsuario(string email,string senha)
        {
            var usuario = new UsuarioDto();

            try
            {
                using (_conexaoBanco)
                {
                    var query = "select * from Usuario where Email = @email and Senha = @senha and Ativo = 1";
                    var parameters = new
                    {
                        email,
                        senha
                    };
                    usuario = _conexaoBanco.QueryFirstOrDefault<UsuarioDto>(query, parameters);
                }
            }
            catch (SqlException e)
            {
                usuario = null;
            }

            return usuario;
        }
        public bool CreateUsuario(UsuarioDto usuario)
        {
            var result = false;

            try
            {
                using (_conexaoBanco)
                {
                    var query = "insert into Usuario (Email,Senha,Ativo) Values(@email,@senha,@ativo)";
                    var parameters = new
                    {
                        usuario.Email,
                        usuario.Senha,
                        usuario.Ativo
                    };
                    _conexaoBanco.Query(query,parameters);
                    result = true;
                }
            }
            catch (SqlException e)
            {
                result = false;
            }

            return result;
        }
    }
}
