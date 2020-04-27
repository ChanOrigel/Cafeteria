using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Web.Script.Services;
using System.Web.Services;
using System.Threading;
using CarlosAg.ExcelXmlWriter;
using LitJson;
using LibPrintTicket;
using Newtonsoft.Json;
using System.Web;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using System.Globalization;
using JPV_Portal.Reportes_Excel;
using JPV_Portal.Modelo.Datos;
using JPV_Portal.Modelo.Negocio;
using JPV_Portal.Modelo.Ayudante;
using JPV_Portal.ReportesExcel;
using JPV_Portal.CORE;

namespace JPV_Portal.Portal.Controllers
{
    /// <summary>
    /// Descripción breve de Ctrl_A_Pagar
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Ctrl_A_Pagar : System.Web.Services.WebService
    {
                
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: 
        ///DESCRIPCIÓN:
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Consultar_Ventas()
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_Balance Obj_Capturado = new Cls_Mdl_Balance();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            try
            {
                //Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_A_Pagar>(Parametros);


                Dt_Registros = Controlador.Consultar_Ventas();
                if (Dt_Registros != null)
                {
                    if (Dt_Registros.Rows.Count > 0)
                        Obj_Respuesta.Registros = JsonConvert.SerializeObject(Dt_Registros, Newtonsoft.Json.Formatting.None);

                    Obj_Respuesta.Mensaje = "ok";
                    Obj_Respuesta.Estatus = true;

                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Consultar Cliente [" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: 
        ///DESCRIPCIÓN:
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Iniciar_Formulario()
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_Balance Obj_Capturado = new Cls_Mdl_Balance();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            try
            {
                //Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_A_Pagar>(Parametros);
                Dt_Registros = Controlador.Iniciar_Formulario();
                if (Dt_Registros != null)
                {
                    if (Dt_Registros.Rows.Count > 0)
                        Obj_Respuesta.Registros = JsonConvert.SerializeObject(Dt_Registros, Newtonsoft.Json.Formatting.None);

                    Obj_Respuesta.Mensaje = "ok";
                    Obj_Respuesta.Estatus = true;

                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Consultar Cliente [" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: 
        ///DESCRIPCIÓN:
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Actualizar(String Parametros)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_Balance Obj_Capturado = new Cls_Mdl_Balance();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_Balance>(Parametros);

                if (Controlador.Actualizar(Obj_Capturado))
                {

                    Obj_Respuesta.Mensaje = "ok";
                    Obj_Respuesta.Estatus = true;
                    Enviar_Correo_Actualizar(Obj_Capturado);
                    //falta enviar el email
                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Consultar Cliente [" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: 
        ///DESCRIPCIÓN:
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        /////*******************************************************************************
        private String Enviar_Correo_Actualizar(Cls_Mdl_Balance Obj_Negocios)
        {
            //Cls_Cat_Parametros_Negocio Obj_Negocio = new Cls_Cat_Parametros_Negocio();
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            ServicioCorreo Envio_Email = new ServicioCorreo();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_Balance Obj_Capturado = new Cls_Mdl_Balance();
            String Json_Resultado = String.Empty;
            String Json_Object = String.Empty;
            DataTable Dt_Dato = new DataTable();
            String Str_Json = String.Empty;
            String Mensaje = String.Empty;

            try
            {
                //Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_Balance>(Obj_Negocios);
                Dt_Dato = Controlador.Consulta_Parametros();

                if (!String.IsNullOrEmpty(Dt_Dato.Rows[0]["Correo_Destino"].ToString()))
                {
                    Mensaje = "<html>";
                    Mensaje += "<body>";
                    Mensaje += "<p style='text-align:justify;font-size:12px;font-family:Century Gothic; color:#000;'>Buen día.</p>";
                    Mensaje += "<br />";
                    Mensaje += "<p style='text-align:justify;font-size:12px;font-family:Century Gothic; color:#000;'>Se ha alterado el Inicio de Caja, de: " + Obj_Negocios.Inicio_Caja + " a : $" + Obj_Negocios.Inicio_Actualizado + "</p>";
                    Mensaje += "<br />";
                    Mensaje += "<p style='text-align:justify;font-size:11px;font-family:Century Gothic; color:#000'>NOTA: Este mensaje es generado de forma automática, favor de no responder este correo.</p>";
                    Mensaje += "<br />";
                    Mensaje += "</body>";
                    Mensaje += "</html>";

                    Envio_Email.Subject = "CAFETERIA - Actualizacion de Inicio de caja.";
                    Envio_Email.Texto = Mensaje;
                    Envio_Email.Recibe = HttpUtility.HtmlDecode(Dt_Dato.Rows[0]["Correo_Destino"].ToString());
                    Envio_Email.Enviar_Correo_Generico(Envio_Email);

                    Obj_Respuesta.Estatus = true;
                    Obj_Respuesta.Mensaje = "La actualizacion fue enviada a su correo electrónico, si no se encuentra en la bandeja de entrada, favor de buscarla en los correos spam.";
                }

            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Favor de introducir el correo de empleado correcto.";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }

            return Json_Resultado;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: 
        ///DESCRIPCIÓN:
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Enviar_Corte(String Parametros)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_Balance Obj_Capturado = new Cls_Mdl_Balance();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_Balance>(Parametros);
                Obj_Capturado.Usuario_Creo = Sessiones.Usuario;

                if (Controlador.Enviar_Corte(Obj_Capturado))
                {
                    Crear_Corte(Obj_Capturado);
                    Obj_Respuesta.Mensaje = "ok";
                    Obj_Respuesta.Estatus = true;

                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Consultar Cliente [" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: 
        ///DESCRIPCIÓN:  
        ///PARAMETROS:  
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Crear_Corte(Cls_Mdl_Balance Datos)
        {
            Cls_Mdl_Balance Obj_Negocio = new Cls_Mdl_Balance();
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            Obj_Respuesta.Registros = "{}";

            try
            {
                Dt_Registros = Controlador.Consultar_Ventas_Detalles();//corregir busqueda, para traes el inner join de Ventas y Ventas detalles

                if (Dt_Registros.Rows.Count > 0)
                {
                    Workbook Book_Reporte = new Workbook();
                    String Ruta = HttpContext.Current.Server.MapPath("~") + "\\Temporal";
                    String ruta_plantilla = System.AppDomain.CurrentDomain.BaseDirectory + "Plantillas\\CorteDiario.xlsx";
                    String nombre_archivo = "CorteDiario.xlsx";
                    String ruta_almacenamiento = Ruta + "\\" + nombre_archivo;

                    Rpt_Inventarios Obj_Reporte = new Rpt_Inventarios(ruta_plantilla, ruta_almacenamiento, Dt_Registros);
                    Obj_Reporte.Crear_Corte(Datos);

                    if (!Directory.Exists(Ruta))
                        Directory.CreateDirectory(Ruta);

                    HttpContext.Current.ApplicationInstance.Response.Clear();
                    HttpContext.Current.ApplicationInstance.Response.Buffer = true;
                    HttpContext.Current.ApplicationInstance.Response.ContentType = "application/vnd.ms-excel";
                    HttpContext.Current.ApplicationInstance.Response.AddHeader("Content-Disposition", "attachment; filename=" + nombre_archivo);
                    HttpContext.Current.ApplicationInstance.Response.Charset = "UTF-8";
                    HttpContext.Current.ApplicationInstance.Response.ContentEncoding = Encoding.Default;
                    HttpContext.Current.ApplicationInstance.Response.WriteFile(ruta_almacenamiento);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();

                    Obj_Respuesta.Registros = "CorteDiario.xlsx";
                    Obj_Respuesta.Estatus = true;

                    Enviar_Correo_Excel(ruta_almacenamiento);
                }
                else
                {
                    Obj_Respuesta.Mensaje = "No hay datos que mostrar.";
                    Obj_Respuesta.Estatus = false;
                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Exportar Excel[" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: 
        ///DESCRIPCIÓN:
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        /////*******************************************************************************
        private String Enviar_Correo_Excel(string adjunto)
        {
            //Cls_Cat_Parametros_Negocio Obj_Negocio = new Cls_Cat_Parametros_Negocio();
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            ServicioCorreo Envio_Email = new ServicioCorreo();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_Balance Obj_Capturado = new Cls_Mdl_Balance();
            String Json_Resultado = String.Empty;
            String Json_Object = String.Empty;
            DataTable Dt_Dato = new DataTable();
            String Str_Json = String.Empty;
            String Mensaje = String.Empty;

            try
            {
                //Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_Balance>(Obj_Negocios);
                Dt_Dato = Controlador.Consulta_Parametros();

                if (!String.IsNullOrEmpty(Dt_Dato.Rows[0]["Correo_Destino"].ToString()))
                {
                    Mensaje = "<html>";
                    Mensaje += "<body>";
                    Mensaje += "<p style='text-align:justify;font-size:12px;font-family:Century Gothic; color:#000;'>Buen día.</p>";
                    Mensaje += "<br />";
                    Mensaje += "<p style='text-align:justify;font-size:12px;font-family:Century Gothic; color:#000;'>Se adjunto el corte de turno </p>";
                    Mensaje += "<br />";
                    Mensaje += "<p style='text-align:justify;font-size:11px;font-family:Century Gothic; color:#000'>NOTA: Este mensaje es generado de forma automática, favor de no responder este correo.</p>";
                    Mensaje += "<br />";
                    Mensaje += "</body>";
                    Mensaje += "</html>";

                    Envio_Email.Subject = "CAFETERIA - Envio de Corte de caja.";
                    Envio_Email.Texto = Mensaje;
                    Envio_Email.Recibe = HttpUtility.HtmlDecode(Dt_Dato.Rows[0]["Correo_Destino"].ToString());
                    Envio_Email.Adjunto = adjunto;
                    Envio_Email.Enviar_Correo_Generico(Envio_Email);

                    Obj_Respuesta.Estatus = true;
                    Obj_Respuesta.Mensaje = "La actualizacion fue enviada a su correo electrónico, si no se encuentra en la bandeja de entrada, favor de buscarla en los correos spam.";
                }

            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Favor de introducir el correo de empleado correcto.";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }

            return Json_Resultado;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: 
        ///DESCRIPCIÓN:  
        ///PARAMETROS:  
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Enviar_Reporte(string Parametros)
        {
            Cls_Mdl_Balance Obj_Negocio = new Cls_Mdl_Balance();
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Cls_Mdl_Balance Obj_Capturado = new Cls_Mdl_Balance();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            Obj_Respuesta.Registros = "{}";

            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_Balance>(Parametros);

                Dt_Registros = Controlador.Consultar_Ventas_Detalles_Fechas(Obj_Capturado);

                if (Dt_Registros.Rows.Count > 0)
                {
                    Workbook Book_Reporte = new Workbook();
                    String Ruta = HttpContext.Current.Server.MapPath("~") + "\\Temporal";
                    String ruta_plantilla = System.AppDomain.CurrentDomain.BaseDirectory + "Plantillas\\ReporteFechas.xlsx";
                    String nombre_archivo = "ReporteFechas.xlsx";
                    String ruta_almacenamiento = Ruta + "\\" + nombre_archivo;

                    Rpt_Inventarios Obj_Reporte = new Rpt_Inventarios(ruta_plantilla, ruta_almacenamiento, Dt_Registros);
                    Obj_Reporte.Enviar_Reporte(Obj_Capturado);

                    if (!Directory.Exists(Ruta))
                        Directory.CreateDirectory(Ruta);

                    HttpContext.Current.ApplicationInstance.Response.Clear();
                    HttpContext.Current.ApplicationInstance.Response.Buffer = true;
                    HttpContext.Current.ApplicationInstance.Response.ContentType = "application/vnd.ms-excel";
                    HttpContext.Current.ApplicationInstance.Response.AddHeader("Content-Disposition", "attachment; filename=" + nombre_archivo);
                    HttpContext.Current.ApplicationInstance.Response.Charset = "UTF-8";
                    HttpContext.Current.ApplicationInstance.Response.ContentEncoding = Encoding.Default;
                    HttpContext.Current.ApplicationInstance.Response.WriteFile(ruta_almacenamiento);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();

                    Obj_Respuesta.Registros = "ReporteFechas.xlsx";
                    Obj_Respuesta.Estatus = true;

                    Enviar_Correo_Excel_Reporte(ruta_almacenamiento);
                }
                else
                {
                    Obj_Respuesta.Mensaje = "No hay datos que mostrar.";
                    Obj_Respuesta.Estatus = false;
                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Exportar Excel[" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
        }
        ///******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: 
        ///DESCRIPCIÓN:
        ///PARAMETROS:  
        ///CREO:       CHANTAL ORIGEL
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        /////*******************************************************************************
        private String Enviar_Correo_Excel_Reporte(string adjunto)
        {
            //Cls_Cat_Parametros_Negocio Obj_Negocio = new Cls_Cat_Parametros_Negocio();
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            ServicioCorreo Envio_Email = new ServicioCorreo();
            Respuesta Obj_Respuesta = new Respuesta();
            Cls_Mdl_Balance Obj_Capturado = new Cls_Mdl_Balance();
            String Json_Resultado = String.Empty;
            String Json_Object = String.Empty;
            DataTable Dt_Dato = new DataTable();
            String Str_Json = String.Empty;
            String Mensaje = String.Empty;

            try
            {
                //Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_Balance>(Obj_Negocios);
                Dt_Dato = Controlador.Consulta_Parametros();

                if (!String.IsNullOrEmpty(Dt_Dato.Rows[0]["Correo_Destino"].ToString()))
                {
                    Mensaje = "<html>";
                    Mensaje += "<body>";
                    Mensaje += "<p style='text-align:justify;font-size:12px;font-family:Century Gothic; color:#000;'>Buen día.</p>";
                    Mensaje += "<br />";
                    Mensaje += "<p style='text-align:justify;font-size:12px;font-family:Century Gothic; color:#000;'>Se adjunto el Reporte de ventas </p>";
                    Mensaje += "<br />";
                    Mensaje += "<p style='text-align:justify;font-size:11px;font-family:Century Gothic; color:#000'>NOTA: Este mensaje es generado de forma automática, favor de no responder este correo.</p>";
                    Mensaje += "<br />";
                    Mensaje += "</body>";
                    Mensaje += "</html>";

                    Envio_Email.Subject = "CAFETERIA - Envio de Reporte.";
                    Envio_Email.Texto = Mensaje;
                    Envio_Email.Recibe = HttpUtility.HtmlDecode(Dt_Dato.Rows[0]["Correo_Destino"].ToString());
                    Envio_Email.Adjunto = adjunto;
                    Envio_Email.Enviar_Correo_Generico(Envio_Email);

                    Obj_Respuesta.Estatus = true;
                    Obj_Respuesta.Mensaje = "La actualizacion fue enviada a su correo electrónico, si no se encuentra en la bandeja de entrada, favor de buscarla en los correos spam.";
                }

            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Favor de introducir el correo de empleado correcto.";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }

            return Json_Resultado;
        }

    }
}
