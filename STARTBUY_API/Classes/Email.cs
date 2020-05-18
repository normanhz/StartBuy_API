using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace STARTBUY_API.Classes
{
    public class Email
    {
        public bool SendRegistrationCode(string email, int code)
        {
            
            var user = "startbuy2020@gmail.com";
            var pass = "Startbuy2@2@";

            var message1 = "Utiliza el siguiente código para la verificación: ";
            var message2 = code.ToString();

            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(email));
            mail.From = new MailAddress(user);
            mail.Subject = "Confirmacion de Registro";
            mail.Body = message1.ToString() + message2.ToString();//BodyEmailCode(message1, message2);
            mail.IsBodyHtml = true;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.SubjectEncoding = System.Text.Encoding.Default;

            SmtpClient Smtp = new SmtpClient();
            Smtp.Port = 587;
            Smtp.Host = "smtp.gmail.com";
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            Smtp.EnableSsl = true;
            Smtp.UseDefaultCredentials = false;
            Smtp.Credentials = new System.Net.NetworkCredential(user, pass);
            try
            {
                Smtp.Send(mail);
            }
            catch(Exception ex)
            {

            }

            return true;
        }

        public bool SendTemporalPassword(string email, string name, string temporalpass)
        {
            var user = "startbuy2020@gmail.com";
            var pass = "startbuy2020";
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(email));
            mail.From = new MailAddress(user);
            mail.Subject = "Contraseña Temporal";
            mail.Body = "<p>Hola " + name + "</p><p>Se te asigno una contraseña temporal para que puedas acceder a nuestra app, la contraseña temporal es:</p><p><h3>" + temporalpass + "</h3></p></p><p>Puedes cambiarla en cualquier momento en las configuraciones de tu perfil.</p></p><p>iBeauty</p>";
            mail.IsBodyHtml = true;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.SubjectEncoding = System.Text.Encoding.Default;

            SmtpClient Smtp = new SmtpClient();
            Smtp.Port = 587;
            Smtp.Host = "smtp.gmail.com";
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            Smtp.EnableSsl = true;
            Smtp.UseDefaultCredentials = false;
            Smtp.Credentials = new System.Net.NetworkCredential(user, pass);
            try
            {
                Smtp.Send(mail);
            }
            catch
            {
            }
            return true;
        }

        private string BodyEmailCode(string param1, string param2)
        {
            string Body = @"
                    <table>
                        <tr>
                            <td>
                            <font face='source sans pro, helvetica neue, helvetica, arial, sans-serif' style='font-size: 25px;'>" + param1 + @"</font>
                            </td>    
                        </tr>
                            
                        <tr>
                            <td>
                            <font face='source sans pro, helvetica neue, helvetica, arial, sans-serif' style='font-size: 25px;'>" + param2 + @"</font>
                            </td>    
                        </tr>

                    </table>
        
                        ";
            return Body;
        }

    }
}
