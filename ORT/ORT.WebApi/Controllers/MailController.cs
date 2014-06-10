using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;

namespace ORT.WebApi.Controllers
{
    public class MailController : ApiController
    {
        public void SendMail(dynamic form)
        {
            var fromEmail = "instituto.ort.smtp@gmail.com";
            var fromPassword = "ort.smtp.2014";

            var toEmail = "ito1@ort.edu.ar";
            
            MailMessage mail = new MailMessage(fromEmail, toEmail);

            mail.Subject = "ORT ARGENTINA :: Consulta desde la aplicación Windows Phone";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Soy de ORT: " + form.UstedEs);
            sb.AppendLine("Estoy interesado en: " + form.TieneInteres);
            sb.AppendLine("Nombre y Apellido: " + form.Nombre + " " + form.Apellido);
            sb.AppendLine("Email: " + form.Mail);
            sb.AppendLine("Mensaje: " + form.Mensaje);

            mail.Body = sb.ToString();

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(fromEmail,
                                            fromPassword);
            
            client.Send(mail);
        }
    }
}