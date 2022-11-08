using ClosedXML.Excel;
using HostDy.Dtos;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostDy.Service
{
    public class ServicePlanilha
    {

        //private List<DadosIBGEDto> dados = new List<DadosIBGEDto>();

        public XLWorkbook GerarPlanilha(List<DadosIBGEDto> dadosIbge)
        {
            var xlworkbook = new XLWorkbook();
            var planilha = xlworkbook.Worksheets.Add("Planilha1");
                
                try
                {
                    using (xlworkbook)
                    {
                        int i = 1;
                        planilha.Cell("A"+i).Value = "UF";
                        planilha.Cell("B"+i).Value = "Região";
                        planilha.Cell("C"+i).Value = "Cidade";
                        planilha.Cell("D"+i).Value = "Mesorregião";
                        planilha.Cell("E"+i).Value = "Cidade/UF";
                        i++;
                        foreach (DadosIBGEDto d in dadosIbge)
                        {
                            
                            planilha.Cell("A" + i).Value = d.SiglaEstado;
                            planilha.Cell("B" + i).Value = d.RegiaoNome;
                            planilha.Cell("C" + i).Value = d.NomeCidade;
                            planilha.Cell("D" + i).Value = d.NomeMesorregiao;
                            planilha.Cell("E" + i).Value = d.NomeFormatado;
                            i++;
                        }
                    xlworkbook.SaveAs("D:\\HostDy\\DadosIBGE.xlsx"); 
                }
                }
                catch (Exception e)
                {
                    xlworkbook = null;
                }
            return xlworkbook;
        }
    }
}
