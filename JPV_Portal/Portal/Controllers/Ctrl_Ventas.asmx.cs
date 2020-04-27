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
using LitJson;
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
using JPV_Portal.CORE;

namespace JPV_Portal.Portal.Controllers
{
    /// <summary>
    /// Descripción breve de Ctrl_Ventas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Ctrl_Ventas : System.Web.Services.WebService
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
        public void Cargar_Cmb_Categoria()
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            List<Cls_Select2> Lista_Select = new List<Cls_Select2>();
            DataTable Dt_Registros = new DataTable();
            try
            {
                String Str_Q = String.Empty;
                NameValueCollection nvc = Context.Request.Form;
                Dt_Registros = Controlador.Consulta_Categorias(nvc["q"].ToString().Trim());
                if (!String.IsNullOrEmpty(nvc["q"]))
                    Str_Q = nvc["q"].ToString().Trim();

                var Datos = from Fila in Dt_Registros.AsEnumerable()
                            orderby Fila.Field<int>("Categoria_ID") ascending
                            select new Cls_Select2
                            {
                                id = Fila.Field<int>("Categoria_ID"),
                                text = Fila.Field<String>("Categoria"),
                            };

                foreach (var p in Datos)
                    Lista_Select.Add((Cls_Select2)p);

                Json_Resultado = JsonMapper.ToJson(Lista_Select);
                Context.Response.Write(Json_Resultado);

            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Consultar Producto [" + Ex.Message + "]";
            }

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
        public void Cargar_Cmb_Producto()
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            List<Cls_Select2> Lista_Select = new List<Cls_Select2>();
            DataTable Dt_Registros = new DataTable();
            try
            {
                String Str_Q = String.Empty;
                NameValueCollection nvc = Context.Request.Form;
                Dt_Registros = Controlador.Consulta_Producto(nvc["q"].ToString().Trim(), nvc["page"].ToString());
                if (!String.IsNullOrEmpty(nvc["q"]))
                    Str_Q = nvc["q"].ToString().Trim();

                var Datos = from Fila in Dt_Registros.AsEnumerable()
                            orderby Fila.Field<int>("Producto_ID") descending
                            select new Cls_Select2
                            {
                                id = Fila.Field<int>("Producto_ID"),
                                text = Fila.Field<String>("Producto"),
                                tag2 = Fila.Field<Decimal>("Precio"),

                            };

                foreach (var p in Datos)
                    Lista_Select.Add((Cls_Select2)p);

                Json_Resultado = JsonMapper.ToJson(Lista_Select);
                Context.Response.Write(Json_Resultado);

            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Consultar Producto [" + Ex.Message + "]";
            }

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
        public String Folio()
        {
            Cls_Ctrl_Operaciones Obj_Negocio = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            Obj_Respuesta.Registros = "{}";

            try
            {
                Dt_Registros = Obj_Negocio.Nuevo_Folio();
                if (Dt_Registros != null)
                {
                    if (Dt_Registros.Rows.Count > 0)
                        Obj_Respuesta.Registros = JsonConvert.SerializeObject(Dt_Registros, Formatting.None);

                    Obj_Respuesta.Mensaje = "ok";
                    Obj_Respuesta.Estatus = true;
                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Folio[" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
        }
        ////******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: 
        ///DESCRIPCIÓN:
        ///PARAMETROS:  
        ///CREO:       MARIA CHANTAL ORIGEL SEGURA
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************      
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Imprimir(String Datos, String Items_Lista)
        {
            Cls_Mdl_Ventas Obj_Negocios = new Cls_Mdl_Ventas();
            Cls_Ctrl_Operaciones Obj_Negocio = new Cls_Ctrl_Operaciones();
            List<Cls_Mdl_Ventas> Lista_Materiales = new List<Cls_Mdl_Ventas>();
            Respuesta Obj_Resp = new Respuesta();
            DataTable Solicitud_ID = new DataTable();
            JsonSerializerSettings Configuracion_Json = new JsonSerializerSettings();
            Configuracion_Json.NullValueHandling = NullValueHandling.Ignore;
            String Str_Respuesta;
            CultureInfo ci = new CultureInfo("en-us");
            DataTable Dt_Parametros = new DataTable();

            try
            {
                Obj_Negocios = JsonConvert.DeserializeObject<Cls_Mdl_Ventas>(Datos);
                Lista_Materiales = JsonConvert.DeserializeObject<List<Cls_Mdl_Ventas>>(Items_Lista);

                Obj_Negocios.Usuario_Creo = Sessiones.Usuario;

                Dt_Parametros = Obj_Negocio.Consultar_Parametros();

                if (Obj_Negocio.Guardar_Venta(Obj_Negocios, Lista_Materiales))
                {
                    Obj_Resp.Estatus = true;
                    Obj_Resp.Mensaje = "Registro exitoso.";
                }

                //activar cuando viole lo solicite
                //Imprimir_Ticket(Obj_Negocios, Lista_Materiales, Dt_Parametros);

                Obj_Resp.Estatus = true;
                Obj_Resp.Mensaje = "Registro exitoso.";
            }
            catch (Exception ex)
            {
                //Documento.Close();
                Obj_Resp.Estatus = false;
                Obj_Resp.Mensaje = "Guardar[" + ex.Message + "]";
            }
            finally
            {
                Str_Respuesta = JsonConvert.SerializeObject(Obj_Resp, Formatting.Indented, Configuracion_Json);
            }
            return Str_Respuesta;
        }
        ////******************************************************************************* 
        ///NOMBRE DE LA FUNCIÓN: 
        ///DESCRIPCIÓN:
        ///PARAMETROS:  
        ///CREO:       MARIA CHANTAL ORIGEL SEGURA
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************     
        private void Imprimir_Ticket(Cls_Mdl_Ventas Reporte_Datos, List<Cls_Mdl_Ventas> Detalles,  DataTable Parametros)
        {
            try
            {
                  CrearTicket ticket = new CrearTicket();
                    //ticket.Imprimir_Logo();
                    ticket.TextoIzquierda(" ");
                    ticket.TextoCentro("CAFETERIA");
                    //ticket.TextoCentro("'NEUROLOGOS'");
                    if (Parametros.Rows.Count > 0)
                    {
                        //ticket.TextoCentro("" + Parametros.Rows[0]["Domicilio"]);
                        //ticket.TextoCentro("" + Parametros.Rows[0]["RFC"]);
                        //ticket.TextoCentro("TEL." + Parametros.Rows[0]["Telefono"]);
                    }
                    ticket.TextoIzquierda(" ");
                    ticket.TextoDerecha("Folio: " + Reporte_Datos.Folio);
                    ticket.TextoIzquierda(" ");
                    ticket.TextoIzquierda("Fecha:" + Reporte_Datos.Fecha + "     " + Reporte_Datos.Dia);
                    ticket.TextoIzquierda("");
                    //ticket.TextoIzquierda("Cliente:" + Reporte_Datos.Cliente);
                    //ticket.TextoIzquierda("");
                    ticket.EncabezadoVenta();
                    ticket.lineasGuio();
                    var Cont = Detalles.Count();

                    for (var i = 0; i < Cont; i++)
                    {
                        ticket.AgregaArticulo(System.Convert.ToDecimal(Detalles[i].Cantidad.ToString()), Detalles[i].Descripcion.ToString(), System.Convert.ToDecimal(Detalles[i].Precio.ToString()), System.Convert.ToDecimal(Detalles[i].Importe.ToString()));
                    }

                    ticket.lineasIgual();
                    ticket.AgregarTotales("          Subtotal : $ ", System.Convert.ToDecimal(Reporte_Datos.Subtotal));
                    ticket.AgregarTotales("          IVA  : $ ", System.Convert.ToDecimal(Reporte_Datos.IVA));
                    ticket.TextoIzquierda(" ");
                    ticket.AgregarTotales("          TOTAL       : $ ", System.Convert.ToDecimal(Reporte_Datos.Total));
                    ticket.TextoIzquierda(" ");
                    ticket.TextoIzquierda("Este ticket forma parte de la factura");
                    ticket.TextoIzquierda("global del dia ");
                    ticket.TextoIzquierda(" ");
                    ticket.TextoCentro("GRACIAS POR SU COMPRA");
                    ticket.TextoCentro("VUELVA PRONTO");
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("");
                    ticket.TextoIzquierda("");
                    ticket.CortaTicket();
                    ticket.ImprimirTicket("" + Parametros.Rows[0]["Impresora"]);
           
            }
            catch (Exception ex)
            {

                //throw new Exception(ex.Message);
            }
        }

    }
}
