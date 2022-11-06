using HostDy.Controllers;
using HostDy.Dtos;
using HostDy.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Threading.Tasks;

namespace HostDy.Service
{
    public class ServiceEmail
    {
        public ServicePlanilha _servicePlanilha = new ServicePlanilha();

        public bool EnviarListCidades(List<DadosIBGEDto> dadosIbge,string email)
        {
            var result = false;
                try
                {
                    _servicePlanilha.GerarPlanilha(dadosIbge);
                    MailMessage mail = new MailMessage("brunobentocaina@gmail.com", email);
                    mail.Subject = "Planilha";
                    mail.SubjectEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                    mail.Body = "Segue em anexo a planilha listando os dados do IBGE.";
                    mail.BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                    
                    Attachment att = new Attachment("D:\\HostDy\\DadosIBGE.xlsx");
                    mail.Attachments.Add(att);
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("brunobentocaina@gmail.com", "jhauzufhbxvwjved");
                    smtp.Send(mail);
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            return result;
        }
    }
}
