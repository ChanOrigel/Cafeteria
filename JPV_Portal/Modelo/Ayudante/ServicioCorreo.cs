using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JPV_Portal.Modelo.Negocio;
using JPV_Portal.Modelo.Datos;
using System.Net.Mail;

namespace JPV_Portal.Modelo.Ayudante
{
    public class ServicioCorreo
    {
        //datos del mail
        public String Envia { get; set; }
        public String Recibe { get; set; }
        public String Subject { get; set; }
        public String Texto { get; set; }
        //datos de configuracion
        public String Servidor;
        public String Password;
        public String Puerto;
        public String SSL;
        public String Adjunto;

        /*******************************************************************************
        NOMBRE DE LA FUNCIÓN: Enviar_Correo_Generico
        DESCRIPCIÓN: Realiza el envio de correos
        PROPIEDADES: 
        METODOS    : 
        CREO:        José Antonio López Hernández
        FECHA_CREO: 11/Septiembre/2015 13:40
        MODIFICO:
        FECHA_MODIFICO: 
        CAUSA_MODIFICACIÓN:
        /*******************************************************************************/

        public String Enviar_Correo_Generico(ServicioCorreo Datos)
        {
            Cls_Mdl_Parametros Consulta_Parametros = new Cls_Mdl_Parametros();

            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            DataTable Dt_Servidor = Controlador.Consulta_Parametros();
            String[] correo = Datos.Recibe.Split(';');
            try
            {
                String Mi_SQL = String.Empty;
                String Servidor_Correo = String.Empty;

                if (Dt_Servidor.Rows.Count > 0)
                {
                    Servidor_Correo = Dt_Servidor.Rows[0]["Servidor"].ToString();
                    Envia = Dt_Servidor.Rows[0]["Correo"].ToString();
                    Password = Dt_Servidor.Rows[0]["Contrasena"].ToString();
                    Puerto = Dt_Servidor.Rows[0]["Puerto"].ToString();
                    SSL = Dt_Servidor.Rows[0]["Cifrar_Conexion"].ToString();
                }
                Dt_Servidor.Dispose();

                //Si existe parametro de servidor continua con el proceso
                if (!String.IsNullOrEmpty(Servidor_Correo))
                {


                    MailMessage email = new MailMessage();
                    Attachment Archivo_Mail = null;

                    email.To.Clear();
                    email.To.Add(Datos.Recibe);

                    email.From = new MailAddress(Envia, "Cafeteria");
                    email.Subject = Datos.Subject;
                    email.SubjectEncoding = System.Text.Encoding.UTF8;

                    email.Body = Datos.Texto;
                    email.BodyEncoding = System.Text.Encoding.UTF8;
                    email.IsBodyHtml = true;

                    if (Adjunto != null)
                    {
                        Archivo_Mail = new Attachment(Adjunto);
                        email.Attachments.Add(Archivo_Mail);
                    }

                    //Valida que existan datos de correo y password de envio
                    if (!String.IsNullOrEmpty(Envia))
                    {
                        //Envia el correo
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = Servidor_Correo;
                        smtp.Port = int.Parse(Puerto);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new System.Net.NetworkCredential(Envia, Password);

                        if (SSL == "Si")
                            smtp.EnableSsl = true;

                        smtp.Send(email);
                        email = null;
                        return "OK";
                    }
                    else
                    {
                        return "No existen datos para el correo de envío.";
                    }
                }
                else
                {
                    return "No existe servidor de correo configurado en los parametros.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }//fin de enviar correo generico
        public bool RecuperarPassword(string EmailSend, string mensaje, string subject)
        {
            Cls_Mdl_Parametros Consulta_Parametros = new Cls_Mdl_Parametros();

            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            DataTable Dt_Servidor = Controlador.Consulta_Parametros();
            //String[] correo = Datos.Recibe.Split(';');
            try
            {
                String Mi_SQL = String.Empty;
                String Servidor_Correo = String.Empty;

                if (Dt_Servidor.Rows.Count > 0)
                {
                    Servidor_Correo = Dt_Servidor.Rows[0]["Servidor"].ToString();
                    Envia = Dt_Servidor.Rows[0]["Correo"].ToString();
                    Password = Dt_Servidor.Rows[0]["Contrasena"].ToString();
                    Puerto = Dt_Servidor.Rows[0]["Puerto"].ToString();
                    SSL = Dt_Servidor.Rows[0]["Cifrar_Conexion"].ToString();
                }
                Dt_Servidor.Dispose();

                //Si existe parametro de servidor continua con el proceso
                if (!String.IsNullOrEmpty(Servidor_Correo))
                {


                    MailMessage email = new MailMessage();
                    Attachment Archivo_Mail = null;

                    email.To.Clear();
                    email.To.Add(EmailSend);

                    email.From = new MailAddress(Envia, "Cafeteria");
                    email.Subject = subject;
                    email.SubjectEncoding = System.Text.Encoding.UTF8;

                    var Mensaje = "<html>";
                    Mensaje += "<body>";
                    Mensaje += "<p style='text-align:justify;font-size:15px;font-family:Century Gothic; color:#000;'>Buen día.</p>";
                    Mensaje += "<br />";
                    Mensaje += "<p style='text-align:justify;font-size:15px;font-family:Century Gothic; color:#000;'>Se ha solicitado la recuperacion de contraseña del portal Kopi-Cafeteria.</p>";
                    Mensaje += "<br />";
                    Mensaje += "<p style='text-align:justify;font-size:15px;font-family:Century Gothic; color:#000;'>Contraseña : " + mensaje + "</p>";
                    Mensaje += "<br />";
                    Mensaje += "<p style='text-align:justify;font-size:12px;font-family:Century Gothic; color:#000'>NOTA: Este mensaje es generado de forma automática, favor de no responder este correo.</p>";
                    Mensaje += "<br />";
                    Mensaje += "</body>";
                    Mensaje += "</html>";

                    email.Body = Mensaje;
                    email.BodyEncoding = System.Text.Encoding.UTF8;
                    email.IsBodyHtml = true;

                    if (Adjunto != null)
                    {
                        Archivo_Mail = new Attachment(Adjunto);

                        email.Attachments.Add(Archivo_Mail);
                    }

                    //Valida que existan datos de correo y password de envio
                    if (!String.IsNullOrEmpty(Envia))
                    {
                        //Envia el correo
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = Servidor_Correo;
                        smtp.Port = int.Parse(Puerto);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new System.Net.NetworkCredential(Envia, Password);

                        if (SSL == "Si")
                            smtp.EnableSsl = true;

                        smtp.Send(email);
                        email = null;

                        return true;
                    }
                    else
                    {
                        return false;

                        //return "No existen datos para el correo de envío.";
                    }
                }
                else
                {
                    return false;

                    //return "No existe servidor de correo configurado en los parametros.";
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }//fin de enviar correo generico
    }
}