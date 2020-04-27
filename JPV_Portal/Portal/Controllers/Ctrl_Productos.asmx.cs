using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using LitJson;
using Newtonsoft.Json;
using JPV_Portal.Modelo.Datos;
using JPV_Portal.Modelo.Negocio;
using JPV_Portal.Modelo.Ayudante;
using JPV_Portal.CORE;
using System.Collections.Specialized;

namespace JPV_Portal.Portal.Controllers
{
    /// <summary>
    /// Descripción breve de Ctrl_Productos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class Ctrl_Productos : System.Web.Services.WebService
    {
        ///******************************************************************************* 
        ///PROYECTO:Cafeteria Neurologos
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
        public String Consultar_Productos()
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            try
            {
                Dt_Registros = Controlador.Consulta_Productos();
                if (Dt_Registros != null)
                {
                    if (Dt_Registros.Rows.Count > 0)
                        Obj_Respuesta.Registros = JsonConvert.SerializeObject(Dt_Registros, Newtonsoft.Json.Formatting.None);

                    Obj_Respuesta.Mensaje = "ok";
                    Obj_Respuesta.Estatus = true;

                    Obj_Respuesta.data = JsonConvert.SerializeObject(Dt_Registros, Formatting.None);

                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Consultar Producto [" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
        }
        ///******************************************************************************* 
        ///PROYECTO:Cafeteria Neurologos
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
        public String Consultar_Categorias()
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            DataTable Dt_Registros = new DataTable();
            try
            {
                Dt_Registros = Controlador.Consultar_Categorias(new Cls_Mdl_Productos());
                if (Dt_Registros != null)
                {
                    if (Dt_Registros.Rows.Count > 0)
                        Obj_Respuesta.Registros = JsonConvert.SerializeObject(Dt_Registros, Newtonsoft.Json.Formatting.None);

                    Obj_Respuesta.Mensaje = "ok";
                    Obj_Respuesta.Estatus = true;

                    Obj_Respuesta.data = JsonConvert.SerializeObject(Dt_Registros, Formatting.None);

                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Consultar Producto [" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
        }
        ///******************************************************************************* 
        ///PROYECTO:Cafeteria Neurologos
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
        public void Cargar_Cmb()
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
                                //tag2 = Fila.Field<Decimal>("Cuentas_Pendientes"),
                            };

                foreach (var p in Datos)
                    Lista_Select.Add((Cls_Select2)p);

                Json_Resultado = JsonMapper.ToJson(Lista_Select);
                Context.Response.Write(Json_Resultado);

            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Consultar [" + Ex.Message + "]";
            }

        }
        ///******************************************************************************* 
        ///PROYECTO:Cafeteria Neurologos
        ///NOMBRE DE LA FUNCIÓN: Ope_Alta
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
        public String Ope_Alta(String Datos)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Cls_Mdl_Productos Obj_Capturado = new Cls_Mdl_Productos();
            JsonSerializerSettings Configuracion_Json = new JsonSerializerSettings();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;

            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_Productos>(Datos);
                Obj_Capturado.Usuario_Creo = Sessiones.Usuario;
                //Obj_Capturado.Fecha_Creo = "(getate())";

                if (Controlador.Guardar_Productos(Obj_Capturado))
                {
                    Obj_Respuesta.Mensaje = "ok";
                    Obj_Respuesta.Estatus = true;
                }
            }
            catch (Exception Ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Guardar [" + Ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonMapper.ToJson(Obj_Respuesta);
            }
            return Json_Resultado;
        }
        ///******************************************************************************* 
        ///PROYECTO:Cafeteria Neurologos
        ///NOMBRE DE LA FUNCIÓN: Ope_Alta
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
        public String Ope_Modificar(String Datos)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Cls_Mdl_Productos Obj_Capturado = new Cls_Mdl_Productos();
            JsonSerializerSettings Configuracion_Json = new JsonSerializerSettings();
            Respuesta Obj_Respuesta = new Respuesta();
            String Json_Resultado = String.Empty;
            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_Productos>(Datos);
                Obj_Capturado.Usuario_Modifico = Sessiones.Usuario;

                if (Controlador.Actualizar_Productos(Obj_Capturado))
                {
                    Obj_Respuesta.Estatus = true;
                    Obj_Respuesta.Mensaje = "Registro exitoso.";
                }
            }
            catch (Exception ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Modificar[" + ex.Message + "]";
            }
            finally
            {
                Json_Resultado = JsonConvert.SerializeObject(Obj_Respuesta, Formatting.Indented, Configuracion_Json);
            }
            return Json_Resultado;
        }
        ///******************************************************************************* 
        ///PROYECTO:Cafeteria Neurologos
        ///NOMBRE DE LA FUNCIÓN: Eliminar
        ///DESCRIPCIÓN: ELIMINA UN REGISTRO, YA SEA DE LA TABLA PRODUCTOS O CATEGORIAS
        ///PARAMETROS:  
        ///CREO:       
        ///FECHA_CREO:  
        ///MODIFICO: 
        ///FECHA_MODIFICO:
        ///CAUSA_MODIFICACIÓN:
        ///*******************************************************************************
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String Eliminar(String Datos)
        {
            Cls_Ctrl_Operaciones Controlador = new Cls_Ctrl_Operaciones();
            Cls_Mdl_Productos Obj_Capturado = new Cls_Mdl_Productos();
            JsonSerializerSettings Configuracion_Json = new JsonSerializerSettings();
            Respuesta Obj_Respuesta = new Respuesta();
            String Str_Respuesta = String.Empty;
            try
            {
                Obj_Capturado = JsonConvert.DeserializeObject<Cls_Mdl_Productos>(Datos);
                if (Controlador.Eliminar_Productos(Obj_Capturado))
                {
                    Obj_Respuesta.Estatus = true;
                    Obj_Respuesta.Mensaje = "Registro exitoso.";
                }
                else
                {
                    Obj_Respuesta.Estatus = false;
                    Obj_Respuesta.Mensaje = "No se logro eliminar la categoria debido a que existen productos que pertenecen a esta.";
                }
            }
            catch (Exception ex)
            {
                Obj_Respuesta.Estatus = false;
                Obj_Respuesta.Mensaje = "Eliminar[" + ex.Message + "]";
            }
            finally
            {
                Str_Respuesta = JsonConvert.SerializeObject(Obj_Respuesta, Formatting.Indented, Configuracion_Json);
            }
            return Str_Respuesta;
        }
    }
}
