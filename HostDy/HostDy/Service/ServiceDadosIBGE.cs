using HostDy.Dtos;
using HostDy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostDy.Service
{
    public class ServiceDadosIBGE
    {
        public List<DadosIBGEDto> PreencherLista(List<DadosIBGE> dadosIBGE)
        {
            var dadosIBGEDtoList = new List<DadosIBGEDto>();
            foreach (DadosIBGE d in dadosIBGE)
            {
                var dadosIBGEDto = new DadosIBGEDto();
                dadosIBGEDto.SiglaEstado = d.Microrregiao.Mesorregiao.Uf.Sigla;
                dadosIBGEDto.NomeCidade = d.Nome;
                dadosIBGEDto.NomeFormatado = d.Nome + "/" + d.Microrregiao.Mesorregiao.Uf.Sigla;
                dadosIBGEDto.NomeMesorregiao = d.Microrregiao.Mesorregiao.Nome;
                dadosIBGEDto.RegiaoNome = d.Microrregiao.Nome;
                dadosIBGEDtoList.Add(dadosIBGEDto);
            }
            return dadosIBGEDtoList;
        }
    }
}
