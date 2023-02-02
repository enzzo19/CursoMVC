using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

// Referencias Necesarias para poder enviar un correo
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Runtime.InteropServices;

namespace CapaNegocio
{
    public class CN_Recursos
    {
        public static String GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6); // Solo de 6 digitos
            return clave;
        }
        
        // encriptacion de Texto en SHA256
        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();

            // USAR LA REFERENCIA DE "System.Security.Cryptography"

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }


    public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("despensa.el.grillo@gmail.com");
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient(){
                    Credentials = new NetworkCredential("despensa.el.grillo@gmail.com", "xenkxzlnoyadnnsl"),
                    Host = "smtp.gmail.com",
                    Port = 25, // Cambie el puerto por defecto para que funcione
                    EnableSsl = true
                };

                smtp.Send(mail);
                Console.Write("Correo Enviado");
                resultado = true;

            }
            catch (Exception ex)
            {
                Console.Write(ex);
                resultado = false;
            }
            return resultado;
        }

    }
}
