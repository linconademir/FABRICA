using CEAG.DOMINIO;
using CEAG.WEB.Constants;
using CEAG.WEB.ViewModel.Funcionario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace CEAG.WEB.Models
{
    public static class SendEmail
    {
        public static void EnviarNotificacaoProfessor(Funcionario funcionario)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("centroteste0401@gmail.com", "31402023");
            MailMessage mail = new MailMessage();
            mail.Sender = new System.Net.Mail.MailAddress("colegioceag@gmail.com", "ENVIADOR");
            mail.From = new MailAddress("colegioceag@gmail.com", "ENVIADOR");
            mail.To.Add(new MailAddress("nicolegaldino@gmail.com; linconademir@gmail.com", "RECEBEDOR"));
            mail.Subject = "Contato";
            mail.Body = " Mensagem do site:<br/> Nome:  TESTE <br/> Email : linconademir@hotmail.com <br/> Mensagem : Caro professor, você precisa lançar a nota no sistema";
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            try
            {
                client.Send(mail);
            }
            catch (System.Exception erro)
            {
                //trata erro
            }
            finally
            {
                mail = null;
            }
        }

        public static void EnviarEmail(Email email)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("centroteste0401@gmail.com", "31402023");
            MailMessage mail = new MailMessage();
            mail.Sender = new System.Net.Mail.MailAddress("colegioceag@gmail.com", "ENVIADOR");
            mail.From = new MailAddress("colegioceag@gmail.com", "ENVIADOR");
            mail.To.Add(new MailAddress(email.Recebedor, "RECEBEDOR"));
            mail.Subject = email.Assunto;
            mail.Body = email.Mensagem; 
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            try
            {
                client.Send(mail);
            }
            catch (System.Exception erro)
            {
                //trata erro
            }
            finally
            {
                mail = null;
            }
        }
    }
}